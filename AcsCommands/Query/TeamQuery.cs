using AcsRepository.Util;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class TeamQuery : IRequest<Result<TeamDto, Error>>
{
    private TeamId TeamId { get; }

    public TeamQuery(TeamId teamId)
    {
        TeamId = teamId;
    }

    internal class TeamQueryHandler
        : IRequestHandler<TeamQuery, Result<TeamDto, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<TeamQueryHandler> _logger;

        public TeamQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<TeamQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<TeamDto, Error>> Handle(TeamQuery request, CancellationToken cancellationToken)
        {
            string sql =
                @"SELECT `t`.`Id`, t.Name, t.MatchType
                    FROM `Teams` AS `t`
                    WHERE `t`.`id` = @Id";
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = connection.Query<TeamDto>(sql, new
                {
                    Id = request.TeamId.Value
                }).First();
                return Result.Success<TeamDto, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {TeamId}", request.TeamId);
                return Result.Failure<TeamDto, Error>(Errors.UnexpectedError);
            }

        }
    }
}