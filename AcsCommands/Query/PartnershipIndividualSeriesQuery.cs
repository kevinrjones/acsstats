using System.Data;
using AcsCommands.Util;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsRepository.Util;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class PartnershipIndividualSeriesQuery : 
    IRequest<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>>
{
    private PartnershipModel Model { get; }

    public PartnershipIndividualSeriesQuery(PartnershipModel model)
    {
        Model = model;
    }

    internal class PartnershipIndividualSeriesQueryHandler
        : IRequestHandler<PartnershipIndividualSeriesQuery,
            Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<PartnershipIndividualSeriesQueryHandler> _logger;

        public PartnershipIndividualSeriesQueryHandler(
            QueriesConnectionString queriesConnectionString,
            ILogger<PartnershipIndividualSeriesQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>> Handle(
            PartnershipIndividualSeriesQuery request, CancellationToken cancellationToken)
        {
            var sql = "fow_partnership_list_by_series";

            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = (IReadOnlyList<PartnershipCareerRecordDetails>) connection
                    .Query<PartnershipCareerRecordDetails>(sql, new
                    {
                        team_id = request.Model.TeamId.Value,
                        opponent_id = request.Model.OpponentsId.Value,
                        match_type = request.Model.MatchType.Value,
                        ground_id = request.Model.GroundId.Value,
                        homecountry_id = request.Model.HostCountryId.Value,
                        homeOrAway = request.Model.ToVenue(),
                        startDate = request.Model.StartDateEpoch,
                        endDate = request.Model.EndDateEpoch,
                        season = request.Model.Season,
                        matchResult = request.Model.MatchResult.Value,
                        runs_limit = request.Model.Limit.Value,
                        sort_by = (int) request.Model.SortOrder,
                        sort_direction = request.Model.SortDirectionAsString(),
                    start_row = request.Model.StartRow,
                    page_size = request.Model.Rows
                    }, commandType: CommandType.StoredProcedure).ToList();
                return Result.Success<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(result).ToDto();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, {opponentsId}, " +
                                       "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                       "{sortBy}, {sortDirection} ",
                    request.Model.MatchType.Value, request.Model.TeamId.Value,
                    request.Model.OpponentsId.Value, request.Model.GroundId.Value,
                    request.Model.HostCountryId.Value, request.Model.ToVenue(),
                    request.Model.StartDateEpoch,
                    request.Model.EndDateEpoch, request.Model.Season,
                    request.Model.MatchResult.Value,
                    (int) request.Model.SortOrder, request.Model.SortDirectionAsString());
                return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }
    }
}