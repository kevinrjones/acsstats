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

public class TeamOverallExtrasCompleteQuery : IRequest<Result<IReadOnlyList<TeamExtrasDetailsDto>, Error>>
{
    private SharedModel Model { get; }
    public TeamOverallExtrasCompleteQuery(SharedModel model)
    {
        Model = model;
    }

    internal class TeamOverallExtrasCompleteQueryHandler
        : IRequestHandler<TeamOverallExtrasCompleteQuery, Result<IReadOnlyList<TeamExtrasDetailsDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<TeamOverallExtrasCompleteQueryHandler> _logger;

        public TeamOverallExtrasCompleteQueryHandler(
            QueriesConnectionString queriesConnectionString,
            ILogger<TeamOverallExtrasCompleteQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<TeamExtrasDetailsDto>, Error>> Handle(
            TeamOverallExtrasCompleteQuery request, CancellationToken cancellationToken)
        {
            var sql = "team_records_total_extras_overall";
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = (IReadOnlyList<TeamExtrasDetails>) connection
                    .Query<TeamExtrasDetails>(sql, new
                    {
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
                        sort_direction = request.Model.SortDirectionAsString()
                    }, commandType: CommandType.StoredProcedure).ToList();
                return Result.Success<IReadOnlyList<TeamExtrasDetails>, Error>(result).ToDto();
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
                return Result.Failure<IReadOnlyList<TeamExtrasDetailsDto>, Error>(Errors.UnexpectedError);
            }
        }
    }
}