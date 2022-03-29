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

public class BowlingRecordsForTeamQuery : IRequest<Result<IReadOnlyList<BowlingCareerRecordDto>, Error>>
{
    private BattingBowlingFieldingModel FieldingModel { get; }
    private string Sql { get; }

    public BowlingRecordsForTeamQuery(BattingBowlingFieldingModel fieldingModel, string sql)
    {
        FieldingModel = fieldingModel;
        Sql = sql;
    }

    internal class BowlingRecordsForTeamQueryHandler
        : IRequestHandler<BowlingRecordsForTeamQuery, Result<IReadOnlyList<BowlingCareerRecordDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<BowlingRecordsForTeamQueryHandler> _logger;

        public BowlingRecordsForTeamQueryHandler(
            QueriesConnectionString queriesConnectionString,
            ILogger<BowlingRecordsForTeamQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<BowlingCareerRecordDto>, Error>> Handle(BowlingRecordsForTeamQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = (IReadOnlyList<PlayerBowlingCareerRecordDetails>) connection
                    .Query<PlayerBowlingCareerRecordDetails>(request.Sql, new
                    {
                        team_id = request.FieldingModel.TeamId.Value,
                        match_type = request.FieldingModel.MatchType.Value,
                        ground_id = request.FieldingModel.GroundId.Value,
                        homecountry_id = request.FieldingModel.HostCountryId.Value,
                        homeOrAway = request.FieldingModel.ToVenue(),
                        startDate = request.FieldingModel.StartDateEpoch,
                        endDate = request.FieldingModel.EndDateEpoch,
                        season = request.FieldingModel.Season,
                        matchResult = request.FieldingModel.MatchResult.Value,
                        wickets_limit = request.FieldingModel.Limit.Value,
                        sort_by = (int) request.FieldingModel.SortOrder,
                        sort_direction = request.FieldingModel.SortDirectionAsString()
                    }, commandType: CommandType.StoredProcedure).ToList();
                return Result.Success<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(result).ToDto();
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
                return Result.Failure<IReadOnlyList<BowlingCareerRecordDto>, Error>(Errors.UnexpectedError);
            }
        }
    }
}