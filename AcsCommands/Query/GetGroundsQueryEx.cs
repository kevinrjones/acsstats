using AcsRepository.Util;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class GetGroundsQuery : IRequest<Result<IReadOnlyList<GroundDto>, Error>>
{
    public AcsTypes.Types.MatchType MatchType { get; }

    public GetGroundsQuery(AcsTypes.Types.MatchType matchType)
    {
        MatchType = matchType;
    }


    internal class GetGroundsQueryHandler
        : IRequestHandler<GetGroundsQuery, Result<IReadOnlyList<GroundDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<GetGroundsQueryHandler> _logger;

        public GetGroundsQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<GetGroundsQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<GroundDto>, Error>> Handle(GetGroundsQuery request,
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
                var result = (IReadOnlyList<GroundDto>) connection.Query<GroundDto>(sql, new
                {
                    MatchType = request.MatchType.Value
                }).ToList();
                return Result.Success<IReadOnlyList<GroundDto>, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {MatchType}", request.MatchType);
                return Result.Failure<IReadOnlyList<GroundDto>, Error>(Errors.UnexpectedError);
            }
        }
    }
}