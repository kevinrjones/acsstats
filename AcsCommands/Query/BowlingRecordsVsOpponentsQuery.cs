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

public class BowlingRecordsVsOpponentsQuery : IRequest<Result<SqlResultEnvelope<IReadOnlyList<BowlingCareerRecordDto>>, Error>>
{
    private BattingBowlingFieldingModel FieldingModel { get; }
    private string Sql { get; }

    public BowlingRecordsVsOpponentsQuery(BattingBowlingFieldingModel fieldingModel, string sql)
    {
        FieldingModel = fieldingModel;
        Sql = sql;
    }

    internal class BowlingRecordsVsOpponentsQueryHandler
        : IRequestHandler<BowlingRecordsVsOpponentsQuery, Result<SqlResultEnvelope<IReadOnlyList<BowlingCareerRecordDto>>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<BowlingRecordsVsOpponentsQueryHandler> _logger;

        public BowlingRecordsVsOpponentsQueryHandler(
            QueriesConnectionString queriesConnectionString,
            ILogger<BowlingRecordsVsOpponentsQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<SqlResultEnvelope<IReadOnlyList<BowlingCareerRecordDto>>, Error>> Handle(BowlingRecordsVsOpponentsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var grid = connection
                    .QueryMultiple(request.Sql, new
                    {
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
                        wicks_limit = request.FieldingModel.Limit.Value,
                        sort_by = (int) request.FieldingModel.SortOrder,
                        sort_direction = request.FieldingModel.SortDirectionAsString(),
                    start_row = request.FieldingModel.StartRow,
                    page_size = request.FieldingModel.Rows
                    }, commandType: CommandType.StoredProcedure);
                    
                    var result = grid.Read<PlayerBowlingCareerRecordDetails>().ToList();
                    var count = grid.Read<int>().First();
                return Result.Success<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(result).ToEnvelope(count);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {matchType}, " +
                                       "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                       "{sortBy}, {sortDirection} ",
                    request.FieldingModel.MatchType.Value, request.FieldingModel.GroundId.Value,
                    request.FieldingModel.HostCountryId.Value, request.FieldingModel.ToVenue(),
                    request.FieldingModel.StartDateEpoch,
                    request.FieldingModel.EndDateEpoch, request.FieldingModel.Season,
                    request.FieldingModel.MatchResult.Value,
                    (int) request.FieldingModel.SortOrder, request.FieldingModel.SortDirectionAsString());
                return Result.Failure<SqlResultEnvelope<IReadOnlyList<BowlingCareerRecordDto>>, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }
    }
}