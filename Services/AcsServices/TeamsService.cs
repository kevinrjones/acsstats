using System.Collections.Generic;
using System.Threading.Tasks;
using AcsCommands.Query;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsRepository;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Models;

namespace Services.AcsServices
{
    public class TeamsService : ITeamsService
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TeamsService> _logger;

        public TeamsService(IEfUnitOfWork unitOfWork, IMediator mediator, ILogger<TeamsService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Result<TeamDto, Error>> GetTeam(TeamId teamIdValue)
        {
            if (teamIdValue == 0)
                return await Task.FromResult(Result.Success<Team, Error>(new Team {Name = "All Teams"})).Map(t => new TeamDto(t.Id, t.Name, t.MatchType));
            
            return await _mediator.Send(new TeamQuery(teamIdValue));
        }

        public async Task<Result<IReadOnlyList<TeamRecordDetailsDto>, Error>> GetTeamRecords(
            SharedModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamRecordsForTeamVsOpponentsQuery(model,
                    "team_records_for_team_vs_opponent"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(
                    new TeamRecordsForTeamQuery(model, "team_records_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamRecordsVsOpponentsQuery(model,
                    "team_records_against_specified_opponent"));
            }

            return await _mediator.Send(
                new TeamRecordsCompleteQuery(model, "team_records_overall"));
        }

        public async Task<Result<IReadOnlyList<TeamRecordDetailsDto>, Error>> GetTeamSeriesRecords(
            SharedModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamRecordsForTeamVsOpponentsQuery(model,
                    "team_records_by_series_for_team_against_opponents"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(
                    new TeamRecordsForTeamQuery(model, "team_records_by_series_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamRecordsVsOpponentsQuery(model,
                    "team_records_by_series_against_opponents"));
            }

            return await _mediator.Send(
                new TeamRecordsCompleteQuery(model, "team_records_by_series_overall"));
        }

        public async Task<Result<IReadOnlyList<TeamRecordDetailsDto>, Error>> GetTeamGroundRecords(
            SharedModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamRecordsForTeamVsOpponentsQuery(model,
                    "team_records_by_ground_for_team_against_opponents"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(
                    new TeamRecordsForTeamQuery(model, "team_records_by_ground_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamRecordsVsOpponentsQuery(model,
                    "team_records_by_ground_against_opponents"));
            }

            return await _mediator.Send(
                new TeamRecordsCompleteQuery(model, "team_records_by_ground_overall"));

        }

        public async Task<Result<IReadOnlyList<TeamRecordDetailsDto>, Error>> GetTeamHostCountryRecords(
            SharedModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamRecordsForTeamVsOpponentsQuery(model,
                    "team_records_by_host_for_team_against_opponents"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(
                    new TeamRecordsForTeamQuery(model, "team_records_by_host_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamRecordsVsOpponentsQuery(model,
                    "team_records_by_host_against_opponents"));
            }

            return await _mediator.Send(
                new TeamRecordsCompleteQuery(model, "team_records_by_host_overall"));
        }

        public async Task<Result<IReadOnlyList<TeamRecordDetailsDto>, Error>> GetTeamOppositionRecords(
            SharedModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamRecordsForTeamVsOpponentsQuery(model,
                    "team_records_by_opp_for__against_opponents"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(
                    new TeamRecordsForTeamQuery(model, "team_records_by_opp_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamRecordsVsOpponentsQuery(model,
                    "team_records_by_opp_against_opponents"));
            }

            return await _mediator.Send(
                new TeamRecordsCompleteQuery(model, "team_records_by_opp_overall"));

        }

        public async Task<Result<IReadOnlyList<TeamRecordDetailsDto>, Error>> GetTeamByYearRecords(
            SharedModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamRecordsForTeamVsOpponentsQuery(model,
                    "team_records_by_year_for_team_against_opponents"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(
                    new TeamRecordsForTeamQuery(model, "team_records_by_year_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamRecordsVsOpponentsQuery(model,
                    "team_records_by_year_against_opponents"));
            }

            return await _mediator.Send(
                new TeamRecordsCompleteQuery(model, "team_records_by_year_overall"));

        }

        public async Task<Result<IReadOnlyList<TeamRecordDetailsDto>, Error>> GetTeamBySeasonRecords(
            SharedModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamRecordsForTeamVsOpponentsQuery(model,
                    "team_records_by_season_for_team_against_opponents"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(
                    new TeamRecordsForTeamQuery(model, "team_records_by_season_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamRecordsVsOpponentsQuery(model,
                    "team_records_by_season_against_opponents"));
            }

            return await _mediator.Send(
                new TeamRecordsCompleteQuery(model, "team_records_by_season_overall"));
        }

        public async Task<Result<IReadOnlyList<TeamExtrasDetailsDto>, Error>> GetTeamOverallExtrasRecords(
            SharedModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamOverallExtrasForTeamVsOpponentsQuery(model));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(
                    new TeamOverallExtrasForTeamQuery(model));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamOverallExtrasVsOpponentsQuery(model));
            }

            return await _mediator.Send(
                new TeamOverallExtrasCompleteQuery(model));
        }

        public async Task<Result<IReadOnlyList<InningsExtrasDetailsDto>, Error>> GetTeamInningsExtrasRecords(
            SharedModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamOverallInningsExtrasForTeamVsOpponentsQuery(model));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(
                    new TeamOverallInningsExtrasForTeamQuery(model));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new TeamOverallInningsExtrasVsOpponentsQuery(model));
            }

            return await _mediator.Send(
                new TeamOverallInningsExtrasCompleteQuery(model));
        }
    }
}