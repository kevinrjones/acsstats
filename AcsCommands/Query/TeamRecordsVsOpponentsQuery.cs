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

public class TeamRecordsVsOpponentsQuery : IRequest<Result<IReadOnlyList<TeamRecordDetailsDto>, Error>>
{
    private SharedModel Model { get; }
    private string Sql { get; }

    public TeamRecordsVsOpponentsQuery(SharedModel model, string sql)
    {
        Model = model;
        Sql = sql;
    }
    
    internal class TeamRecordsVsOpponentsQueryHandler
        : IRequestHandler<TeamRecordsVsOpponentsQuery, Result<IReadOnlyList<TeamRecordDetailsDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<TeamRecordsVsOpponentsQueryHandler> _logger;

        public TeamRecordsVsOpponentsQueryHandler(
            QueriesConnectionString queriesConnectionString,
            ILogger<TeamRecordsVsOpponentsQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<TeamRecordDetailsDto>, Error>> Handle(TeamRecordsVsOpponentsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = (IReadOnlyList<TeamRecordDetails>) connection
                    .Query<TeamRecordDetails>(request.Sql, new
                    {
                        opponents_id = request.Model.OpponentsId.Value,
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
                return Result.Success<IReadOnlyList<TeamRecordDetails>, Error>(result).ToDto();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {matchType}, " +
                                       "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                       "{sortBy}, {sortDirection} ",
                    request.Model.MatchType.Value, request.Model.GroundId.Value,
                    request.Model.HostCountryId.Value, request.Model.ToVenue(),
                    request.Model.StartDateEpoch,
                    request.Model.EndDateEpoch, request.Model.Season,
                    request.Model.MatchResult.Value,
                    (int) request.Model.SortOrder, request.Model.SortDirectionAsString());
                return Result.Failure<IReadOnlyList<TeamRecordDetailsDto>, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }
    }
}