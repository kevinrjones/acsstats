using AcsDto.Dtos;
using AcsRepository.Util;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class MatchSeriesDatesQuery : IRequest<Result<IReadOnlyList<string>, Error>>
{
    private AcsTypes.Types.MatchType MatchType { get; }

    public MatchSeriesDatesQuery(AcsTypes.Types.MatchType matchType)
    {
        MatchType = matchType;
    }

    internal class MatchSeriesDatesQueryHandler
        : IRequestHandler<MatchSeriesDatesQuery, Result<IReadOnlyList<string>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<MatchSeriesDatesQueryHandler> _logger;

        public MatchSeriesDatesQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<MatchSeriesDatesQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<string>, Error>> Handle(MatchSeriesDatesQuery request,
            CancellationToken cancellationToken)
        {
            string sql =
                @"SELECT DISTINCT SeriesDate 
                    FROM Matches 
                    WHERE Id in (select matchid from MatchSubType where MatchType = @matchtype)";
            
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = connection.Query<string>(sql, new
                {
                    MatchType = request.MatchType.Value
                }).ToList();


                return Result.Success<IReadOnlyList<string>, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {MatchType}", request.MatchType);
                return Result.Failure<IReadOnlyList<string>, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }
    }
}