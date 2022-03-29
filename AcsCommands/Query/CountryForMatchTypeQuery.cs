using AcsDto.Dtos;
using AcsRepository.Util;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class CountryForMatchTypeQuery : IRequest<Result<IReadOnlyList<CountryDto>, Error>>
{
    private AcsTypes.Types.MatchType MatchType { get; }

    public CountryForMatchTypeQuery(AcsTypes.Types.MatchType matchType)
    {
        MatchType = matchType;
    }

    internal class CountryForMatchTypeQueryHandler
        : IRequestHandler<CountryForMatchTypeQuery, Result<IReadOnlyList<CountryDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<CountryForMatchTypeQueryHandler> _logger;

        public CountryForMatchTypeQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<CountryForMatchTypeQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<CountryDto>, Error>> Handle(CountryForMatchTypeQuery request, CancellationToken cancellationToken)
        {
            string sql =
                @"SELECT DISTINCT CountryId AS Id, CountryName AS Name, @MatchType AS MatchType
                    FROM Grounds 
                    WHERE MatchType = @MatchType";
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = (IReadOnlyList<CountryDto>) connection.Query<CountryDto>(sql, new
                {
                    MatchType = request.MatchType.Value
                }).ToList();
                return Result.Success<IReadOnlyList<CountryDto>, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {MatchType}", request.MatchType.Value);
                return Result.Failure<IReadOnlyList<CountryDto>, Error>(Errors.UnexpectedError);
            }

        }
    }
}