using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services.Remote
{
    public interface IRemoteBattingRecordsService
    {
        Task<Result<List<PlayerBattingRecordDto>, Error>> GetOverall(SharedModel sharedServiceModel);
        Task<Result<List<IndividualBattingDetailsDto>, Error>> GetInningsByInnings(SharedModel sharedServiceModel);
        Task<Result<List<IndividualBattingDetailsDto>, Error>> GetMatchDetails(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBattingRecordDto>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBattingRecordDto>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBattingRecordDto>, Error>> GetRecordsForHost(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBattingRecordDto>, Error>> GetRecordsForOpponents(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBattingRecordDto>, Error>> GetRecordsByYear(SharedModel sharedServiceModel);
        Task<Result<List<PlayerBattingRecordDto>, Error>> GetRecordsBySeason(SharedModel sharedServiceModel);
    }
}