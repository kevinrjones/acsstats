using AcsRepository.Util;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class MatchSeriesDatesForMatchTypesQuery : IRequest<Result<IReadOnlyList<string>, Error>>
{
    private IEnumerable<string> MatchTypes { get; }

    public MatchSeriesDatesForMatchTypesQuery(List<AcsTypes.Types.MatchType> matchTypes)
    {
        MatchTypes = matchTypes.Map(m => m.Value);
    }

    internal class MatchSeriesDatesForMatchTypesQueryHandler
        : IRequestHandler<MatchSeriesDatesForMatchTypesQuery, Result<IReadOnlyList<string>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<MatchSeriesDatesForMatchTypesQueryHandler> _logger;

        public MatchSeriesDatesForMatchTypesQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<MatchSeriesDatesForMatchTypesQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<string>, Error>> Handle(MatchSeriesDatesForMatchTypesQuery request,
            CancellationToken cancellationToken)
        {
            string sql =
                @"SELECT DISTINCT SeriesDate 
                    FROM Matches 
                    WHERE MatchType in @MatchTypes
                    order by SeriesDate";
            
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = connection.Query<string>(sql, new
                {
                    MatchTypes = request.MatchTypes
                }).ToList();


                return Result.Success<IReadOnlyList<string>, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {MatchType}", request.MatchTypes);
                return Result.Failure<IReadOnlyList<string>, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }
    }
}