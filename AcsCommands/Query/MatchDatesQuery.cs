using AcsDto.Dtos;
using AcsRepository.Util;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class MatchDatesQuery : IRequest<Result<IReadOnlyList<MatchDateDto>, Error>>
{
    private AcsTypes.Types.MatchType MatchType { get; }

    public MatchDatesQuery(AcsTypes.Types.MatchType matchType)
    {
        MatchType = matchType;
    }

    internal class MatchDatesQueryHandler
        : IRequestHandler<MatchDatesQuery, Result<IReadOnlyList<MatchDateDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<MatchDatesQueryHandler> _logger;

        public MatchDatesQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<MatchDatesQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<MatchDateDto>, Error>> Handle(MatchDatesQuery request, CancellationToken cancellationToken)
        {
            string getFirstDateSql =
                @"SELECT Id, MatchStartDate as Date, MatchStartDateAsOffset as DateOffset, MatchType, SeriesDate
                    FROM Matches 
                    WHERE MatchType = @MatchType
                    ORDER BY MatchStartDateAsOffset
                LIMIT 1";

            string getLastDateSql =
                @"SELECT Id, MatchStartDate as Date, MatchStartDateAsOffset as DateOffset, MatchType, SeriesDate
                    FROM Matches 
                    WHERE MatchType = @MatchType
                    ORDER BY MatchStartDateAsOffset DESC
                LIMIT 1";

            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var firstDate = connection.Query<MatchDate>(getFirstDateSql, new
                {
                    MatchType = request.MatchType.Value
                }).First();
                
                var lastDate = connection.Query<MatchDate>(getLastDateSql, new
                {
                    MatchType = request.MatchType.Value
                }).First();

                var result = new List<MatchDateDto>
                {
                    new MatchDateDto(firstDate.Date, firstDate.DateOffset, firstDate.MatchType),
                    new MatchDateDto(lastDate.Date, lastDate.DateOffset, lastDate.MatchType),
                };
                
                return Result.Success<IReadOnlyList<MatchDateDto>, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {MatchType}", request.MatchType);
                return Result.Failure<IReadOnlyList<MatchDateDto>, Error>(Errors.GetUnexpectedError(e.Message));
            }

        }
    }
}