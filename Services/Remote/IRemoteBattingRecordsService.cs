using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services.Remote
{
    public interface IRemoteBattingRecordsService
    {
        Task<Result<List<PlayerBattingCareerRecordDetails>, Error>> GetOverall(SharedModel sharedServiceModel);
        Task<Result<List<IndividualBattingDetails>, Error>> GetInningsByInnings(SharedModel sharedServiceModel);
        Task<Result<List<IndividualBattingDetails>, Error>> GetMatchDetails(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBattingCareerRecordDetails>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBattingCareerRecordDetails>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBattingCareerRecordDetails>, Error>> GetRecordsForHost(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBattingCareerRecordDetails>, Error>> GetRecordsForOpponents(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBattingCareerRecordDetails>, Error>> GetRecordsByYear(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBattingCareerRecordDetails>, Error>> GetRecordsBySeason(SharedModel sharedServiceModel);
    }
}