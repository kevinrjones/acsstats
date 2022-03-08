using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;

namespace Repository
{
    public interface IPlayerFieldingRecordDetailsRepository : IReadOnlyRepository<PlayerFieldingCareerRecordDetails>
    {
        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetCompleteFieldingCareerRecords(string matchType,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult,
            int dismissalsLimit, int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsBySeries(string matchType,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult,
            int dismissalsLimit, int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsBySeriesAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsBySeriesForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsBySeriesForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByGround(string matchType,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult,
            int dismissalsLimit, int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByGroundAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByGroundForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByGroundForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);
        
        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByHost(string matchType,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult,
            int dismissalsLimit, int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByHostAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByHostForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByHostForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);
        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByOpposition(string matchType,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult,
            int dismissalsLimit, int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByOppositionAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByOppositionForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByOppositionForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByYear(string matchType,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult,
            int dismissalsLimit, int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByYearAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByYearForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByYearForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsBySeason(string matchType,
            int groundId,
            int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult,
            int dismissalsLimit, int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsBySeasonAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsBySeasonForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsBySeasonForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

    }
}