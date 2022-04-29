using System.Globalization;
using System.Text.RegularExpressions;
using AcsDto.Dtos;
using AcsRepository.Util;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class MatchesInTournamentQuery : IRequest<Result<IReadOnlyList<MatchListDto>, Error>>
{
    private string Tournament { get; }

    public MatchesInTournamentQuery(string tournament)
    {
        Tournament = tournament;
    }

    internal class MatchesInTournamentQueryHandler
        : IRequestHandler<MatchesInTournamentQuery, Result<IReadOnlyList<MatchListDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<MatchesInTournamentQueryHandler> _logger;

        public MatchesInTournamentQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<MatchesInTournamentQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<MatchListDto>, Error>> Handle(MatchesInTournamentQuery request,
            CancellationToken cancellationToken)
        {
            string sql =
                @"SELECT HomeTeamName, AwayTeamName, Location, LocationId, MatchStartDate, MatchTitle, Tournament, ResultString 
                    FROM Matches 
                    WHERE Tournament = @Tournament";

            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = connection.Query<LocalMatchListDto>(sql, new
                {
                    request.Tournament,
                }).ToList();


                /*
                 * GroupBy().Select() removes duplicates where we have matches in 'wf' and 'wt' for example
                 */
                return Result.Success<IReadOnlyList<MatchListDto>, Error>(result.Map(ml =>
                    {
                        var date = DateTime.ParseExact(Regex.Replace(ml.MatchStartDate, "(%dth|%dst|%dnd|%drd)", "")
                            , "d MMMM yyyy"
                            , CultureInfo.CurrentCulture).ToString("dd MMM yyyy");

                        return new MatchListDto(ml.HomeTeamName, ml.AwayTeamName, ml.Location, ml.LocationId, date,
                            ml.MatchTitle, ml.Tournament, ml.ResultString,
                            $"{ml.HomeTeamName}-v-{ml.AwayTeamName}-{ml.MatchStartDate}");
                    }).GroupBy(m => m.Key)
                    .Select(c => c.First())
                    .ToList());
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {Tournament}", request.Tournament);
                return Result.Failure<IReadOnlyList<MatchListDto>, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }
    }

    public record LocalMatchListDto(string HomeTeamName, string AwayTeamName, string Location, int LocationId,
        string MatchStartDate,
        string MatchTitle, string Tournament, string ResultString);
}