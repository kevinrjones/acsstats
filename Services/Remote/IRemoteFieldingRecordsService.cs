using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Models;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services.Remote;

public interface IRemoteFieldingRecordsService
{
    Task<Result<List<PlayerFieldingCareerRecordDetails>, Error>> GetOverall(SharedModel sharedServiceModel);
    Task<Result<List<IndividualFieldingDetails>, Error>> GetInningsByInnings(SharedModel sharedServiceModel);
    Task<Result<List<IndividualFieldingDetails>, Error>> GetMatchDetails(SharedModel sharedServiceModel);
    Task<Result<List<PlayerFieldingCareerRecordDetails>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel);
    Task<Result<List<PlayerFieldingCareerRecordDetails>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel);
    Task<Result<List<PlayerFieldingCareerRecordDetails>, Error>> GetRecordsForHost(SharedModel sharedServiceModel);
    Task<Result<List<PlayerFieldingCareerRecordDetails>, Error>> GetRecordsForOpponents(SharedModel sharedServiceModel);
    Task<Result<List<PlayerFieldingCareerRecordDetails>, Error>> GetRecordsByYear(SharedModel sharedServiceModel);
    Task<Result<List<PlayerFieldingCareerRecordDetails>, Error>> GetRecordsBySeason(SharedModel sharedServiceModel);
}