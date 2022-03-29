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
using Services.Models;

namespace AcsCommands.Query;

public class BattingRecordsVsOpponentsQuery : IRequest<Result<IReadOnlyList<BattingCareerRecordDto>, Error>>
{
    private BattingBowlingFieldingModel FieldingModel { get; }
    private string Sql { get; }

    public BattingRecordsVsOpponentsQuery(BattingBowlingFieldingModel fieldingModel, string sql)
    {
        FieldingModel = fieldingModel;
        Sql = sql;
    }

    internal class BattingRecordsVsOpponentsQueryHandler
        : IRequestHandler<BattingRecordsVsOpponentsQuery, Result<IReadOnlyList<BattingCareerRecordDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<BattingRecordsVsOpponentsQueryHandler> _logger;

        public BattingRecordsVsOpponentsQueryHandler(
            QueriesConnectionString queriesConnectionString,
            ILogger<BattingRecordsVsOpponentsQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;  
        }
        
        public async Task<Result<IReadOnlyList<BattingCareerRecordDto>, Error>> Handle(BattingRecordsVsOpponentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = (IReadOnlyList<PlayerBattingCareerRecordDetails>) connection.Query<PlayerBattingCareerRecordDetails>(request.Sql, new
                {
                    opponents_id = request.FieldingModel.OpponentsId.Value,
                    match_type = request.FieldingModel.MatchType.Value,
                    ground_id = request.FieldingModel.GroundId.Value,
                    homecountry_id = request.FieldingModel.HostCountryId.Value,
                    homeOrAway = request.FieldingModel.ToVenue(),
                    startDate = request.FieldingModel.StartDateEpoch,
                    endDate = request.FieldingModel.EndDateEpoch,
                    season = request.FieldingModel.Season,
                    matchResult = request.FieldingModel.MatchResult.Value,
                    runs_limit = request.FieldingModel.Limit.Value,
                    sort_by = (int) request.FieldingModel.SortOrder,
                    sort_direction = request.FieldingModel.SortDirectionAsString()
                }, commandType:CommandType.StoredProcedure).ToList();
                return Result.Success<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(result).ToDto();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {matchType}, {opponentsId}, " +
                                       "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                       "{sortBy}, {sortDirection} ",
                    request.FieldingModel.MatchType.Value,
                    request.FieldingModel.OpponentsId.Value, request.FieldingModel.GroundId.Value,
                    request.FieldingModel.HostCountryId.Value, request.FieldingModel.ToVenue(), request.FieldingModel.StartDateEpoch,
                    request.FieldingModel.EndDateEpoch, request.FieldingModel.Season, request.FieldingModel.MatchResult.Value,
                    (int) request.FieldingModel.SortOrder, request.FieldingModel.SortDirectionAsString());
                return Result.Failure<IReadOnlyList<BattingCareerRecordDto>, Error>(Errors.UnexpectedError);
            }
        }
    }
}