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

public class BattingIndividualSeriesQuery : IRequest<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>>
{
    private BattingBowlingFieldingModel FieldingModel { get; }

    public BattingIndividualSeriesQuery(BattingBowlingFieldingModel fieldingModel)
    {
        FieldingModel = fieldingModel;
    }

    internal class BattingIndividualSeriesQueryHandler
        : IRequestHandler<BattingIndividualSeriesQuery, Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<BattingIndividualSeriesQueryHandler> _logger;

        public BattingIndividualSeriesQueryHandler(
            QueriesConnectionString queriesConnectionString,
            ILogger<BattingIndividualSeriesQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> Handle(
            BattingIndividualSeriesQuery request, CancellationToken cancellationToken)
        {
            var sql = "batting_individual_career_records_by_series";
            
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var grid = connection
                    .QueryMultiple(sql, new
                    {
                        team_id = request.FieldingModel.TeamId.Value,
                        opponents_id = request.FieldingModel.OpponentsId.Value,
                        match_type = request.FieldingModel.MatchType.Value,
                        match_subtype = request.FieldingModel.MatchSubType.Value,
                        ground_id = request.FieldingModel.GroundId.Value,
                        homecountry_id = request.FieldingModel.HostCountryId.Value,
                        homeOrAway = request.FieldingModel.ToVenue(),
                        startDate = request.FieldingModel.StartDateEpoch,
                        endDate = request.FieldingModel.EndDateEpoch,
                        season = request.FieldingModel.Season,
                        matchResult = request.FieldingModel.MatchResult.Value,
                        runs_limit = request.FieldingModel.Limit.Value,
                        sort_by = (int) request.FieldingModel.SortOrder,
                        sort_direction = request.FieldingModel.SortDirectionAsString(),
                        start_row = request.FieldingModel.StartRow,
                        page_size = request.FieldingModel.Rows
                    }, commandType: CommandType.StoredProcedure);
                
                var result =
                    (IReadOnlyList<PlayerBattingCareerRecordDetails>) grid.Read<PlayerBattingCareerRecordDetails>()
                        .ToList();
                var count = grid.Read<int>().First();

                return Result.Success<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(result).ToEnvelope(count);

            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, {opponentsId}, " +
                                       "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                       "{sortBy}, {sortDirection} ",
                    request.FieldingModel.MatchType.Value, request.FieldingModel.TeamId.Value,
                    request.FieldingModel.OpponentsId.Value, request.FieldingModel.GroundId.Value,
                    request.FieldingModel.HostCountryId.Value, request.FieldingModel.ToVenue(),
                    request.FieldingModel.StartDateEpoch,
                    request.FieldingModel.EndDateEpoch, request.FieldingModel.Season,
                    request.FieldingModel.MatchResult.Value,
                    (int) request.FieldingModel.SortOrder, request.FieldingModel.SortDirectionAsString());
                return Result.Failure<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }
    }
}