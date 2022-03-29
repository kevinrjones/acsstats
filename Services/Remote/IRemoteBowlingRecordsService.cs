using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services.Remote
{
    public interface IRemoteBowlingRecordsService
    {
        Task<Result<List<BowlingCareerRecordDto>, Error>> GetOverall(SharedModel sharedServiceModel);
        Task<Result<List<IndividualBowlingDetailsDto>, Error>> GetInningsByInnings(SharedModel sharedServiceModel);
        Task<Result<List<IndividualBowlingDetailsDto>, Error>> GetMatchDetails(SharedModel sharedServiceModel);
        Task<Result<List<BowlingCareerRecordDto>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel);
        Task<Result<List<BowlingCareerRecordDto>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel);
        Task<Result<List<BowlingCareerRecordDto>, Error>> GetRecordsForHost(SharedModel sharedServiceModel);
        Task<Result<List<BowlingCareerRecordDto>, Error>> GetRecordsForOpponents(SharedModel sharedServiceModel);
        Task<Result<List<BowlingCareerRecordDto>, Error>> GetRecordsByYear(SharedModel sharedServiceModel);
        Task<Result<List<BowlingCareerRecordDto>, Error>> GetRecordsBySeason(SharedModel sharedServiceModel);
    }
}