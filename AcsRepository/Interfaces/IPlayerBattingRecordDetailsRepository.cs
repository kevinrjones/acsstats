using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;

namespace AcsRepository.Interfaces
{
    public interface IPlayerBattingRecordDetailsRepository : IReadOnlyRepository<PlayerBattingCareerRecordDetails>
    {
        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingCareerRecordsAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetCompleteBattingIndividualSeries(int teamId,
            int opponentsId, string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingCareerRecordsForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetCompleteBattingCareerRecords(
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingCareerRecordsForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingGroundsRecordsForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingGroundsRecordsForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingGroundsRecordsAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);


        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetCompleteBattingGroundsRecords(
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingHostRecordsForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingHostRecordsForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingHostRecordsAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);


        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetCompleteBattingHostRecords(
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingOpponentsRecordsForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingOpponentsRecordsForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingOpponentsRecordsAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);


        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetCompleteBattingOpponentsRecords(
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingSeasonRecordsForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingSeasonRecordsForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingSeasonRecordsAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);


        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetCompleteBattingSeasonRecords(
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingYearRecordsForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingYearRecordsForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingYearRecordsAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);


        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetCompleteBattingYearRecords(
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);
    }
}