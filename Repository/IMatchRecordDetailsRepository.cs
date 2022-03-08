using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;

namespace Repository
{
    public interface IMatchRecordDetailsRepository : IReadOnlyRepository<MatchRecordDetails>
    {
        Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetInningsByInnings(string matchType, int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetInningsByInningsForTeam(string matchType, int teamId,
            int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetInningsByInningsAgainstOpponents(string matchType,
            int opponentsId,
            int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetInningsByInningsForTeamAgainstOpponents(string matchType,
            int teamId,
            int opponentsId, int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetMatchTotalsHigherThan(string matchType, int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetMatchTotalsHigherThanForTeam(string matchType, int teamId,
            int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetMatchTotalsHigherThanAgainstOpponents(string matchType,
            int opponentsId,
            int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetMatchTotalsHigherThanForTeamAgainstOpponents(string matchType,
            int teamId,
            int opponentsId, int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<MatchResult>, Error>> GetMatchResults(string matchType, int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<MatchResult>, Error>> GetMatchResultsForTeam(string matchType, int teamId,
            int minimum,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<MatchResult>, Error>> GetMatchResultsAgainstOpponents(string matchType,
            int opponentsId,
            int minimum,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<MatchResult>, Error>> GetMatchResultsForTeamAgainstOpponents(string matchType,
            int teamId,
            int opponentsId, int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOverall(string matchType, int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOverallForTeam(string matchType, int teamId,
            int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOverallAgainstOpponents(string matchType,
            int opponentsId,
            int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOverallForTeamAgainstOpponents(string matchType,
            int teamId,
            int opponentsId, int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetSeriesOverall(string matchType, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetSeriesOverallForTeam(string matchType, int teamId,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetSeriesOverallAgainstOpponents(string matchType,
            int opponentsId,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetSeriesOverallForTeamAgainstOpponents(string matchType,
            int teamId,
            int opponentsId, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetGroundOverall(string matchType, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetGroundOverallForTeam(string matchType, int teamId,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetGroundOverallAgainstOpponents(string matchType,
            int opponentsId,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetGroundOverallForTeamAgainstOpponents(string matchType,
            int teamId,
            int opponentsId, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetHostCountryOverall(string matchType, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetHostCountryOverallForTeam(string matchType, int teamId,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetHostCountryOverallAgainstOpponents(string matchType,
            int opponentsId,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetHostCountryOverallForTeamAgainstOpponents(string matchType,
            int teamId,
            int opponentsId, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOppositionOverall(string matchType, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOppositionOverallForTeam(string matchType, int teamId,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOppositionOverallAgainstOpponents(string matchType,
            int opponentsId,
             int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOppositionOverallForTeamAgainstOpponents(string matchType,
            int teamId,
            int opponentsId, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetByYearOverall(string matchType, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetByYearOverallForTeam(string matchType, int teamId,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetByYearOverallAgainstOpponents(string matchType,
            int opponentsId,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetByYearOverallForTeamAgainstOpponents(string matchType,
            int teamId,
            int opponentsId, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        
        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetBySeasonOverall(string matchType, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetBySeasonOverallForTeam(string matchType, int teamId,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetBySeasonOverallAgainstOpponents(string matchType,
            int opponentsId,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetBySeasonOverallForTeamAgainstOpponents(string matchType,
            int teamId,
            int opponentsId, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);
        
        Task<Result<IReadOnlyList<TeamExtrasDetails>, Error>> GetOverallExtrasOverall(string matchType, int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamExtrasDetails>, Error>> GetOverallExtrasOverallForTeam(string matchType, int teamId,
            int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamExtrasDetails>, Error>> GetOverallExtrasOverallAgainstOpponents(string matchType,
            int opponentsId,
            int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<TeamExtrasDetails>, Error>> GetOverallExtrasOverallForTeamAgainstOpponents(string matchType,
            int teamId,
            int opponentsId, int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<InningsExtrasDetails>, Error>> GetInningsExtrasOverall(string matchType, int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<InningsExtrasDetails>, Error>> GetInningsExtrasOverallForTeam(string matchType, int teamId,
            int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<InningsExtrasDetails>, Error>> GetInningsExtrasOverallAgainstOpponents(string matchType,
            int opponentsId,
            int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<InningsExtrasDetails>, Error>> GetInningsExtrasOverallForTeamAgainstOpponents(string matchType,
            int teamId,
            int opponentsId, int minimum, int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult,
            int sortBy, string sortDirection);
    }
}