using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcsRepository;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.Extensions.Logging;
using Services.Models;

namespace Services.AcsServices
{
    public class MatchesService : IMatchesService
    {
        private readonly IEfUnitOfWork _unitOfWork;
        private readonly ILogger<MatchesService> _logger;

        public MatchesService(IEfUnitOfWork unitOfWork, ILogger<MatchesService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // recordInputModel.homeVenue.Value | recordInputModel.awayVenue.Value | recordInputModel.neutralVenue.Value
        public async Task<Result<IReadOnlyList<MatchRecordDetails>, AcsTypes.Error.Error>> GetHighestInningsForTeam(
            SharedModel teamModel)
        {
            var venueId = teamModel.HomeVenue.Value | teamModel.AwayVenue.Value |
                          teamModel.NeutralVenue.Value;
            var sortBy = (int) teamModel.SortOrder;
            var sortDirection = teamModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (teamModel.TeamId.Value == 0 && teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetInningsByInnings(teamModel.MatchType.Value,
                    teamModel.Limit.Value, teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            if (teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetInningsByInningsForTeam(
                    teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.Limit.Value, teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy,
                    sortDirection);

            if (teamModel.TeamId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetInningsByInningsAgainstOpponents(
                    teamModel.MatchType.Value, teamModel.OpponentsId.Value, teamModel.Limit.Value,
                    teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId,
                    teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            return await _unitOfWork.MatchRecordDetailsRepository.GetInningsByInningsForTeamAgainstOpponents(
                teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.OpponentsId.Value, teamModel.Limit.Value,
                teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                venueId,
                teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                teamModel.MatchResult.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<MatchRecordDetails>, AcsTypes.Error.Error>> GetMatchTotals(
            SharedModel teamModel)
        {
            var venueId = teamModel.HomeVenue.Value | teamModel.AwayVenue.Value |
                          teamModel.NeutralVenue.Value;
            var sortBy = (int) teamModel.SortOrder;
            var sortDirection = teamModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (teamModel.TeamId.Value == 0 && teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetMatchTotalsHigherThan(
                    teamModel.MatchType.Value,
                    teamModel.Limit.Value, teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            if (teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetMatchTotalsHigherThanForTeam(
                    teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.Limit.Value, teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy,
                    sortDirection);

            if (teamModel.TeamId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetMatchTotalsHigherThanAgainstOpponents(
                    teamModel.MatchType.Value, teamModel.OpponentsId.Value, teamModel.Limit.Value,
                    teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId,
                    teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            return await _unitOfWork.MatchRecordDetailsRepository.GetMatchTotalsHigherThanForTeamAgainstOpponents(
                teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.OpponentsId.Value, teamModel.Limit.Value,
                teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                venueId,
                teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                teamModel.MatchResult.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<MatchResult>, Error>> GetMatchResults(SharedModel teamModel)
        {
            var venueId = teamModel.HomeVenue.Value | teamModel.AwayVenue.Value |
                          teamModel.NeutralVenue.Value;
            var sortBy = (int) teamModel.SortOrder;
            var sortDirection = teamModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (teamModel.TeamId.Value == 0 && teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetMatchResults(
                    teamModel.MatchType.Value, teamModel.Limit.Value,
                    teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            if (teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetMatchResultsForTeam(
                    teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.Limit.Value, teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy,
                    sortDirection);

            if (teamModel.TeamId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetMatchResultsAgainstOpponents(
                    teamModel.MatchType.Value, teamModel.OpponentsId.Value, teamModel.Limit.Value,
                    teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId,
                    teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season, teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            return await _unitOfWork.MatchRecordDetailsRepository.GetMatchResultsForTeamAgainstOpponents(
                teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.OpponentsId.Value, teamModel.Limit.Value,
                teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                venueId,
                teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season, teamModel.MatchResult.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<MatchDate>, Error>> GetDatesForMatchType(string matchType)
        {
            return await _unitOfWork.MatchesRepository.GetTeamsForMatchType(matchType);
        }

        public async Task<Result<IReadOnlyList<string>, Error>> GetSeriesDatesForMatchType(string matchType)
        {
            return await _unitOfWork.MatchesRepository.GetSeriesDatesForMatchType(matchType);
        }
    }
}