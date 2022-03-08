using System.Collections.Generic;
using System.Threading.Tasks;
using AcsRepository;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services.AcsServices
{
    public class PlayersService : IPlayersService
    { 
        private readonly IEfUnitOfWork _unitOfWork;

        public PlayersService(IEfUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingCareerRecords(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (fieldingModel.TeamId.Value != 0 && fieldingModel.OpponentsId.Value != 0)
            {
                return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingCareerRecordsForTeamAgainstTeam(
                    fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                    fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                    fieldingModel.HostCountryId.Value, venueId,
                    fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                    fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                    sortBy, sortDirection);
            }

            if (fieldingModel.TeamId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingCareerRecordsForTeam(
                        fieldingModel.TeamId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingCareerRecordsForTeamAgainstTeam(
                    fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                    fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                    fieldingModel.HostCountryId.Value, venueId,
                    fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                    fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                    sortBy, sortDirection);
            }

            if (fieldingModel.OpponentsId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingCareerRecordsAgainstTeam(
                        fieldingModel.OpponentsId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value,
                        venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingCareerRecordsForTeamAgainstTeam(
                    fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                    fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                    fieldingModel.HostCountryId.Value, venueId,
                    fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                    fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                    sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetCompleteBattingCareerRecords(
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                fieldingModel.EndDateEpoch, fieldingModel.Season, fieldingModel.MatchResult.Value,
                fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<IndividualBattingDetails>, Error>> GetBattingIndividualInnings(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            return await _unitOfWork.IndividualBattingDetailsRepository.GetCompleteBattingIndividualInnings(
                fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch,
                fieldingModel.Season,
                fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<IndividualBattingDetails>, Error>> GetBattingIndividualMatches(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            return await _unitOfWork.IndividualBattingDetailsRepository.GetCompleteBattingIndividualMatches(
                fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch,
                fieldingModel.Season,
                fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingIndividualSeries(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetCompleteBattingIndividualSeries(
                fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch,
                fieldingModel.Season,
                fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingIndividualGrounds(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (fieldingModel.TeamId.Value != 0 && fieldingModel.OpponentsId.Value != 0)
            {
                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingGroundsRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.TeamId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingGroundsRecordsForTeam(
                        fieldingModel.TeamId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingGroundsRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.OpponentsId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingGroundsRecordsAgainstTeam(
                        fieldingModel.OpponentsId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value,
                        venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingGroundsRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetCompleteBattingGroundsRecords(
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                fieldingModel.EndDateEpoch, fieldingModel.Season, fieldingModel.MatchResult.Value,
                fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingCareerRecords(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (fieldingModel.TeamId.Value != 0 && fieldingModel.OpponentsId.Value != 0)
            {
                return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingCareerRecordsForTeamAgainstTeam(
                    fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                    fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                    fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                    fieldingModel.EndDateEpoch, fieldingModel.Season,
                    fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                    sortBy, sortDirection);
            }

            if (fieldingModel.TeamId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingCareerRecordsForTeam(
                        fieldingModel.TeamId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingCareerRecordsForTeamAgainstTeam(
                    fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                    fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                    fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                    fieldingModel.EndDateEpoch, fieldingModel.Season,
                    fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                    sortBy, sortDirection);
            }

            if (fieldingModel.OpponentsId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingCareerRecordsAgainstTeam(
                        fieldingModel.OpponentsId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value,
                        venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingCareerRecordsForTeamAgainstTeam(
                    fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                    fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                    fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                    fieldingModel.EndDateEpoch, fieldingModel.Season,
                    fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                    sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetCompleteBowlingCareerRecords(
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                fieldingModel.EndDateEpoch, fieldingModel.Season, fieldingModel.MatchResult.Value,
                fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }


        public async Task<Result<IReadOnlyList<IndividualBowlingDetails>, Error>> GetBowlingIndividualInnings(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            var data = await _unitOfWork.IndividualBowlingDetailsRepository.GetCompleteBowlingIndividualInnings(
                fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch,
                fieldingModel.Season,
                fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                sortBy, sortDirection);
            return data;
        }

        public async Task<Result<IReadOnlyList<IndividualBowlingDetails>, Error>> GetBowlingIndividualMatches(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            return await _unitOfWork.IndividualBowlingDetailsRepository.GetCompleteBowlingIndividualMatches(
                fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch,
                fieldingModel.Season,
                fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingIndividualSeries(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetCompleteBowlingIndividualSeries(
                fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch,
                fieldingModel.Season,
                fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingIndividualGrounds(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (fieldingModel.TeamId.Value != 0 && fieldingModel.OpponentsId.Value != 0)
            {
                return await
                    _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingGroundsRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                        fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.TeamId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingGroundsRecordsForTeam(
                        fieldingModel.TeamId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingGroundsRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                        fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.OpponentsId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingGroundsRecordsAgainstTeam(
                        fieldingModel.OpponentsId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value,
                        venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingGroundsRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                        fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetCompleteBowlingGroundsRecords(
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                fieldingModel.EndDateEpoch, fieldingModel.Season, fieldingModel.MatchResult.Value,
                fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingIndividualHost(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (fieldingModel.TeamId.Value != 0 && fieldingModel.OpponentsId.Value != 0)
            {
                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingHostRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.TeamId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingHostRecordsForTeam(
                        fieldingModel.TeamId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingHostRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.OpponentsId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingHostRecordsAgainstTeam(
                        fieldingModel.OpponentsId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value,
                        venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingHostRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetCompleteBattingHostRecords(
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                fieldingModel.EndDateEpoch, fieldingModel.Season, fieldingModel.MatchResult.Value,
                fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingIndividualHost(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (fieldingModel.TeamId.Value != 0 && fieldingModel.OpponentsId.Value != 0)
            {
                return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingHostRecordsForTeamAgainstTeam(
                    fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                    fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                    fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                    fieldingModel.EndDateEpoch, fieldingModel.Season,
                    fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                    sortBy, sortDirection);
            }

            if (fieldingModel.TeamId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingHostRecordsForTeam(
                        fieldingModel.TeamId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingHostRecordsForTeamAgainstTeam(
                    fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                    fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                    fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                    fieldingModel.EndDateEpoch, fieldingModel.Season,
                    fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                    sortBy, sortDirection);
            }

            if (fieldingModel.OpponentsId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingHostRecordsAgainstTeam(
                        fieldingModel.OpponentsId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value,
                        venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingHostRecordsForTeamAgainstTeam(
                    fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                    fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                    fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                    fieldingModel.EndDateEpoch, fieldingModel.Season,
                    fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                    sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetCompleteBowlingHostRecords(
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                fieldingModel.EndDateEpoch, fieldingModel.Season, fieldingModel.MatchResult.Value,
                fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingIndividualOpponents(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (fieldingModel.TeamId.Value != 0 && fieldingModel.OpponentsId.Value != 0)
            {
                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingOpponentsRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.TeamId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingOpponentsRecordsForTeam(
                        fieldingModel.TeamId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingOpponentsRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.OpponentsId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingOpponentsRecordsAgainstTeam(
                        fieldingModel.OpponentsId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value,
                        venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingOpponentsRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetCompleteBattingOpponentsRecords(
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                fieldingModel.EndDateEpoch, fieldingModel.Season, fieldingModel.MatchResult.Value,
                fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingIndividualOpponents(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (fieldingModel.TeamId.Value != 0 && fieldingModel.OpponentsId.Value != 0)
            {
                return await _unitOfWork.PlayerBowlingRecordDetailsRepository
                    .GetBowlingOpponentsRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                        fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.TeamId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingOpponentsRecordsForTeam(
                        fieldingModel.TeamId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerBowlingRecordDetailsRepository
                    .GetBowlingOpponentsRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                        fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.OpponentsId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingOpponentsRecordsAgainstTeam(
                        fieldingModel.OpponentsId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value,
                        venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerBowlingRecordDetailsRepository
                    .GetBowlingOpponentsRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                        fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetCompleteBowlingOpponentsRecords(
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                fieldingModel.EndDateEpoch, fieldingModel.Season, fieldingModel.MatchResult.Value,
                fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingIndividualSeason(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (fieldingModel.TeamId.Value != 0 && fieldingModel.OpponentsId.Value != 0)
            {
                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingSeasonRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.TeamId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingSeasonRecordsForTeam(
                        fieldingModel.TeamId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingSeasonRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.OpponentsId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingSeasonRecordsAgainstTeam(
                        fieldingModel.OpponentsId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value,
                        venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingSeasonRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetCompleteBattingSeasonRecords(
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                fieldingModel.EndDateEpoch, fieldingModel.Season, fieldingModel.MatchResult.Value,
                fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingIndividualSeason(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (fieldingModel.TeamId.Value != 0 && fieldingModel.OpponentsId.Value != 0)
            {
                return await _unitOfWork.PlayerBowlingRecordDetailsRepository
                    .GetBowlingSeasonRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                        fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.TeamId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingSeasonRecordsForTeam(
                        fieldingModel.TeamId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerBowlingRecordDetailsRepository
                    .GetBowlingSeasonRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                        fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.OpponentsId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingSeasonRecordsAgainstTeam(
                        fieldingModel.OpponentsId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value,
                        venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerBowlingRecordDetailsRepository
                    .GetBowlingSeasonRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                        fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetCompleteBowlingSeasonRecords(
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                fieldingModel.EndDateEpoch, fieldingModel.Season, fieldingModel.MatchResult.Value,
                fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingIndividualYear(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (fieldingModel.TeamId.Value != 0 && fieldingModel.OpponentsId.Value != 0)
            {
                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingYearRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.TeamId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingYearRecordsForTeam(
                        fieldingModel.TeamId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingYearRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.OpponentsId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingYearRecordsAgainstTeam(
                        fieldingModel.OpponentsId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value,
                        venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await
                    _unitOfWork.PlayerBattingRecordDetailsRepository.GetBattingYearRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerBattingRecordDetailsRepository.GetCompleteBattingYearRecords(
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                fieldingModel.EndDateEpoch, fieldingModel.Season, fieldingModel.MatchResult.Value,
                fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingIndividualYear(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            if (fieldingModel.TeamId.Value != 0 && fieldingModel.OpponentsId.Value != 0)
            {
                return await _unitOfWork.PlayerBowlingRecordDetailsRepository
                    .GetBowlingYearRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                        fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.TeamId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingYearRecordsForTeam(
                        fieldingModel.TeamId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value, venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerBowlingRecordDetailsRepository
                    .GetBowlingYearRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                        fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            if (fieldingModel.OpponentsId.Value != 0)
            {
                if (!fieldingModel.TeamGrouping)
                {
                    return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetBowlingYearRecordsAgainstTeam(
                        fieldingModel.OpponentsId.Value, fieldingModel.MatchType.Value,
                        fieldingModel.GroundId.Value, fieldingModel.HostCountryId.Value,
                        venueId,
                        fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value,
                        fieldingModel.Limit.Value,
                        sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerBowlingRecordDetailsRepository
                    .GetBowlingYearRecordsForTeamAgainstTeam(
                        fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                        fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                        fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                        fieldingModel.EndDateEpoch, fieldingModel.Season,
                        fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerBowlingRecordDetailsRepository.GetCompleteBowlingYearRecords(
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch,
                fieldingModel.EndDateEpoch, fieldingModel.Season, fieldingModel.MatchResult.Value,
                fieldingModel.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecords(
            BattingBowlingFieldingModel model)
        {
            var venueId = model.HomeVenue.Value | model.AwayVenue.Value |
                          model.NeutralVenue.Value;

            var sortBy = (int) model.SortOrder;
            var sortDirection = model.SortDirection == SortDirection.Asc ? "ASC" : "DESC";


            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            if (model.TeamId.Value != 0)
            {
                if (!model.TeamGrouping)
                {
                    return await _unitOfWork.PlayerFieldingRecordDetailsRepository.GetFieldingCareerRecordsForTeam(
                        model.TeamId.Value, model.MatchType.Value,
                        model.GroundId.Value, model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            if (model.OpponentsId.Value != 0)
            {
                if (!model.TeamGrouping)
                {
                    return await _unitOfWork.PlayerFieldingRecordDetailsRepository.GetFieldingCareerRecordsAgainstTeam(
                        model.OpponentsId.Value, model.MatchType.Value,
                        model.GroundId.Value, model.HostCountryId.Value,
                        venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerFieldingRecordDetailsRepository.GetCompleteFieldingCareerRecords(
                model.MatchType.Value, model.GroundId.Value,
                model.HostCountryId.Value, venueId, model.StartDateEpoch,
                model.EndDateEpoch, model.Season, model.MatchResult.Value, model.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<IndividualFieldingDetails>, Error>> GetFieldingIndividualInnings(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            var data = await _unitOfWork.IndividualFieldingDetailsRepository.GetCompleteFieldingIndividualInnings(
                fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch,
                fieldingModel.Season,
                fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                sortBy, sortDirection);
            return data;
        }

        public async Task<Result<IReadOnlyList<IndividualFieldingDetails>, Error>> GetFieldingIndividualMatches(
            BattingBowlingFieldingModel fieldingModel)
        {
            var venueId = fieldingModel.HomeVenue.Value | fieldingModel.AwayVenue.Value |
                          fieldingModel.NeutralVenue.Value;

            var sortBy = (int) fieldingModel.SortOrder;
            var sortDirection = fieldingModel.SortDirection == SortDirection.Asc ? "ASC" : "DESC";

            var data = await _unitOfWork.IndividualFieldingDetailsRepository.GetCompleteFieldingIndividualMatches(
                fieldingModel.TeamId.Value, fieldingModel.OpponentsId.Value,
                fieldingModel.MatchType.Value, fieldingModel.GroundId.Value,
                fieldingModel.HostCountryId.Value, venueId, fieldingModel.StartDateEpoch, fieldingModel.EndDateEpoch,
                fieldingModel.Season,
                fieldingModel.MatchResult.Value, fieldingModel.Limit.Value,
                sortBy, sortDirection);
            return data;
        }

        public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsBySeries(
            BattingBowlingFieldingModel model)
        {
            var venueId = model.HomeVenue.Value | model.AwayVenue.Value |
                          model.NeutralVenue.Value;

            var sortBy = (int) model.SortOrder;
            var sortDirection = model.SortDirection == SortDirection.Asc ? "ASC" : "DESC";


            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsBySeriesForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            if (model.TeamId.Value != 0)
            {
                if (!model.TeamGrouping)
                {
                    return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                        .GetFieldingCareerRecordsBySeriesForTeam(
                            model.TeamId.Value, model.MatchType.Value,
                            model.GroundId.Value, model.HostCountryId.Value, venueId,
                            model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                            model.Limit.Value,
                            sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsBySeriesForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            if (model.OpponentsId.Value != 0)
            {
                if (!model.TeamGrouping)
                {
                    return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                        .GetFieldingCareerRecordsBySeriesAgainstTeam(
                            model.OpponentsId.Value, model.MatchType.Value,
                            model.GroundId.Value, model.HostCountryId.Value,
                            venueId,
                            model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                            model.Limit.Value,
                            sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsBySeriesForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerFieldingRecordDetailsRepository.GetFieldingCareerRecordsBySeries(
                model.MatchType.Value, model.GroundId.Value,
                model.HostCountryId.Value, venueId, model.StartDateEpoch,
                model.EndDateEpoch, model.Season, model.MatchResult.Value, model.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByGround(
            BattingBowlingFieldingModel model)
        {
            var venueId = model.HomeVenue.Value | model.AwayVenue.Value |
                          model.NeutralVenue.Value;

            var sortBy = (int) model.SortOrder;
            var sortDirection = model.SortDirection == SortDirection.Asc ? "ASC" : "DESC";


            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsByGroundForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            if (model.TeamId.Value != 0)
            {
                if (!model.TeamGrouping)
                {
                    return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                        .GetFieldingCareerRecordsByGroundForTeam(
                            model.TeamId.Value, model.MatchType.Value,
                            model.GroundId.Value, model.HostCountryId.Value, venueId,
                            model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                            model.Limit.Value,
                            sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsByGroundForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            if (model.OpponentsId.Value != 0)
            {
                if (!model.TeamGrouping)
                {
                    return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                        .GetFieldingCareerRecordsByGroundAgainstTeam(
                            model.OpponentsId.Value, model.MatchType.Value,
                            model.GroundId.Value, model.HostCountryId.Value,
                            venueId,
                            model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                            model.Limit.Value,
                            sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsByGroundForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerFieldingRecordDetailsRepository.GetFieldingCareerRecordsByGround(
                model.MatchType.Value, model.GroundId.Value,
                model.HostCountryId.Value, venueId, model.StartDateEpoch,
                model.EndDateEpoch, model.Season, model.MatchResult.Value, model.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByHost(
            BattingBowlingFieldingModel model)
        {
            var venueId = model.HomeVenue.Value | model.AwayVenue.Value |
                          model.NeutralVenue.Value;

            var sortBy = (int) model.SortOrder;
            var sortDirection = model.SortDirection == SortDirection.Asc ? "ASC" : "DESC";


            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsByHostForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            if (model.TeamId.Value != 0)
            {
                if (!model.TeamGrouping)
                {
                    return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                        .GetFieldingCareerRecordsByHostForTeam(
                            model.TeamId.Value, model.MatchType.Value,
                            model.GroundId.Value, model.HostCountryId.Value, venueId,
                            model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                            model.Limit.Value,
                            sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsByHostForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            if (model.OpponentsId.Value != 0)
            {
                if (!model.TeamGrouping)
                {
                    return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                        .GetFieldingCareerRecordsByHostAgainstTeam(
                            model.OpponentsId.Value, model.MatchType.Value,
                            model.GroundId.Value, model.HostCountryId.Value,
                            venueId,
                            model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                            model.Limit.Value,
                            sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsByHostForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerFieldingRecordDetailsRepository.GetFieldingCareerRecordsByHost(
                model.MatchType.Value, model.GroundId.Value,
                model.HostCountryId.Value, venueId, model.StartDateEpoch,
                model.EndDateEpoch, model.Season, model.MatchResult.Value, model.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByOpposition(
            BattingBowlingFieldingModel model)
        {
            var venueId = model.HomeVenue.Value | model.AwayVenue.Value |
                          model.NeutralVenue.Value;

            var sortBy = (int) model.SortOrder;
            var sortDirection = model.SortDirection == SortDirection.Asc ? "ASC" : "DESC";


            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsByOppositionForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            if (model.TeamId.Value != 0)
            {
                if (!model.TeamGrouping)
                {
                    return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                        .GetFieldingCareerRecordsByOppositionForTeam(
                            model.TeamId.Value, model.MatchType.Value,
                            model.GroundId.Value, model.HostCountryId.Value, venueId,
                            model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                            model.Limit.Value,
                            sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsByOppositionForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            if (model.OpponentsId.Value != 0)
            {
                if (!model.TeamGrouping)
                {
                    return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                        .GetFieldingCareerRecordsByOppositionAgainstTeam(
                            model.OpponentsId.Value, model.MatchType.Value,
                            model.GroundId.Value, model.HostCountryId.Value,
                            venueId,
                            model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                            model.Limit.Value,
                            sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsByOppositionForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerFieldingRecordDetailsRepository.GetFieldingCareerRecordsByOpposition(
                model.MatchType.Value, model.GroundId.Value,
                model.HostCountryId.Value, venueId, model.StartDateEpoch,
                model.EndDateEpoch, model.Season, model.MatchResult.Value, model.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByYear(
            BattingBowlingFieldingModel model)
        {
            var venueId = model.HomeVenue.Value | model.AwayVenue.Value |
                          model.NeutralVenue.Value;

            var sortBy = (int) model.SortOrder;
            var sortDirection = model.SortDirection == SortDirection.Asc ? "ASC" : "DESC";


            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsByYearForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            if (model.TeamId.Value != 0)
            {
                if (!model.TeamGrouping)
                {
                    return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                        .GetFieldingCareerRecordsByYearForTeam(
                            model.TeamId.Value, model.MatchType.Value,
                            model.GroundId.Value, model.HostCountryId.Value, venueId,
                            model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                            model.Limit.Value,
                            sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsByYearForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            if (model.OpponentsId.Value != 0)
            {
                if (!model.TeamGrouping)
                {
                    return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                        .GetFieldingCareerRecordsByYearAgainstTeam(
                            model.OpponentsId.Value, model.MatchType.Value,
                            model.GroundId.Value, model.HostCountryId.Value,
                            venueId,
                            model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                            model.Limit.Value,
                            sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsByYearForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerFieldingRecordDetailsRepository.GetFieldingCareerRecordsByYear(
                model.MatchType.Value, model.GroundId.Value,
                model.HostCountryId.Value, venueId, model.StartDateEpoch,
                model.EndDateEpoch, model.Season, model.MatchResult.Value, model.Limit.Value,
                sortBy, sortDirection);
        }

        public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsBySeason(
            BattingBowlingFieldingModel model)
        {
            var venueId = model.HomeVenue.Value | model.AwayVenue.Value |
                          model.NeutralVenue.Value;

            var sortBy = (int) model.SortOrder;
            var sortDirection = model.SortDirection == SortDirection.Asc ? "ASC" : "DESC";


            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsBySeasonForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            if (model.TeamId.Value != 0)
            {
                if (!model.TeamGrouping)
                {
                    return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                        .GetFieldingCareerRecordsBySeasonForTeam(
                            model.TeamId.Value, model.MatchType.Value,
                            model.GroundId.Value, model.HostCountryId.Value, venueId,
                            model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                            model.Limit.Value,
                            sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsBySeasonForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            if (model.OpponentsId.Value != 0)
            {
                if (!model.TeamGrouping)
                {
                    return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                        .GetFieldingCareerRecordsBySeasonAgainstTeam(
                            model.OpponentsId.Value, model.MatchType.Value,
                            model.GroundId.Value, model.HostCountryId.Value,
                            venueId,
                            model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                            model.Limit.Value,
                            sortBy, sortDirection);
                }

                return await _unitOfWork.PlayerFieldingRecordDetailsRepository
                    .GetFieldingCareerRecordsBySeasonForTeamAgainstTeam(
                        model.TeamId.Value, model.OpponentsId.Value,
                        model.MatchType.Value, model.GroundId.Value,
                        model.HostCountryId.Value, venueId,
                        model.StartDateEpoch, model.EndDateEpoch, model.Season, model.MatchResult.Value,
                        model.Limit.Value,
                        sortBy, sortDirection);
            }

            return await _unitOfWork.PlayerFieldingRecordDetailsRepository.GetFieldingCareerRecordsBySeason(
                model.MatchType.Value, model.GroundId.Value,
                model.HostCountryId.Value, venueId, model.StartDateEpoch,
                model.EndDateEpoch, model.Season, model.MatchResult.Value, model.Limit.Value,
                sortBy, sortDirection);
        }
    }
}