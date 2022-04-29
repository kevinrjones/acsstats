using AcsRepository.Util;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class MatchesDatesQuery : IRequest<Result<IReadOnlyList<string>, Error>>
{
    private int HomeTeamId { get; }
    private int AwayTeamId { get; }
    private AcsTypes.Types.MatchType MatchType { get; }

    public MatchesDatesQuery(int homeTeamId, int awayTeamId, AcsTypes.Types.MatchType matchType)
    {
        HomeTeamId = homeTeamId;
        AwayTeamId = awayTeamId;
        MatchType = matchType;
    }

    internal class MatchesDatesQueryHandler
        : IRequestHandler<MatchesDatesQuery, Result<IReadOnlyList<string>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<MatchesDatesQueryHandler> _logger;

        public MatchesDatesQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<MatchesDatesQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<string>, Error>> Handle(MatchesDatesQuery request,
            CancellationToken cancellationToken)
        {
            string sql =
                @"SELECT MatchStartDate
                    FROM Matches 
                    WHERE MatchType = @MatchType
                    and HomeTeamId = @HomeTeamId
                    and AwayTeamId = @AwayTeamId";

            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = connection.Query<string>(sql, new
                {
                    MatchType = request.MatchType.Value,
                    request.HomeTeamId,
                    request.AwayTeamId,
                }).ToList();


                return Result.Success<IReadOnlyList<string>, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {MatchType}, {HomeTeamId} and {AwayTeamId}",
                    request.MatchType.Value, request.HomeTeamId, request.AwayTeamId);
                return Result.Failure<IReadOnlyList<string>, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }
    }
}