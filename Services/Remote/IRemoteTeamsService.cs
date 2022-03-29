using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Models;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services.Remote
{
    public interface IRemoteTeamsService
    {
        Task<Result<List<TeamRecordDetails>, Error>> GetTeamRecords(SharedModel sharedServiceModel);
        Task<Result<List<MatchRecordDetails>, Error>> GetInningsByInnings(SharedModel sharedServiceModel);
        Task<Result<List<MatchRecordDetails>, Error>> GetHighestTotals(SharedModel sharedServiceModel);
        Task<Result<List<MatchResult>, Error>> GetMatchResults(SharedModel sharedServiceModel);
        Task<Result<List<TeamRecordDetails>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel);
        Task<Result<List<TeamRecordDetails>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel);
        Task<Result<List<TeamRecordDetails>, Error>> GetRecordsForHost(SharedModel sharedServiceModel);
        Task<Result<List<TeamRecordDetails>, Error>> GetRecordsForOpposition(SharedModel sharedServiceModel);
        Task<Result<List<TeamRecordDetails>, Error>> GetRecordsForYear(SharedModel sharedServiceModel);
        Task<Result<List<TeamRecordDetails>, Error>> GetRecordsForSeason(SharedModel sharedServiceModel);
        Task<Result<List<TeamExtrasDetails>, Error>> GetOverallExtrasForteam(SharedModel sharedServiceModel);
        Task<Result<List<InningsExtrasDetails>, Error>> GetInningsExtrasForteam(SharedModel sharedServiceModel);
    }
}