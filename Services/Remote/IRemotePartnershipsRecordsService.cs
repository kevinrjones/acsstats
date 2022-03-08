using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services.Remote
{
    public interface IRemotePartnershipsRecordsService
    {
        Task<Result<List<PartnershipCareerRecordDetails>, Error>> GetOverall(SharedModel sharedServiceModel);
        Task<Result<List<PartnershipIndividualRecordDetails>, Error>> GetInningsByInnings(SharedModel sharedServiceModel);
        Task<Result<List<PartnershipIndividualRecordDetails>, Error>> GetMatchDetails(SharedModel sharedServiceModel);
        Task<Result<List<PartnershipCareerRecordDetails>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel);
        Task<Result<List<PartnershipCareerRecordDetails>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel);
        Task<Result<List<PartnershipCareerRecordDetails>, Error>> GetRecordsForHost(SharedModel sharedServiceModel);
        Task<Result<List<PartnershipCareerRecordDetails>, Error>> GetRecordsForOpponents(SharedModel sharedServiceModel);
        Task<Result<List<PartnershipCareerRecordDetails>, Error>> GetRecordsByYear(SharedModel sharedServiceModel);
        Task<Result<List<PartnershipCareerRecordDetails>, Error>> GetRecordsBySeason(SharedModel sharedServiceModel);    }
}