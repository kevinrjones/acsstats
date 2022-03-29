using AcsRepository.Util;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class TeamsQuery : IRequest<Result<IReadOnlyList<TeamDto>, Error>>
{
    private AcsTypes.Types.MatchType MatchType { get; }

    public TeamsQuery(AcsTypes.Types.MatchType matchType)
    {
        MatchType = matchType;
    }

    internal sealed class TeamsQueryHandler : IRequestHandler<TeamsQuery, Result<IReadOnlyList<TeamDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<TeamsQueryHandler> _logger;

        public TeamsQueryHandler(QueriesConnectionString queriesConnectionString, ILogger<TeamsQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<TeamDto>, Error>> Handle(TeamsQuery request, CancellationToken cancellationToken)
        {
            string sql = @"SELECT Id, Name, MatchType
            FROM Teams
            WHERE MatchType = @MatchType
            ORDER BY Name;
            ";
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = (IReadOnlyList<TeamDto>)connection.Query<TeamDto>(sql, new
                {
                    MatchType = request.MatchType.Value
                }).ToList();
                return Result.Success<IReadOnlyList<TeamDto>, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {MatchType}", request.MatchType);
                return Result.Failure<IReadOnlyList<TeamDto>, Error>(Errors.UnexpectedError);
            }
        }

    }
}