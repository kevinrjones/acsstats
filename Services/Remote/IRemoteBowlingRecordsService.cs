using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services.Remote
{
    public interface IRemoteBowlingRecordsService
    {
        Task<Result<List<PlayerBowlingCareerRecordDetailsDto>, Error>> GetOverall(SharedModel sharedServiceModel);
        Task<Result<List<IndividualBowlingDetailsDto>, Error>> GetInningsByInnings(SharedModel sharedServiceModel);
        Task<Result<List<IndividualBowlingDetailsDto>, Error>> GetMatchDetails(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBowlingCareerRecordDetailsDto>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBowlingCareerRecordDetailsDto>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBowlingCareerRecordDetailsDto>, Error>> GetRecordsForHost(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBowlingCareerRecordDetailsDto>, Error>> GetRecordsForOpponents(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBowlingCareerRecordDetailsDto>, Error>> GetRecordsByYear(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBowlingCareerRecordDetailsDto>, Error>> GetRecordsBySeason(SharedModel sharedServiceModel);
    }
}