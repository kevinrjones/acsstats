using System.Collections.Generic;
using System.Threading.Tasks;
using AcsRepository;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Models;

namespace Services.AcsServices
{
    public class TeamsService : ITeamsService
    {
        private readonly IEfUnitOfWork _unitOfWork;
        private readonly ILogger<TeamsService> _logger;

        public TeamsService(IEfUnitOfWork unitOfWork, ILogger<TeamsService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<Team, Error>> GetTeam(TeamId teamIdValue)
        {
            if (teamIdValue == 0)
                return await Task.FromResult(Result.Success<Team, Error>(new Team {Name = "All"}));

            return await _unitOfWork.TeamsRepository.Entities.FirstAsync(t => t.Id == teamIdValue);
        }

        public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetTeamRecords(
            SharedModel teamModel)
        {
            var venueId = teamModel.HomeVenue.Value | teamModel.AwayVenue.Value |
                          teamModel.NeutralVenue.Value;
            var sortBy = (int) teamModel.SortOrder;
            var sortDirection = teamModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (teamModel.TeamId.Value == 0 && teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetOverall(teamModel.MatchType.Value,
                    teamModel.Limit.Value, teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            if (teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetOverallForTeam(teamModel.MatchType.Value,
                    teamModel.TeamId.Value, teamModel.Limit.Value, teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy,
                    sortDirection);

            if (teamModel.TeamId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetOverallAgainstOpponents(
                    teamModel.MatchType.Value, teamModel.OpponentsId.Value, teamModel.Limit.Value,
                    teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId,
                    teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            return await _unitOfWork.MatchRecordDetailsRepository.GetOverallForTeamAgainstOpponents(
                teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.OpponentsId.Value, teamModel.Limit.Value,
                teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                venueId,
                teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                teamModel.MatchResult.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetTeamSeriesRecords(
            SharedModel teamModel)
        {
            var venueId = teamModel.HomeVenue.Value | teamModel.AwayVenue.Value |
                          teamModel.NeutralVenue.Value;
            var sortBy = (int) teamModel.SortOrder;
            var sortDirection = teamModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (teamModel.TeamId.Value == 0 && teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetSeriesOverall(teamModel.MatchType.Value,
                    teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            if (teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetSeriesOverallForTeam(teamModel.MatchType.Value,
                    teamModel.TeamId.Value, teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy,
                    sortDirection);

            if (teamModel.TeamId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetSeriesOverallAgainstOpponents(
                    teamModel.MatchType.Value, teamModel.OpponentsId.Value,
                    teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId,
                    teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            return await _unitOfWork.MatchRecordDetailsRepository.GetSeriesOverallForTeamAgainstOpponents(
                teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.OpponentsId.Value,
                teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                venueId,
                teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                teamModel.MatchResult.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetTeamGroundRecords(
            SharedModel teamModel)
        {
            var venueId = teamModel.HomeVenue.Value | teamModel.AwayVenue.Value |
                          teamModel.NeutralVenue.Value;
            var sortBy = (int) teamModel.SortOrder;
            var sortDirection = teamModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (teamModel.TeamId.Value == 0 && teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetGroundOverall(teamModel.MatchType.Value,
                    teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            if (teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetGroundOverallForTeam(teamModel.MatchType.Value,
                    teamModel.TeamId.Value, teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy,
                    sortDirection);

            if (teamModel.TeamId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetGroundOverallAgainstOpponents(
                    teamModel.MatchType.Value, teamModel.OpponentsId.Value,
                    teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId,
                    teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            return await _unitOfWork.MatchRecordDetailsRepository.GetGroundOverallForTeamAgainstOpponents(
                teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.OpponentsId.Value,
                teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                venueId,
                teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                teamModel.MatchResult.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetTeamHostCountryRecords(
            SharedModel teamModel)
        {
            var venueId = teamModel.HomeVenue.Value | teamModel.AwayVenue.Value |
                          teamModel.NeutralVenue.Value;
            var sortBy = (int) teamModel.SortOrder;
            var sortDirection = teamModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (teamModel.TeamId.Value == 0 && teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetHostCountryOverall(teamModel.MatchType.Value,
                    teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            if (teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetHostCountryOverallForTeam(
                    teamModel.MatchType.Value,
                    teamModel.TeamId.Value, teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy,
                    sortDirection);

            if (teamModel.TeamId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetHostCountryOverallAgainstOpponents(
                    teamModel.MatchType.Value, teamModel.OpponentsId.Value,
                    teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId,
                    teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            return await _unitOfWork.MatchRecordDetailsRepository.GetHostCountryOverallForTeamAgainstOpponents(
                teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.OpponentsId.Value,
                teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                venueId,
                teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                teamModel.MatchResult.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetTeamOppositionRecords(
            SharedModel teamModel)
        {
            var venueId = teamModel.HomeVenue.Value | teamModel.AwayVenue.Value |
                          teamModel.NeutralVenue.Value;
            var sortBy = (int) teamModel.SortOrder;
            var sortDirection = teamModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (teamModel.TeamId.Value == 0 && teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetOppositionOverall(teamModel.MatchType.Value,
                    teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            if (teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetOppositionOverallForTeam(
                    teamModel.MatchType.Value,
                    teamModel.TeamId.Value, teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy,
                    sortDirection);

            if (teamModel.TeamId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetOppositionOverallAgainstOpponents(
                    teamModel.MatchType.Value, teamModel.OpponentsId.Value,
                    teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId,
                    teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            return await _unitOfWork.MatchRecordDetailsRepository.GetOppositionOverallForTeamAgainstOpponents(
                teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.OpponentsId.Value,
                teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                venueId,
                teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                teamModel.MatchResult.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetTeamByYearRecords(
            SharedModel teamModel)
        {
            var venueId = teamModel.HomeVenue.Value | teamModel.AwayVenue.Value |
                          teamModel.NeutralVenue.Value;
            var sortBy = (int) teamModel.SortOrder;
            var sortDirection = teamModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (teamModel.TeamId.Value == 0 && teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetByYearOverall(teamModel.MatchType.Value,
                    teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            if (teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetByYearOverallForTeam(
                    teamModel.MatchType.Value,
                    teamModel.TeamId.Value, teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy,
                    sortDirection);

            if (teamModel.TeamId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetByYearOverallAgainstOpponents(
                    teamModel.MatchType.Value, teamModel.OpponentsId.Value,
                    teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId,
                    teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            return await _unitOfWork.MatchRecordDetailsRepository.GetByYearOverallForTeamAgainstOpponents(
                teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.OpponentsId.Value,
                teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                venueId,
                teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                teamModel.MatchResult.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetTeamBySeasonRecords(
            SharedModel teamModel)
        {
            var venueId = teamModel.HomeVenue.Value | teamModel.AwayVenue.Value |
                          teamModel.NeutralVenue.Value;
            var sortBy = (int) teamModel.SortOrder;
            var sortDirection = teamModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (teamModel.TeamId.Value == 0 && teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetBySeasonOverall(teamModel.MatchType.Value,
                    teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            if (teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetBySeasonOverallForTeam(
                    teamModel.MatchType.Value,
                    teamModel.TeamId.Value, teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy,
                    sortDirection);

            if (teamModel.TeamId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetBySeasonOverallAgainstOpponents(
                    teamModel.MatchType.Value, teamModel.OpponentsId.Value,
                    teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId,
                    teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            return await _unitOfWork.MatchRecordDetailsRepository.GetBySeasonOverallForTeamAgainstOpponents(
                teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.OpponentsId.Value,
                teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                venueId,
                teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                teamModel.MatchResult.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<TeamExtrasDetails>, Error>> GetTeamOverallExtrasRecords(
            SharedModel teamModel)
        {
            var venueId = teamModel.HomeVenue.Value | teamModel.AwayVenue.Value |
                          teamModel.NeutralVenue.Value;
            var sortBy = (int) teamModel.SortOrder;
            var sortDirection = teamModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (teamModel.TeamId.Value == 0 && teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetOverallExtrasOverall(teamModel.MatchType.Value,
                    teamModel.Limit.Value, teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            if (teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetOverallExtrasOverallForTeam(
                    teamModel.MatchType.Value,
                    teamModel.TeamId.Value, teamModel.Limit.Value, teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy,
                    sortDirection);

            if (teamModel.TeamId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetOverallExtrasOverallAgainstOpponents(
                    teamModel.MatchType.Value, teamModel.OpponentsId.Value, teamModel.Limit.Value,
                    teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId,
                    teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            return await _unitOfWork.MatchRecordDetailsRepository.GetOverallExtrasOverallForTeamAgainstOpponents(
                teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.OpponentsId.Value, teamModel.Limit.Value,
                teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                venueId,
                teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                teamModel.MatchResult.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<InningsExtrasDetails>, Error>> GetTeamInningsExtrasRecords(
            SharedModel teamModel)
        {
            var venueId = teamModel.HomeVenue.Value | teamModel.AwayVenue.Value |
                          teamModel.NeutralVenue.Value;
            var sortBy = (int) teamModel.SortOrder;
            var sortDirection = teamModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (teamModel.TeamId.Value == 0 && teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetInningsExtrasOverall(teamModel.MatchType.Value,
                    teamModel.Limit.Value, teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            if (teamModel.OpponentsId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetInningsExtrasOverallForTeam(
                    teamModel.MatchType.Value,
                    teamModel.TeamId.Value, teamModel.Limit.Value, teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId, teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy,
                    sortDirection);

            if (teamModel.TeamId.Value == 0)
                return await _unitOfWork.MatchRecordDetailsRepository.GetInningsExtrasOverallAgainstOpponents(
                    teamModel.MatchType.Value, teamModel.OpponentsId.Value, teamModel.Limit.Value,
                    teamModel.GroundId.Value,
                    teamModel.HostCountryId.Value,
                    venueId,
                    teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                    teamModel.MatchResult.Value,
                    sortBy, sortDirection);

            return await _unitOfWork.MatchRecordDetailsRepository.GetInningsExtrasOverallForTeamAgainstOpponents(
                teamModel.MatchType.Value, teamModel.TeamId.Value, teamModel.OpponentsId.Value, teamModel.Limit.Value,
                teamModel.GroundId.Value, teamModel.HostCountryId.Value,
                venueId,
                teamModel.StartDateEpoch, teamModel.EndDateEpoch, teamModel.Season,
                teamModel.MatchResult.Value,
                sortBy, sortDirection);
        }
    }
}