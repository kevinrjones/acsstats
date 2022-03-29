using System.Collections.Generic;
using System.Threading.Tasks;
using AcsCommands.Query;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using MediatR;

namespace Services.AcsServices
{
    public class PartnershipService : IPartnershipService
    {
        private readonly IMediator _mediator;

        public PartnershipService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>> GetPartnershipCareerRecords(
            PartnershipModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsForTeamVsOpponentsQuery(model,
                    "fow_career_records_for_team_against_opponent"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsForTeamQuery(model,
                    "fow_career_records_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsVsOpponentsQuery(model,
                    "fow_career_records_against_opponent"));
            }

            return await _mediator.Send(new PartnershipRecordsCompleteQuery(model,
                "fow_career_records_complete"));
        }


        public async Task<Result<IReadOnlyList<PartnershipIndividualRecordDetailsDto>, Error>>
            GetPartnershipIndividualInnings(
                PartnershipModel model)
        {
            return await _mediator.Send(
                new PartnershipIndividualCareerRecordsQuery(model, "fow_partnership_list_by_innings"));
        }

        public async Task<Result<IReadOnlyList<PartnershipIndividualRecordDetailsDto>, Error>>
            GetPartnershipIndividualMatches(PartnershipModel model)
        {
            return await _mediator.Send(
                new PartnershipIndividualCareerRecordsQuery(model, "fow_partnership_list_by_match"));
        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>>
            GetPartnershipIndividualSeries(PartnershipModel model)
        {
            return await _mediator.Send(new PartnershipIndividualSeriesQuery(model));
        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>>
            GetPartnershipIndividualGrounds(PartnershipModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsForTeamVsOpponentsQuery(model,
                    "fow_partnership_list_by_ground_for_team_against_opponents"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsForTeamQuery(model,
                    "fow_partnership_list_by_ground_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsVsOpponentsQuery(model,
                    "fow_partnership_list_by_ground_against_opponents"));
            }

            return await _mediator.Send(new PartnershipRecordsCompleteQuery(model,
                "fow_partnership_list_by_ground_complete"));
        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>> GetPartnershipIndividualHost(
            PartnershipModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsForTeamVsOpponentsQuery(model,
                    "fow_partnership_list_by_host_for_team_against_opponents"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsForTeamQuery(model,
                    "fow_partnership_list_by_host_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsVsOpponentsQuery(model,
                    "fow_partnership_list_by_host_against_opponents"));
            }

            return await _mediator.Send(new PartnershipRecordsCompleteQuery(model,
                "fow_partnership_list_by_host_complete"));
        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>>
            GetPartnershipIndividualOpponents(
                PartnershipModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsForTeamVsOpponentsQuery(model,
                    "fow_partnership_list_by_opposition_for_team_against_opponents"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsForTeamQuery(model,
                    "fow_partnership_list_by_opposition_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsVsOpponentsQuery(model,
                    "fow_partnership_list_by_opposition_against_opponents"));
            }

            return await _mediator.Send(new PartnershipRecordsCompleteQuery(model,
                "fow_partnership_list_by_opposition_complete"));
        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>>
            GetPartnershipIndividualSeason(
                PartnershipModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsForTeamVsOpponentsQuery(model,
                    "fow_partnership_list_by_season_for_team_against_opponents"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsForTeamQuery(model,
                    "fow_partnership_list_by_season_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsVsOpponentsQuery(model,
                    "fow_partnership_list_by_season_against_opponents"));
            }

            return await _mediator.Send(new PartnershipRecordsCompleteQuery(model,
                "fow_partnership_list_by_season_complete"));
        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>> GetPartnershipIndividualYear(
            PartnershipModel model)
        {
            if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsForTeamVsOpponentsQuery(model,
                    "fow_partnership_list_by_year_for_team_against_opponents"));
            }

            if (model.TeamId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsForTeamQuery(model,
                    "fow_partnership_list_by_year_for_team"));
            }

            if (model.OpponentsId.Value != 0)
            {
                return await _mediator.Send(new PartnershipRecordsVsOpponentsQuery(model,
                    "fow_partnership_list_by_year_against_opponents"));
            }

            return await _mediator.Send(new PartnershipRecordsCompleteQuery(model,
                "fow_partnership_list_by_year_complete"));
        }
    }
}