using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services.Remote
{
    public interface IRemoteBowlingRecordsService
    {
        Task<Result<List<PlayerBowlingCareerRecordDetails>, Error>> GetOverall(SharedModel sharedServiceModel);
        Task<Result<List<IndividualBowlingDetails>, Error>> GetInningsByInnings(SharedModel sharedServiceModel);
        Task<Result<List<IndividualBowlingDetails>, Error>> GetMatchDetails(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBowlingCareerRecordDetails>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBowlingCareerRecordDetails>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBowlingCareerRecordDetails>, Error>> GetRecordsForHost(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBowlingCareerRecordDetails>, Error>> GetRecordsForOpponents(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBowlingCareerRecordDetails>, Error>> GetRecordsByYear(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBowlingCareerRecordDetails>, Error>> GetRecordsBySeason(SharedModel sharedServiceModel);
    }
}