using AcsRepository.Util;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class GroundsQuery : IRequest<Result<IReadOnlyList<GroundWithCodeDto>, Error>>
{
    private AcsTypes.Types.MatchType MatchType { get; }

    public GroundsQuery(AcsTypes.Types.MatchType matchType)
    {
        MatchType = matchType;
    }


    internal class GroundsQueryHandler
        : IRequestHandler<GroundsQuery, Result<IReadOnlyList<GroundWithCodeDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<GroundsQueryHandler> _logger;

        public GroundsQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<GroundsQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<GroundWithCodeDto>, Error>> Handle(GroundsQuery request,
            CancellationToken cancellationToken)
        {
            string sql =
                @"SELECT `g`.`Id`, @MatchType AS `MatchType`, `c`.`Code`, `c`.`Country` AS `CountryName`, `g`.`GroundId`, `g`.`KnownAs`
                    FROM `Grounds` AS `g`
                    INNER JOIN `CountryCodes` AS `c` ON `g`.`CountryName` = `c`.`Country`
                    WHERE `g`.`MatchType` = @MatchType
                    ORDER BY `g`.`CountryName`, `g`.`KnownAs`";
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = (IReadOnlyList<GroundWithCodeDto>) connection.Query<GroundWithCodeDto>(sql, new
                {
                    MatchType = request.MatchType.Value
                }).ToList();
                return Result.Success<IReadOnlyList<GroundWithCodeDto>, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {MatchType}", request.MatchType);
                return Result.Failure<IReadOnlyList<GroundWithCodeDto>, Error>(Errors.UnexpectedError);
            }
        }
    }
}