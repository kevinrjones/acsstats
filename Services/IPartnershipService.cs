using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services
{
    public interface IPartnershipService
    {
        Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>> GetPartnershipCareerRecords(
            PartnershipModel sharedServiceFieldingModel);

        Task<Result<IReadOnlyList<PartnershipIndividualRecordDetailsDto>, Error>> GetPartnershipIndividualInnings(PartnershipModel fieldingModel);
        Task<Result<IReadOnlyList<PartnershipIndividualRecordDetailsDto>, Error>> GetPartnershipIndividualMatches(PartnershipModel fieldingModel);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>> GetPartnershipIndividualSeries(
            PartnershipModel fieldingModel);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>> GetPartnershipIndividualGrounds(
            PartnershipModel fieldingModel);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>,Error>> GetPartnershipIndividualHost(
            PartnershipModel fieldingModel);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>,Error>> GetPartnershipIndividualOpponents(
            PartnershipModel fieldingModel);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>,Error>> GetPartnershipIndividualSeason(
            PartnershipModel fieldingModel);

        Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>,Error>> GetPartnershipIndividualYear(
            PartnershipModel fieldingModel);
    }
}