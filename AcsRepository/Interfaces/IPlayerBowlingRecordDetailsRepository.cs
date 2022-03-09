using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;

namespace AcsRepository.Interfaces
{
    public interface IPlayerBowlingRecordDetailsRepository : IReadOnlyRepository<PlayerBowlingCareerRecordDetails>
    {
        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetCompleteBowlingCareerRecords(
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingCareerRecordsForTeam(
            int teamId,
            string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingCareerRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingCareerRecordsForTeamAgainstTeam(
            int teamId, int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetCompleteBowlingIndividualSeries(int teamId,
            int opponentsId, string matchType, int groundId, int hostCountryId, int venueId, long startDate,
            long endDate, string season, int matchResult, int wicketsLimit, int sortBy,
            string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingGroundsRecordsForTeam(
            int teamId,
            string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingGroundsRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingGroundsRecordsForTeamAgainstTeam(
            int teamId, int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetCompleteBowlingGroundsRecords(
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingHostRecordsForTeam(
            int teamId,
            string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingHostRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingHostRecordsForTeamAgainstTeam(
            int teamId, int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetCompleteBowlingHostRecords(
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingOpponentsRecordsForTeam(
            int teamId,
            string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingOpponentsRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingOpponentsRecordsForTeamAgainstTeam(
            int teamId, int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetCompleteBowlingOpponentsRecords(
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingSeasonRecordsForTeam(
            int teamId,
            string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingSeasonRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingSeasonRecordsForTeamAgainstTeam(
            int teamId, int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetCompleteBowlingSeasonRecords(
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);


        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingYearRecordsForTeam(
            int teamId,
            string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingYearRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingYearRecordsForTeamAgainstTeam(
            int teamId, int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetCompleteBowlingYearRecords(
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection);
    }
}