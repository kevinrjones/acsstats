using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services
{
    public interface IPartnershipService
    {
        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipCareerRecords(
            PartnershipModel sharedServiceFieldingModel);

        Task<Result<IReadOnlyList<PartnershipIndividualRecordDetails>, Error>> GetPartnershipIndividualInnings(PartnershipModel fieldingModel);
        Task<Result<IReadOnlyList<PartnershipIndividualRecordDetails>, Error>> GetPartnershipIndividualMatches(PartnershipModel fieldingModel);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipIndividualSeries(
            PartnershipModel fieldingModel);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipIndividualGrounds(
            PartnershipModel fieldingModel);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>,Error>> GetPartnershipIndividualHost(
            PartnershipModel fieldingModel);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>,Error>> GetPartnershipIndividualOpponents(
            PartnershipModel fieldingModel);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>,Error>> GetPartnershipIndividualSeason(
            PartnershipModel fieldingModel);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>,Error>> GetPartnershipIndividualYear(
            PartnershipModel fieldingModel);
    }
}