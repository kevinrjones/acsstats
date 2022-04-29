using System.Globalization;
using System.Text.RegularExpressions;
using AcsDto.Dtos;
using AcsRepository.Util;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class SearchQuery : IRequest<Result<IReadOnlyList<MatchListDto>, Error>>
{
    private string HomeTeamName { get; }
    public string AwayTeamName { get; }
    public string StartDate { get; }
    public string EndDate { get; }
    public IEnumerable<int> Venue { get; }
    public int[] VictoryType { get; }
    public int MatchResult { get; }
    public string MatchType { get; }

    public bool ExactHomeTeamMatch { get; set; }
    public bool ExactAwayTeamMatch { get; set; }

    public SearchQuery(string? homeTeam, string? awayTeam, EpochDateType startDate,
        EpochDateType endDate, int[]? venue, int? matchResult, AcsTypes.Types.MatchType matchType,
        bool exactHomeTeamMatch, bool exactAwayTeamMatch)
    {
        HomeTeamName = homeTeam ?? "";
        AwayTeamName = awayTeam ?? "";
        StartDate = startDate.EpochDate.ToString();
        EndDate = endDate.EpochDate.ToString();
        Venue = GetVenue(venue);
        VictoryType = GetVictoryValuesFromMatchResult(matchResult);
        MatchResult = matchResult ?? 1;
        MatchType = matchType;
        ExactHomeTeamMatch = exactHomeTeamMatch;
        ExactAwayTeamMatch = exactAwayTeamMatch;
    }

    private static IEnumerable<int> GetVenue(int[]? venue)
    {
        if (venue == null) return new[] {1, 2, 4};
        if (venue.Length == 1 && venue[0] == 0) return new[] {1, 2, 4};
        return venue;
    }

    private int[] GetVictoryValuesFromMatchResult(int? matchResult)
    {
        if (matchResult is null or 0)
        {
            return new[]
            {
                (int) AcsDto.Dtos.VictoryType.Awarded,
                (int) AcsDto.Dtos.VictoryType.Innings,
                (int) AcsDto.Dtos.VictoryType.Runs,
                (int) AcsDto.Dtos.VictoryType.RunRate,
                (int) AcsDto.Dtos.VictoryType.Wickets,
                (int) AcsDto.Dtos.VictoryType.LosingFewerWickets,
                (int) AcsDto.Dtos.VictoryType.FasterScoringRate,
                (int) AcsDto.Dtos.VictoryType.Drawn,
                (int) AcsDto.Dtos.VictoryType.Tied,
                (int) AcsDto.Dtos.VictoryType.NoResult,
                (int) AcsDto.Dtos.VictoryType.Abandoned,
                (int) AcsDto.Dtos.VictoryType.Unknown,
            };
        }

        if (matchResult is 1 or 5)
        {
            return new[]
            {
                (int) AcsDto.Dtos.VictoryType.Awarded,
                (int) AcsDto.Dtos.VictoryType.Innings,
                (int) AcsDto.Dtos.VictoryType.Runs,
                (int) AcsDto.Dtos.VictoryType.RunRate,
                (int) AcsDto.Dtos.VictoryType.Wickets,
                (int) AcsDto.Dtos.VictoryType.LosingFewerWickets,
                (int) AcsDto.Dtos.VictoryType.FasterScoringRate,
                (int) AcsDto.Dtos.VictoryType.Unknown,
            };
        }

        if (matchResult is 2 or 6)
        {
            return new[]
            {
                (int) AcsDto.Dtos.VictoryType.Innings,
                (int) AcsDto.Dtos.VictoryType.Unknown,
            };
        }

        if (matchResult is 3 or 7)
        {
            return new[]
            {
                (int) AcsDto.Dtos.VictoryType.Runs,
                (int) AcsDto.Dtos.VictoryType.Unknown,
            };
        }

        if (matchResult is 4 or 8)
        {
            return new[]
            {
                (int) AcsDto.Dtos.VictoryType.Wickets,
                (int) AcsDto.Dtos.VictoryType.Unknown,
            };
        }

        if (matchResult is 9)
        {
            return new[]
            {
                (int) AcsDto.Dtos.VictoryType.Drawn,
                (int) AcsDto.Dtos.VictoryType.Unknown,
            };
        }

        if (matchResult is 10)
        {
            return new[]
            {
                (int) AcsDto.Dtos.VictoryType.Tied,
                (int) AcsDto.Dtos.VictoryType.Unknown,
            };
        }

        if (matchResult is 11)
        {
            return new[]
            {
                (int) AcsDto.Dtos.VictoryType.NoResult,
                (int) AcsDto.Dtos.VictoryType.Unknown,
            };
        }

        return Array.Empty<int>();
    }

    internal class SearchQueryHandler
        : IRequestHandler<SearchQuery, Result<IReadOnlyList<MatchListDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<SearchQueryHandler> _logger;

        public SearchQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<SearchQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<MatchListDto>, Error>> Handle(SearchQuery request,
            CancellationToken cancellationToken)
        {
            string sql =
                @"SELECT HomeTeamName, AwayTeamName, Location, LocationId, MatchStartDate, MatchTitle, Tournament, ResultString 
                    from matches M                    
                    join ExtraMatchDetails EMD on M.Id = EMD.MatchId
                    where (HomeTeamName like @likehometeamname and (@awayteamname = '' or AwayTeamName like @likeawayteamname) 
                               and emd.TeamId = HomeTeamId  and emd.Result in @matchresult and VictoryType in @victorytype 
                               and emd.HomeAway in @venue
                      or (AwayTeamName like @likehometeamname and (@awayteamname='' or HomeTeamName like @likeawayteamname) 
                              and emd.TeamId = AwayTeamId and emd.Result in @matchresult and VictoryType in @victorytype) and emd.HomeAway in @venue)
                      and @startdate <= M.MatchStartDateAsOffset
                      and @enddate >= M.MatchStartDateAsOffset
                       and M.MatchType = @matchtype
                       order by M.MatchStartDateAsOffset";

            try
            {
                var likeHomeTeamName =
                    request.ExactHomeTeamMatch ? request.HomeTeamName : "%" + request.HomeTeamName + "%";
                var likeAwayTeamName =
                    request.ExactAwayTeamMatch ? request.AwayTeamName : "%" + request.AwayTeamName + "%";


                var matchresult = new int[] {1, 2, 4, 8};
                if (request.MatchResult is 1 or 2 or 3 or 4) // main team win
                {
                    matchresult = new[] {1};
                }

                if (request.MatchResult is 5 or 6 or 7 or 8) // main team loss
                {
                    matchresult = new[] {2};
                }

                if (request.MatchResult is 9) // main team draw
                {
                    matchresult = new[] {4};
                }

                if (request.MatchResult is 10) // main team tied
                {
                    matchresult = new[] {8};
                }

                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = connection.Query<LocalMatchListDto>(sql, new
                {
                    request.HomeTeamName,
                    request.AwayTeamName,
                    LikeHomeTeamName = likeHomeTeamName,
                    LikeAwayTeamname = likeAwayTeamName,
                    request.StartDate,
                    request.EndDate,
                    request.Venue,
                    request.MatchType,
                    request.VictoryType,
                    matchresult
                }).ToList();

                return Result.Success<IReadOnlyList<MatchListDto>, Error>(result.Map(ml =>
                {
                    var date = DateTime.ParseExact(Regex.Replace(ml.MatchStartDate, "(%dth|%dst|%dnd|%drd)", "")
                        , "d MMMM yyyy"
                        , CultureInfo.CurrentCulture).ToString("dd MMM yyyy");

                    return new MatchListDto(ml.HomeTeamName, ml.AwayTeamName, ml.Location, ml.LocationId,
                        date,
                        ml.MatchTitle, ml.Tournament, ml.ResultString,
                        $"{ml.HomeTeamName}-v-{ml.AwayTeamName}-{ml.MatchStartDate}");
                }).ToList());
            }
            catch (Exception e)
            {
                _logger.LogCritical(e,
                    "Unable to process this request: {HomeTeam}, {AwayTeam}, {StartDate}, {EndDate}, {Venue}, {MatchResult}, {MatchType}, ",
                    request.HomeTeamName,
                    request.AwayTeamName, request.StartDate, request.EndDate, request.Venue, request.VictoryType,
                    request.MatchType);
                return Result.Failure<IReadOnlyList<MatchListDto>, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }
    }

    public record LocalMatchListDto(string HomeTeamName, string AwayTeamName, string Location, int LocationId,
        string MatchStartDate, string MatchTitle, string Tournament, string ResultString);
}