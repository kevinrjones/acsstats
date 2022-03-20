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
        Task<Result<List<BattingCareerRecordDto>, Error>> GetOverall(SharedModel sharedServiceModel);
        Task<Result<List<IndividualBattingDetailsDto>, Error>> GetInningsByInnings(SharedModel sharedServiceModel);
        Task<Result<List<IndividualBattingDetailsDto>, Error>> GetMatchDetails(SharedModel sharedServiceModel);
        Task<Result<List<BattingCareerRecordDto>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel);
        Task<Result<List<BattingCareerRecordDto>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel);
        Task<Result<List<BattingCareerRecordDto>, Error>> GetRecordsForHost(SharedModel sharedServiceModel);
        Task<Result<List<BattingCareerRecordDto>, Error>> GetRecordsForOpponents(SharedModel sharedServiceModel);
        Task<Result<List<BattingCareerRecordDto>, Error>> GetRecordsByYear(SharedModel sharedServiceModel);
        Task<Result<List<BattingCareerRecordDto>, Error>> GetRecordsBySeason(SharedModel sharedServiceModel);
    }
}