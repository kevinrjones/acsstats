using AcsRepository.Util;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class TournamentByYearQuery : IRequest<Result<IReadOnlyList<string>, Error>>
{
    private string SeriesDate { get; }

    private IEnumerable<string> MatchTypes { get; }

    public TournamentByYearQuery(List<AcsTypes.Types.MatchType> matchTypes, string seriesDate)
    {
        SeriesDate = seriesDate;
        MatchTypes = matchTypes.Map(m => m.Value);
    }

    internal class TournamentByYearQueryHandler
        : IRequestHandler<TournamentByYearQuery, Result<IReadOnlyList<string>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<TournamentByYearQueryHandler> _logger;

        public TournamentByYearQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<TournamentByYearQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<string>, Error>> Handle(TournamentByYearQuery request,
            CancellationToken cancellationToken)
        {
            string sql =
                @"SELECT DISTINCT Tournament 
                    FROM Matches 
                    WHERE MatchType in @MatchTypes
                    and SeriesDate = @SeriesDate";
            
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = connection.Query<string>(sql, new
                {
                    request.MatchTypes,
                    request.SeriesDate,
                }).ToList();


                return Result.Success<IReadOnlyList<string>, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {MatchType}, {SeriesDate}", request.MatchTypes, request.SeriesDate);
                return Result.Failure<IReadOnlyList<string>, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }
    }
}