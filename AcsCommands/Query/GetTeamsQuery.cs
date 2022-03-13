using AcsRepository.Util;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class GetTeamsQuery : IRequest<Result<IReadOnlyList<TeamDto>, Error>>
{
    private AcsTypes.Types.MatchType MatchType { get; }

    public GetTeamsQuery(AcsTypes.Types.MatchType matchType)
    {
        MatchType = matchType;
    }

    internal sealed class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, Result<IReadOnlyList<TeamDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<GetTeamsQueryHandler> _logger;

        public GetTeamsQueryHandler(QueriesConnectionString queriesConnectionString, ILogger<GetTeamsQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<TeamDto>, Error>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
        {
            string sql = @"SELECT Id, MatchType, Name
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