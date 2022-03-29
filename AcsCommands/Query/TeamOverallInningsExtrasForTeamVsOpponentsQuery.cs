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

public class TeamOverallInningsExtrasForTeamVsOpponentsQuery : IRequest<Result<IReadOnlyList<InningsExtrasDetailsDto>, Error>>
{
    private SharedModel Model { get; }

    public TeamOverallInningsExtrasForTeamVsOpponentsQuery(SharedModel model)
    {
        Model = model;
    }

    internal class TeamOverallInningsExtrasForTeamVsOpponentsQueryHandler
        : IRequestHandler<TeamOverallInningsExtrasForTeamVsOpponentsQuery, Result<IReadOnlyList<InningsExtrasDetailsDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<TeamOverallInningsExtrasForTeamVsOpponentsQueryHandler> _logger;

        public TeamOverallInningsExtrasForTeamVsOpponentsQueryHandler(
            QueriesConnectionString queriesConnectionString,
            ILogger<TeamOverallInningsExtrasForTeamVsOpponentsQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<InningsExtrasDetailsDto>, Error>> Handle(
            TeamOverallInningsExtrasForTeamVsOpponentsQuery request, CancellationToken cancellationToken)
        {
            var sql = "team_records_total_extras_for_team_against_opponents";

            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = (IReadOnlyList<InningsExtrasDetails>) connection
                    .Query<InningsExtrasDetails>(sql, new
                    {
                        team_id = request.Model.TeamId.Value,
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
                        sort_direction = request.Model.SortDirectionAsString()
                    }, commandType: CommandType.StoredProcedure).ToList();
                return Result.Success<IReadOnlyList<InningsExtrasDetails>, Error>(result).ToDto();
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
                return Result.Failure<IReadOnlyList<InningsExtrasDetailsDto>, Error>(Errors.UnexpectedError);
            }
        }
    }
}