using AcsCommandHandlers;
using AcsRepository.Util;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class GetGroundsQuery : IQuery<IReadOnlyList<GroundDto>>
{
    private AcsTypes.Types.MatchType MatchType { get; }

    public GetGroundsQuery(AcsTypes.Types.MatchType matchType)
    {
        MatchType = matchType;
    }

    internal sealed class GetGroundsQueryHandler : IQueryHandler<GetGroundsQuery, IReadOnlyList<GroundDto>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<GetTeamsQuery.GetTeamsQueryHandler> _logger;

        public GetGroundsQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<GetTeamsQuery.GetTeamsQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<GroundDto>, Error>> Handle(GetGroundsQuery query)
        {
            string sql =
                @"SELECT `g`.`Id`, 't' AS `MatchType`, `c`.`Code`, `c`.`Country` AS `CountryName`, `g`.`GroundId`, `g`.`KnownAs`
                    FROM `Grounds` AS `g`
                    INNER JOIN `CountryCodes` AS `c` ON `g`.`CountryName` = `c`.`Country`
                    WHERE `g`.`MatchType` = 't'
                    ORDER BY `g`.`CountryName`, `g`.`KnownAs`";
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = (IReadOnlyList<GroundDto>) connection.Query<GroundDto>(sql, new
                {
                    MatchType = query.MatchType.Value
                }).ToList();
                return Result.Success<IReadOnlyList<GroundDto>, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {MatchType}", query.MatchType);
                return Result.Failure<IReadOnlyList<GroundDto>, Error>(Errors.UnexpectedError);
            }
        }
    }
}