using System.Collections.Generic;
using System.Threading.Tasks;
using AcsRepository;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services.AcsServices
{
    public class PartnershipService : IPartnershipService
    {
        private readonly IEfUnitOfWork _unitOfWork;

        public PartnershipService(IEfUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipCareerRecords(
            PartnershipModel partnershipModel)
        {
            var venueId = partnershipModel.HomeVenue.Value | partnershipModel.AwayVenue.Value |
                          partnershipModel.NeutralVenue.Value;

            var sortBy = (int) partnershipModel.SortOrder;
            var sortDirection = partnershipModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (partnershipModel.TeamId.Value != 0 && partnershipModel.OpponentsId.Value != 0)
            {
                return await _unitOfWork.PartnershipDetailsRepository
                    .GetPartnershipCareerRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value, partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }
            else
            {
                if (partnershipModel.TeamId.Value != 0)
                {
                    if (!partnershipModel.TeamGrouping)
                    {
                        return
                            await _unitOfWork.PartnershipDetailsRepository.GetPartnershipCareerRecordsForTeam(
                                partnershipModel.TeamId.Value, partnershipModel.MatchType.Value,
                                partnershipModel.GroundId.Value, partnershipModel.HostCountryId.Value, venueId,
                                partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                                partnershipModel.MatchResult.Value,
                                partnershipModel.Limit.Value,
                                sortBy, sortDirection);
                    }

                    return await _unitOfWork.PartnershipDetailsRepository
                        .GetPartnershipCareerRecordsForTeamAgainstTeam(
                            partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                            partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                            partnershipModel.HostCountryId.Value, venueId,
                            partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                            partnershipModel.MatchResult.Value, partnershipModel.Limit.Value,
                            sortBy, sortDirection);
                }
                if (partnershipModel.OpponentsId.Value != 0)
                {
                    if (!partnershipModel.TeamGrouping)
                    {
                        return await _unitOfWork.PartnershipDetailsRepository
                            .GetPartnershipCareerRecordsAgainstTeam(
                                partnershipModel.OpponentsId.Value, partnershipModel.MatchType.Value,
                                partnershipModel.GroundId.Value, partnershipModel.HostCountryId.Value,
                                venueId,
                                partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                                partnershipModel.MatchResult.Value,
                                partnershipModel.Limit.Value,
                                sortBy, sortDirection);
                    }

                    return await _unitOfWork.PartnershipDetailsRepository
                        .GetPartnershipCareerRecordsForTeamAgainstTeam(
                            partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                            partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                            partnershipModel.HostCountryId.Value, venueId,
                            partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                            partnershipModel.MatchResult.Value, partnershipModel.Limit.Value,
                            sortBy, sortDirection);
                }
                return
                    await _unitOfWork.PartnershipDetailsRepository.GetCompletePartnershipCareerRecords(
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId, partnershipModel.StartDateEpoch,
                        partnershipModel.EndDateEpoch, partnershipModel.Season, partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }
        }


        public async Task<Result<IReadOnlyList<PartnershipIndividualRecordDetails>, Error>> GetPartnershipIndividualInnings(
            PartnershipModel partnershipModel)
        {
            var venueId = partnershipModel.HomeVenue.Value | partnershipModel.AwayVenue.Value |
                          partnershipModel.NeutralVenue.Value;

            var sortBy = (int) partnershipModel.SortOrder;
            var sortDirection = partnershipModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            return await _unitOfWork.PartnershipDetailsRepository.GetCompletePartnershipIndividualInnings(
                partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                partnershipModel.HostCountryId.Value, venueId, partnershipModel.StartDateEpoch,
                partnershipModel.EndDateEpoch,
                partnershipModel.Season,
                partnershipModel.MatchResult.Value, partnershipModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PartnershipIndividualRecordDetails>, Error>> GetPartnershipIndividualMatches(
            PartnershipModel partnershipModel)
        {
            var venueId = partnershipModel.HomeVenue.Value | partnershipModel.AwayVenue.Value |
                          partnershipModel.NeutralVenue.Value;

            var sortBy = (int) partnershipModel.SortOrder;
            var sortDirection = partnershipModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            return await _unitOfWork.PartnershipDetailsRepository.GetCompletePartnershipIndividualMatches(
                partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                partnershipModel.HostCountryId.Value, venueId, partnershipModel.StartDateEpoch,
                partnershipModel.EndDateEpoch,
                partnershipModel.Season,
                partnershipModel.MatchResult.Value, partnershipModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipIndividualSeries(
            PartnershipModel partnershipModel)
        {
            var venueId = partnershipModel.HomeVenue.Value | partnershipModel.AwayVenue.Value |
                          partnershipModel.NeutralVenue.Value;

            var sortBy = (int) partnershipModel.SortOrder;
            var sortDirection = partnershipModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            return await _unitOfWork.PartnershipDetailsRepository.GetCompletePartnershipIndividualSeries(
                partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                partnershipModel.HostCountryId.Value, venueId, partnershipModel.StartDateEpoch,
                partnershipModel.EndDateEpoch,
                partnershipModel.Season,
                partnershipModel.MatchResult.Value, partnershipModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipIndividualGrounds(
            PartnershipModel partnershipModel)
        {
            var venueId = partnershipModel.HomeVenue.Value | partnershipModel.AwayVenue.Value |
                          partnershipModel.NeutralVenue.Value;

            var sortBy = (int) partnershipModel.SortOrder;
            var sortDirection = partnershipModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (partnershipModel.TeamId.Value != 0 && partnershipModel.OpponentsId.Value != 0)
            {
                return await
                    _unitOfWork.PartnershipDetailsRepository.GetPartnershipGroundsRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (partnershipModel.TeamId.Value != 0)
            {
                if (!partnershipModel.TeamGrouping)
                {
                    return
                        await _unitOfWork.PartnershipDetailsRepository.GetPartnershipGroundsRecordsForTeam(
                            partnershipModel.TeamId.Value, partnershipModel.MatchType.Value,
                            partnershipModel.GroundId.Value, partnershipModel.HostCountryId.Value, venueId,
                            partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                            partnershipModel.MatchResult.Value,
                            partnershipModel.Limit.Value,
                            sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PartnershipDetailsRepository.GetPartnershipGroundsRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (partnershipModel.OpponentsId.Value != 0)
            {
                if (!partnershipModel.TeamGrouping)
                {
                    return await _unitOfWork.PartnershipDetailsRepository
                        .GetPartnershipGroundsRecordsAgainstTeam(
                            partnershipModel.OpponentsId.Value, partnershipModel.MatchType.Value,
                            partnershipModel.GroundId.Value, partnershipModel.HostCountryId.Value,
                            venueId,
                            partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                            partnershipModel.MatchResult.Value,
                            partnershipModel.Limit.Value,
                            sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PartnershipDetailsRepository.GetPartnershipGroundsRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }

            return
                await _unitOfWork.PartnershipDetailsRepository.GetCompletePartnershipGroundsRecords(
                    partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                    partnershipModel.HostCountryId.Value, venueId, partnershipModel.StartDateEpoch,
                    partnershipModel.EndDateEpoch, partnershipModel.Season, partnershipModel.MatchResult.Value,
                    partnershipModel.Limit.Value,
                    sortBy, sortDirection);

        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipIndividualHost(
            PartnershipModel partnershipModel)
        {
            var venueId = partnershipModel.HomeVenue.Value | partnershipModel.AwayVenue.Value |
                          partnershipModel.NeutralVenue.Value;

            var sortBy = (int) partnershipModel.SortOrder;
            var sortDirection = partnershipModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";


            if (partnershipModel.TeamId.Value != 0 && partnershipModel.OpponentsId.Value != 0)
            {
                return await
                    _unitOfWork.PartnershipDetailsRepository.GetPartnershipHostRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (partnershipModel.TeamId.Value != 0)
            {
                if (!partnershipModel.TeamGrouping)
                {
                    return
                        await _unitOfWork.PartnershipDetailsRepository.GetPartnershipHostRecordsForTeam(
                            partnershipModel.TeamId.Value, partnershipModel.MatchType.Value,
                            partnershipModel.GroundId.Value, partnershipModel.HostCountryId.Value, venueId,
                            partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                            partnershipModel.MatchResult.Value,
                            partnershipModel.Limit.Value,
                            sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PartnershipDetailsRepository.GetPartnershipHostRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (partnershipModel.OpponentsId.Value != 0)
            {
                if (!partnershipModel.TeamGrouping)
                {
                    return
                        await _unitOfWork.PartnershipDetailsRepository.GetPartnershipHostRecordsAgainstTeam(
                            partnershipModel.OpponentsId.Value, partnershipModel.MatchType.Value,
                            partnershipModel.GroundId.Value, partnershipModel.HostCountryId.Value,
                            venueId,
                            partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                            partnershipModel.MatchResult.Value,
                            partnershipModel.Limit.Value,
                            sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PartnershipDetailsRepository.GetPartnershipHostRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }

            return
                await _unitOfWork.PartnershipDetailsRepository.GetCompletePartnershipHostRecords(
                    partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                    partnershipModel.HostCountryId.Value, venueId, partnershipModel.StartDateEpoch,
                    partnershipModel.EndDateEpoch, partnershipModel.Season, partnershipModel.MatchResult.Value,
                    partnershipModel.Limit.Value,
                    sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipIndividualOpponents(
            PartnershipModel partnershipModel)
        {
            var venueId = partnershipModel.HomeVenue.Value | partnershipModel.AwayVenue.Value |
                          partnershipModel.NeutralVenue.Value;

            var sortBy = (int) partnershipModel.SortOrder;
            var sortDirection = partnershipModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (partnershipModel.TeamId.Value != 0 && partnershipModel.OpponentsId.Value != 0)
            {
                return await
                    _unitOfWork.PartnershipDetailsRepository.GetPartnershipOpponentsRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (partnershipModel.TeamId.Value != 0)
            {
                if (!partnershipModel.TeamGrouping)
                {
                    return await _unitOfWork.PartnershipDetailsRepository
                        .GetPartnershipOpponentsRecordsForTeam(
                            partnershipModel.TeamId.Value, partnershipModel.MatchType.Value,
                            partnershipModel.GroundId.Value, partnershipModel.HostCountryId.Value, venueId,
                            partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                            partnershipModel.MatchResult.Value,
                            partnershipModel.Limit.Value,
                            sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PartnershipDetailsRepository.GetPartnershipOpponentsRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (partnershipModel.OpponentsId.Value != 0)
            {
                if (!partnershipModel.TeamGrouping)
                {
                    return await _unitOfWork.PartnershipDetailsRepository
                        .GetPartnershipOpponentsRecordsAgainstTeam(
                            partnershipModel.OpponentsId.Value, partnershipModel.MatchType.Value,
                            partnershipModel.GroundId.Value, partnershipModel.HostCountryId.Value,
                            venueId,
                            partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                            partnershipModel.MatchResult.Value,
                            partnershipModel.Limit.Value,
                            sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PartnershipDetailsRepository.GetPartnershipOpponentsRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PartnershipDetailsRepository.GetCompletePartnershipOpponentsRecords(
                partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                partnershipModel.HostCountryId.Value, venueId, partnershipModel.StartDateEpoch,
                partnershipModel.EndDateEpoch, partnershipModel.Season, partnershipModel.MatchResult.Value,
                partnershipModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipIndividualSeason(
            PartnershipModel partnershipModel)
        {
            var venueId = partnershipModel.HomeVenue.Value | partnershipModel.AwayVenue.Value |
                          partnershipModel.NeutralVenue.Value;

            var sortBy = (int) partnershipModel.SortOrder;
            var sortDirection = partnershipModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (partnershipModel.TeamId.Value != 0 && partnershipModel.OpponentsId.Value != 0)
            {
                return await
                    _unitOfWork.PartnershipDetailsRepository.GetPartnershipSeasonRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (partnershipModel.TeamId.Value != 0)
            {
                if (!partnershipModel.TeamGrouping)
                {
                    return
                        await _unitOfWork.PartnershipDetailsRepository.GetPartnershipSeasonRecordsForTeam(
                            partnershipModel.TeamId.Value, partnershipModel.MatchType.Value,
                            partnershipModel.GroundId.Value, partnershipModel.HostCountryId.Value, venueId,
                            partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                            partnershipModel.MatchResult.Value,
                            partnershipModel.Limit.Value,
                            sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PartnershipDetailsRepository.GetPartnershipSeasonRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (partnershipModel.OpponentsId.Value != 0)
            {
                if (!partnershipModel.TeamGrouping)
                {
                    return await _unitOfWork.PartnershipDetailsRepository
                        .GetPartnershipSeasonRecordsAgainstTeam(
                            partnershipModel.OpponentsId.Value, partnershipModel.MatchType.Value,
                            partnershipModel.GroundId.Value, partnershipModel.HostCountryId.Value,
                            venueId,
                            partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                            partnershipModel.MatchResult.Value,
                            partnershipModel.Limit.Value,
                            sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PartnershipDetailsRepository.GetPartnershipSeasonRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }

            return
                await _unitOfWork.PartnershipDetailsRepository.GetCompletePartnershipSeasonRecords(
                    partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                    partnershipModel.HostCountryId.Value, venueId, partnershipModel.StartDateEpoch,
                    partnershipModel.EndDateEpoch, partnershipModel.Season, partnershipModel.MatchResult.Value,
                    partnershipModel.Limit.Value,
                    sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipIndividualYear(
            PartnershipModel partnershipModel)
        {
            var venueId = partnershipModel.HomeVenue.Value | partnershipModel.AwayVenue.Value |
                          partnershipModel.NeutralVenue.Value;

            var sortBy = (int) partnershipModel.SortOrder;
            var sortDirection = partnershipModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (partnershipModel.TeamId.Value != 0 && partnershipModel.OpponentsId.Value != 0)
            {
                return await
                    _unitOfWork.PartnershipDetailsRepository.GetPartnershipYearRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (partnershipModel.TeamId.Value != 0)
            {
                if (!partnershipModel.TeamGrouping)
                {
                    return
                        await _unitOfWork.PartnershipDetailsRepository.GetPartnershipYearRecordsForTeam(
                            partnershipModel.TeamId.Value, partnershipModel.MatchType.Value,
                            partnershipModel.GroundId.Value, partnershipModel.HostCountryId.Value, venueId,
                            partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                            partnershipModel.MatchResult.Value,
                            partnershipModel.Limit.Value,
                            sortBy, sortDirection);
                }
                else
                {
                    return await
                        _unitOfWork.PartnershipDetailsRepository.GetPartnershipYearRecordsForTeamAgainstTeam(
                            partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                            partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                            partnershipModel.HostCountryId.Value, venueId,
                            partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                            partnershipModel.MatchResult.Value,
                            partnershipModel.Limit.Value,
                            sortBy, sortDirection);
                }
            }

            if (partnershipModel.OpponentsId.Value != 0)
            {
                if (!partnershipModel.TeamGrouping)
                {
                    return
                        await _unitOfWork.PartnershipDetailsRepository.GetPartnershipYearRecordsAgainstTeam(
                            partnershipModel.OpponentsId.Value, partnershipModel.MatchType.Value,
                            partnershipModel.GroundId.Value, partnershipModel.HostCountryId.Value,
                            venueId,
                            partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                            partnershipModel.MatchResult.Value,
                            partnershipModel.Limit.Value,
                            sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PartnershipDetailsRepository.GetPartnershipYearRecordsForTeamAgainstTeam(
                        partnershipModel.TeamId.Value, partnershipModel.OpponentsId.Value,
                        partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                        partnershipModel.HostCountryId.Value, venueId,
                        partnershipModel.StartDateEpoch, partnershipModel.EndDateEpoch, partnershipModel.Season,
                        partnershipModel.MatchResult.Value,
                        partnershipModel.Limit.Value,
                        sortBy, sortDirection);
            }

            return
                await _unitOfWork.PartnershipDetailsRepository.GetCompletePartnershipYearRecords(
                    partnershipModel.MatchType.Value, partnershipModel.GroundId.Value,
                    partnershipModel.HostCountryId.Value, venueId, partnershipModel.StartDateEpoch,
                    partnershipModel.EndDateEpoch, partnershipModel.Season, partnershipModel.MatchResult.Value,
                    partnershipModel.Limit.Value,
                    sortBy, sortDirection);
        }
    }
}