using AcsRepository.Util;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class TeamsQuery : IRequest<Result<IReadOnlyList<TeamForMatchTypeDto>, Error>>
{
    private AcsTypes.Types.MatchType MatchType { get; }

    public TeamsQuery(AcsTypes.Types.MatchType matchType)
    {
        MatchType = matchType;
    }

    internal sealed class TeamsQueryHandler : IRequestHandler<TeamsQuery, Result<IReadOnlyList<TeamForMatchTypeDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<TeamsQueryHandler> _logger;

        public TeamsQueryHandler(QueriesConnectionString queriesConnectionString, ILogger<TeamsQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<TeamForMatchTypeDto>, Error>> Handle(TeamsQuery request, CancellationToken cancellationToken)
        {
            string sql = @"SELECT T.Id, T.Name, TMT.MatchType
            FROM Teams T
            join teamsmatchtypes tmt
            on t.Id=tmt.TeamId
            WHERE MatchType = @MatchType
            ORDER BY Name";
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = (IReadOnlyList<TeamForMatchTypeDto>)connection.Query<TeamForMatchTypeDto>(sql, new
                {
                    MatchType = request.MatchType.Value
                }).ToList();
                return Result.Success<IReadOnlyList<TeamForMatchTypeDto>, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {MatchType}", request.MatchType);
                return Result.Failure<IReadOnlyList<TeamForMatchTypeDto>, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }

    }
}