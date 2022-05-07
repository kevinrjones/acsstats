using AcsDto.Dtos;
using AcsRepository.Util;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class ScorecardQuery : IRequest<Result<ScorecardDto, Error>>
{
    private string HomeTeam { get; }
    private string AwayTeam { get; }
    private string Date { get; }
    private int MatchId { get; }


    public ScorecardQuery(string homeTeam, string awayTeam, string date)
    {
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        Date = date;
    }

    public ScorecardQuery(int matchId)
    {
        MatchId = matchId;
    }

    internal class ScorecardQueryHandler
        : IRequestHandler<ScorecardQuery, Result<ScorecardDto, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<ScorecardQueryHandler> _logger;

        private string findMatch =
            @"select M.Id, M.HomeTeamId, M.HomeTeamName, M.AwayTeamId, M.AwayTeamName, M.MatchDesignator, M.MatchTitle, M.Location, M.LocationId, M.MatchDate,
               M.SeriesDate, M.ResultString, M.BallsPerOver, M.DayNight, M.TossTeamId, t.name as Toss, 
               M.WhoWonId, w.name as WhoWon, M.WhoLostId, l.name as WhoLost, M.VictoryType, M.MatchType
               from Matches M
                left join teams t on t.id=m.TossTeamId
                left join teams w on w.id=m.WhoWonId
                left join teams l on l.id=m.WhoLostId 
          where HomeTeamName=@HomeTeamName
          and AwayTeamName=@AwayTeamName
          and MatchStartDate=@MatchStartDate";

        private string matchSql =
            @"select M.Id, M.HomeTeamId, M.HomeTeamName, M.AwayTeamId, M.AwayTeamName, M.MatchDesignator, M.MatchTitle, M.Location, M.LocationId, M.MatchDate,
               M.SeriesDate, M.ResultString, M.BallsPerOver, M.DayNight, M.TossTeamId, t.name as Toss, 
               M.WhoWonId, w.name as WhoWon, M.WhoLostId, l.name as WhoLost, M.VictoryType, M.MatchType
               from Matches M
                left join teams t on t.id=m.TossTeamId
                left join teams w on w.id=m.WhoWonId
                left join teams l on l.id=m.WhoLostId
               where M.id =@MatchId";

        private string inningSql =
            @"select i.TeamId as hometeamid,
       t.name as HomeTeamName,
       i.OpponentsId as awayteamid,
       o.name as AwayTeamName,
       InningsNumber,
       InningsOrder,
       i.Total,
       i.Wickets,
       i.Complete,
       i.Minutes,
       i.Byes,
       i.LegByes,
       i.Wides,
       i.Noballs,
       i.Penalty,
       i.Extras,
       i.Overs,
       i.Declared
        from Innings i
         join teams t on t.id = i.TeamId
         join teams o on o.id = i.OpponentsId
      where i.matchid = @MatchId";

        private string battingSql =
            @"select BattingDetails.PlayerId,
               FullName as PlayerName,
               InningsNumber,
               InningsOrder,
               Dismissal,
               DismissalType,
               BowlerId,
               BowlerName as BowlerName,
               FielderId,
               FielderName as FielderName,
               Score,
               Position,
               NotOut,
               Balls,
               Minutes,
               Fours,
               Sixes,
               Captain,
               WicketKeeper
                from BattingDetails
      where BattingDetails.matchid = @MatchId
      order by InningsOrder, Position";

        private string bowlingSql =
            @"select PlayerId,
               Name as FullName,
               InningsOrder,
               Overs,
               Balls,
               Runs,
               Maidens,
               Wickets,
               Fours,
               Sixes,
               Dots,
               Wides,
               NoBalls,
               Captain
        from BowlingDetails
      where BowlingDetails.matchid = @MatchId
        order by InningsOrder, Position";

        private string officialsSql = @"select u.id as `key`, u.FullName as name from {0} um
        join {1} u on um.PersonId = u.id
        where matchid =  @MatchId";

        private string fallOfWicketsSql =
            @"select 
                MatchId, 
                InningsOrder, 
                Wicket, 
                FallOfWickets.PlayerId, 
                Players.SortNamePart as PlayerName,                
                CurrentScore, 
                Overs from FallOfWickets
                      join Players on FallOfWickets.PlayerId = Players.Id 
        where matchid =  @MatchId";

        private string closeOfPlaySql =
            @"select Day, Note from CloseOfPlay
        where matchid =  @MatchId";

        private string notesSql =
            @"select Note from Notes
        where matchid =  @MatchId";

        private string debutsSql = @"select P.PlayerId, P.FullName, P.SortNamePart, T.TeamId, T.Name as TeamName
        from PlayersTeams
        join Players P on PlayersTeams.PlayerId = P.Id
            join teams T on PlayersTeams.TeamId = T.Id
            where DebutId = @MatchId";

        public ScorecardQueryHandler(
            QueriesConnectionString queriesConnectionString,
            ILogger<ScorecardQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }


        public async Task<Result<ScorecardDto, Error>> Handle(ScorecardQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var matchData = connection.Query<LocalMatchDto>(findMatch, new
                {
                    HomeTeamName = request.HomeTeam,
                    AwayTeamName = request.AwayTeam,
                    MatchStartDate = request.Date
                }).FirstOrDefault();

                if (matchData != null)
                {
                    var innings = connection.Query<LocalInningsDto>(inningSql, new
                    {
                        MatchId = matchData.Id
                    });

                    var fowData = connection.Query<LocalFowDto>(fallOfWicketsSql, new
                    {
                        MatchId = matchData.Id
                    });

                    var umpiresSql = string.Format(officialsSql, "UmpiresMatches", "Umpires");

                    var umpires = connection.Query<PersonDto>(umpiresSql, new
                    {
                        MatchId = matchData.Id
                    });

                    umpiresSql = string.Format(officialsSql, "MatchRefereesMatches", "MatchReferees");

                    var referees = connection.Query<PersonDto>(umpiresSql, new
                    {
                        MatchId = matchData.Id
                    });
                    umpiresSql = string.Format(officialsSql, "TVUmpiresMatches", "TVUmpires");

                    var tvumpires = connection.Query<PersonDto>(umpiresSql, new
                    {
                        MatchId = matchData.Id
                    });

                    umpiresSql = string.Format(officialsSql, "ScorersMatches", "Scorers");

                    var scorers = connection.Query<PersonDto>(umpiresSql, new
                    {
                        MatchId = matchData.Id
                    });

                    var closeOfPlay = connection.Query<CloseOfPlayDto>(closeOfPlaySql, new
                    {
                        MatchId = matchData.Id
                    });

                    var notes = connection.Query<string>(notesSql, new
                    {
                        MatchId = matchData.Id
                    });

                    var debuts = connection.Query<DebutDto>(debutsSql, new
                    {
                        MatchId = matchData.Id
                    });

                    var batting = connection.Query<LocalBattingDetailDto>(battingSql, new
                    {
                        MatchId = matchData.Id
                    });

                    var bowling = connection.Query<LocalBowlingDetailDto>(bowlingSql, new
                    {
                        MatchId = matchData.Id
                    });


                    ScorecardTeamDto? tossteam = matchData.TossTeamId != null
                        ? new ScorecardTeamDto(matchData.TossTeamId.Value, matchData.Toss)
                        : null;
                    var where = new LocationDto(matchData.LocationId, matchData.Location);

                    var result = new ResultDto(
                        matchData.WhoWonId != null
                            ? new ScorecardTeamDto(matchData.WhoWonId.Value, matchData.WhoWon)
                            : null,
                        matchData.WhoLostId != null
                            ? new ScorecardTeamDto(matchData.WhoLostId.Value, matchData.WhoLost)
                            : null,
                        matchData.ResultString,
                        (VictoryType) matchData.VictoryType);

                    var awayTeam = new ScorecardTeamDto(matchData.AwayTeamId, matchData.AwayTeamName);
                    var homeTeam = new ScorecardTeamDto(matchData.HomeTeamId, matchData.HomeTeamName);

                    var scorecardHeaderDto = new ScorecardHeaderDto(tossteam
                        , where
                        , result
                        , scorers.ToList()
                        , umpires.ToList()
                        , awayTeam
                        , Array.Empty<string>()
                        , matchData.DayNight
                        , homeTeam
                        , Array.Empty<string>()
                        , tvumpires.ToList()
                        , matchData.MatchDate
                        , matchData.MatchType
                        , matchData.MatchTitle
                        , matchData.SeriesDate
                        , closeOfPlay.ToList()
                        , matchData.BallsPerOver
                        , referees.ToList()
                        , matchData.MatchDesignator
                    );

                    var battingDtos = new List<List<BattlingLineDto>>();

                    var inningsOrder = -1;
                    var maxInningsOrder = batting.MaxBy(b => b.InningsOrder).InningsOrder;
                    while (++inningsOrder <= maxInningsOrder)
                    {
                        battingDtos.Add(batting.Filter(b => b.InningsOrder == inningsOrder)
                            .Map(b =>
                                new BattlingLineDto(new PersonDto(b.PlayerId, b.PlayerName)
                                    , b.Score
                                    , b.Balls
                                    , b.Fours
                                    , b.Sixes
                                    , b.NotOut != 0
                                    , b.Minutes
                                    , b.Position
                                    , new DismissalDto(b.DismissalType, b.Dismissal, new PersonDto(b.BowlerId, b.BowlerName),
                                        new PersonDto(b.FielderId, b.FielderName))
                                    , b.Captain != 0
                                    , b.WicketKeeper != 0
                                )).ToList());
                    }

                    var bowlingDtos = new List<List<BowlingLineDto>>();

                    inningsOrder = -1;
                    maxInningsOrder = bowling.MaxBy(b => b.InningsOrder).InningsOrder;
                    while (++inningsOrder <= maxInningsOrder)
                    {
                        bowlingDtos.Add(bowling.Filter(b => b.InningsOrder == inningsOrder)
                            .Map(b =>
                                new BowlingLineDto(
                                    b.Dots
                                    , b.Runs
                                    , b.Balls
                                    , b.Fours
                                    , b.Sixes
                                    , b.Overs
                                    , b.Wides
                                    , new PersonDto(b.PlayerId, b.FullName)
                                    , b.Maidens
                                    , b.NoBalls
                                    , b.Wickets
                                    , b.Captain != 0
                                )).ToList());
                    }

                    var fallOfWicketDtos = new List<List<FallOfWicketDto>>();

                    inningsOrder = -1;
                    if (fowData.Length() > 0)
                    {
                        maxInningsOrder = fowData.MaxBy(b => b.InningsOrder).InningsOrder;
                        while (++inningsOrder <= maxInningsOrder)
                        {
                            fallOfWicketDtos.Add(fowData.Filter(b => b.InningsOrder == inningsOrder)
                                .Map(f =>
                                    new FallOfWicketDto(
                                        f.Wicket
                                        , f.CurrentScore
                                        , new PersonDto(f.PlayerId, f.PlayerName)
                                        , f.overs
                                    )).ToList());
                        }
                    }

                    var inningsDtos = innings.Map(inning =>
                        new InningDto(
                            new ScorecardTeamDto(inning.HomeTeamId, inning.HomeTeamName)
                            , new ScorecardTeamDto(inning.AwayTeamId, inning.AwayTeamName)
                            , inning.InningsNumber
                            , inning.InningsOrder
                            , new TotalDto(inning.Wickets, inning.Declared != 0, inning.Overs, inning.Minutes,
                                inning.Total)
                            , new ExtrasDto(inning.Byes, inning.LegByes, inning.Wides, inning.Noballs, inning.Penalty,
                                inning.Total)
                            , battingDtos.Count > inning.InningsOrder ? battingDtos[inning.InningsOrder] : new List<BattlingLineDto>()
                            , bowlingDtos.Count > inning.InningsOrder ? bowlingDtos[inning.InningsOrder] : new List<BowlingLineDto>()
                            , fallOfWicketDtos.Count > inning.InningsOrder ? fallOfWicketDtos[inning.InningsOrder] : new List<FallOfWicketDto>()
                        )).ToList();

                    var scoreCard = new ScorecardDto(notes.ToList(), debuts.ToList(), scorecardHeaderDto, inningsDtos);
                    return Result.Success<ScorecardDto, Error>(scoreCard);
                }
                else
                {
                    _logger.LogError("Unable to precess query");
                    return Result.Failure<ScorecardDto, Error>(Errors.GetUnexpectedError("Unable to precess query"));
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create scorecard");
                return Result.Failure<ScorecardDto, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }

    }


    record LocalMatchDto(int Id, int HomeTeamId, string HomeTeamName, int AwayTeamId, string AwayTeamName,
        string MatchDesignator, string MatchTitle,
        string Location, int LocationId, string MatchDate, string SeriesDate, string ResultString, int BallsPerOver,
        bool DayNight, int? TossTeamId, string Toss, int? WhoWonId, string WhoWon, int? WhoLostId, string WhoLost,
        int VictoryType, string MatchType);

    record LocalInningsDto(int HomeTeamId, string HomeTeamName, int AwayTeamId, string AwayTeamName, int InningsNumber,
        int InningsOrder, int Total, int Wickets,
        ulong Complete,
        int Minutes, int Byes, int LegByes, int Wides, int Noballs, int Penalty, int Extras, string Overs,
        ulong Declared);

    record LocalFowDto(int MatchId, int InningsOrder, int Wicket, int PlayerId, string PlayerName, int? CurrentScore,
        string overs);


    record LocalBattingDetailDto(int PlayerId, string PlayerName, int InningsNumber, int InningsOrder
        , string Dismissal, int DismissalType, int BowlerId, string BowlerName, int FielderId, string FielderName
        , int? Score, int Position, ulong NotOut
        , int? Balls, int? Minutes, int? Fours, int? Sixes, ulong Captain, ulong WicketKeeper);
    
    record LocalBowlingDetailDto(int PlayerId, string FullName, int InningsOrder, string Overs, int? Balls, int? Runs,
        int? Maidens,
        int? Wickets, int? Fours,
        int? Sixes, int? Dots, int? Wides, int? NoBalls, ulong Captain);
  
}