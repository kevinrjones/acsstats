using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcsCommands.Query;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsRepository;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Models;

namespace Services.AcsServices
{
    public class MatchesService : IMatchesService
    {
        private readonly IEfUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly ILogger<MatchesService> _logger;

        public MatchesService(IEfUnitOfWork unitOfWork, IMediator mediator, ILogger<MatchesService> logger)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _logger = logger;
        }

        // recordInputModel.homeVenue.Value | recordInputModel.awayVenue.Value | recordInputModel.neutralVenue.Value
        public async Task<Result<IReadOnlyList<MatchRecordDetailsDto>, AcsTypes.Error.Error>> GetHighestInningsForTeam(
            SharedModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new MatchScoresForTeamVsOpponentQuery(model,
                    "team_records_highest_innings_for_team_against_opponents"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(
                    new MatchScoresForTeamQuery(model, "team_records_highest_innings_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new MatchScoresVsOpponentQuery(model,
                    "team_records_highest_innings_against_opponents"));
            }

            return await _mediator.Send(
                new MatchScoresForTeamVsOpponentQuery(model, "team_records_highest_innings_overall"));
        }

        public async Task<Result<IReadOnlyList<MatchRecordDetailsDto>, AcsTypes.Error.Error>> GetMatchTotals(
            SharedModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new MatchScoresForTeamVsOpponentQuery(model,
                    "team_records_match_totals_for_team_against_opponents"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(
                    new MatchScoresForTeamQuery(model, "team_records_match_totals_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new MatchScoresVsOpponentQuery(model,
                    "team_records_match_totals_against_opponents"));
            }

            return await _mediator.Send(
                new MatchScoresCompleteQuery(model, "team_records_match_totals_overall"));
        }

        public async Task<Result<IReadOnlyList<MatchResultDto>, Error>> GetMatchResults(SharedModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new MatchResultForTeamVsOpponentsQuery(model,
                    "team_records_match_totals_for_team_against_opponents"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(
                    new MatchResultForTeamQuery(model, "team_records_match_totals_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new MatchResultVsOpponentsQuery(model,
                    "team_records_match_totals_against_opponents"));
            }

            return await _mediator.Send(
                new MatchResultCompleteQuery(model, "team_records_match_totals_overall"));
        }

        public async Task<Result<IReadOnlyList<MatchDateDto>, Error>> GetDatesForMatchType(string matchType)
        {
            var m = MatchType.Create(matchType);
            if (m.IsSuccess)
            {
                return await _mediator.Send(new MatchDatesQuery(m.Value));
            }

            return Result.Failure<IReadOnlyList<MatchDateDto>, Error>(m.Error);
        }

        public async Task<Result<IReadOnlyList<string>, Error>> GetSeriesDatesForMatchType(string matchType)
        {
            var m = MatchType.Create(matchType);
            if (m.IsSuccess)
            {
                return await _mediator.Send(new MatchSeriesDatesQuery(m.Value));
            }

            return Result.Failure<IReadOnlyList<string>, Error>(m.Error);
        }

        public async Task<Result<IReadOnlyList<string>, Error>> GetSeriesDatesForMatchTypes(string[] matchTypes)
        {
            var listMatchTypes = new List<MatchType>();
            foreach (var matchType in matchTypes)
            {
                var m = MatchType.Create(matchType);
                if (!m.IsSuccess)
                    return Result.Failure<IReadOnlyList<string>, Error>(m.Error);
                listMatchTypes.Add(m.Value);
            }

            return await _mediator.Send(new MatchSeriesDatesForMatchTypesQuery(listMatchTypes));
        }

        public async Task<Result<IReadOnlyList<string>, Error>> GetSeriesDatesForMatches(int homeTeamId, int awayTeamId,
            string matchType)
        {
            var m = MatchType.Create(matchType);
            if (m.IsSuccess)
            {
                return await _mediator.Send(new MatchesDatesQuery(homeTeamId, awayTeamId, m.Value));
            }

            return Result.Failure<IReadOnlyList<string>, Error>(m.Error);
        }

        public async Task<Result<IReadOnlyList<string>, Error>> GetTournamentsForSeason(string[] matchTypes,
            string season)
        {
            var listMatchTypes = new List<MatchType>();
            foreach (var matchType in matchTypes)
            {
                var m = MatchType.Create(matchType);
                if (!m.IsSuccess)
                    return Result.Failure<IReadOnlyList<string>, Error>(m.Error);
                listMatchTypes.Add(m.Value);
            }

            return await _mediator.Send(new TournamentByYearQuery(listMatchTypes, season));
        }

        public async Task<Result<IReadOnlyList<MatchListDto>, Error>> GetMatchesInTournament(string tournament)
        {
            var res = await _mediator.Send(new MatchesInTournamentQuery(tournament));
            return res;
        }

        public async Task<Result<IReadOnlyList<MatchListDto>, Error>> GetMatchesFromSearch(
            MatchSearchModel matchSearchModel)
        {
            string format = "yyyy-MM-dd";

            var startDate = EpochDateType.Create(matchSearchModel.StartDate, format);
            var endDate = EpochDateType.Create(matchSearchModel.EndDate, format);
            if (startDate.IsFailure || endDate.IsFailure)
                return Result.Failure<IReadOnlyList<MatchListDto>, Error>(
                    Errors.InvalidDateError("Either the start or end date is invalid"));
            var matchType = MatchType.Create(matchSearchModel.MatchType);
            if (!matchType.IsSuccess)
                return Result.Failure<IReadOnlyList<MatchListDto>, Error>(matchType.Error);

            return await _mediator.Send(new SearchQuery(matchSearchModel.HomeTeam, matchSearchModel.AwayTeam,
                startDate.Value,
                endDate.Value, matchSearchModel.Venue, matchSearchModel.MatchResult, matchType.Value, matchSearchModel.ExactHomeTeamMatch, matchSearchModel.ExactAwayTeamMatch));
        }
    }
}