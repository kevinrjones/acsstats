using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;


namespace Repository
{
    public interface IPartnershipRecordDetailsRepository : IReadOnlyRepository<PartnershipIndividualRecordDetails>
    {
        Task<Result<IReadOnlyList<PartnershipIndividualRecordDetails>, Error>> GetCompletePartnershipIndividualMatches(
            int teamId,
            int opponentsId, string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipIndividualRecordDetails>, Error>> GetCompletePartnershipIndividualInnings(
            int teamId,
            int opponentsId, string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipCareerRecordsAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetCompletePartnershipIndividualSeries(int teamId,
            int opponentsId, string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipCareerRecordsForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetCompletePartnershipCareerRecords(
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipCareerRecordsForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipGroundsRecordsForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipGroundsRecordsForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipGroundsRecordsAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);


        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetCompletePartnershipGroundsRecords(
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipHostRecordsForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipHostRecordsForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipHostRecordsAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);


        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetCompletePartnershipHostRecords(
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipOpponentsRecordsForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipOpponentsRecordsForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipOpponentsRecordsAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);


        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetCompletePartnershipOpponentsRecords(
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipSeasonRecordsForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipSeasonRecordsForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipSeasonRecordsAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);


        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetCompletePartnershipSeasonRecords(
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipYearRecordsForTeam(int teamId,
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipYearRecordsForTeamAgainstTeam(
            int teamId
            , int opponentsId, string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipYearRecordsAgainstTeam(
            int opponentsId,
            string matchType, int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);


        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetCompletePartnershipYearRecords(
            string matchType,
            int groundId, int hostCountryId, int venueId
            , long startDate, long endDate, string season
            , int matchResult, int runLimit
            , int sortBy, string sortDirection);
    }
}