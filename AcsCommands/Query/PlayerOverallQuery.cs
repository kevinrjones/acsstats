using AcsDto.Dtos;
using AcsRepository.Util;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class PlayerOverallQuery : IRequest<Result<IReadOnlyList<PlayerOverallDto>, Error>>
{
  private int PlayerId { get; }

  public PlayerOverallQuery(int playerId)
  {
    PlayerId = playerId;
  }

  internal class PlayerOverallQueryHandler
    : IRequestHandler<PlayerOverallQuery, Result<IReadOnlyList<PlayerOverallDto>, Error>>
  {
    private readonly QueriesConnectionString _queriesConnectionString;
    private readonly ILogger<PlayerOverallQueryHandler> _logger;

    public PlayerOverallQueryHandler(QueriesConnectionString queriesConnectionString,
      ILogger<PlayerOverallQueryHandler> logger)
    {
      _queriesConnectionString = queriesConnectionString;
      _logger = logger;
    }

    public async Task<Result<IReadOnlyList<PlayerOverallDto>, Error>> Handle(PlayerOverallQuery request,
      CancellationToken cancellationToken)
    {
      string battingsql =
        @"select BattingDetails.MatchType,
               BattingDetails.TeamId,
               T.Name   as                  Team,
               count(*) as                  matches,
               sum(coalesce(score, 0))      runs,
               count(case when dismissaltype != 11 and dismissaltype != 14 then 1 end) innings,
               sum(coalesce(notout, 0))     notouts,
               sum(coalesce(balls, 0))      balls,
               sum(coalesce(fours, 0))      fours,
               sum(coalesce(sixes, 0))      sixes,
               sum(hundred)                 hundreds,
               sum(fifty)                   fifties
        from BattingDetails
                 join Teams T on BattingDetails.TeamId = T.Id
        where PlayerId = @PlayerId
        group by MatchType, TeamId";

      string bowlingsql =
        @"select BowlingDetails.matchtype,
                 BowlingDetails.TeamId,
                 count(*) as               matches,
                 sum(coalesce(balls, 0))   bowlingballs,
                 sum(coalesce(Runs, 0))    bowlingruns,
                 sum(coalesce(Maidens, 0)) maidens,
                 sum(coalesce(Wickets, 0)) wickets,
                 sum(coalesce(fours, 0))   bowlingfours,
                 sum(coalesce(sixes, 0))   bowlingsixes,
                 sum(coalesce(Wides, 0))   wides,
                 sum(coalesce(NoBalls, 0)) noballs
          from BowlingDetails
         where PlayerId = @PlayerId
         group by MatchType, TeamId";

      try
      {
        await using var connection = new MySqlConnection(_queriesConnectionString.Value);
        var resultBatting = connection.Query<PlayerBattingOverallDto>(battingsql, new
        {
          request.PlayerId,
        }).ToList();

        var resultBowling = connection.Query<PlayerBowlingOverallDto>(bowlingsql, new
        {
          request.PlayerId,
        }).ToList();

        var playerOverallDto = MergeOverall(resultBatting, resultBowling);

        return Result.Success<IReadOnlyList<PlayerOverallDto>, Error>(playerOverallDto.AsReadOnly());
      }
      catch (Exception e)
      {
        _logger.LogCritical(e, "Unable to process this request: {Tournament}", request.PlayerId);
        return Result.Failure<IReadOnlyList<PlayerOverallDto>, Error>(Errors.GetUnexpectedError(e.Message));
      }
    }

    private List<PlayerOverallDto> MergeOverall(List<PlayerBattingOverallDto> resultBatting,
      List<PlayerBowlingOverallDto> resultBowling)
    {
      List<PlayerOverallDto> result = new List<PlayerOverallDto>();
      foreach (var battingOverallDto in resultBatting)
      {
        PlayerBowlingOverallDto? bowling = resultBowling
          .Filter(b => b.TeamId == battingOverallDto.TeamId && b.MatchType == battingOverallDto.MatchType)
          .FirstOrDefault();

        if (bowling == null)
        {
          bowling = new PlayerBowlingOverallDto(battingOverallDto.MatchType, battingOverallDto.TeamId,
            battingOverallDto.Matches, null, null, null,
            null, null, null, null, null);
        }


        var playerOverallDto = new PlayerOverallDto(battingOverallDto.Team,
          battingOverallDto.MatchType,
          battingOverallDto.TeamId,
          battingOverallDto.Matches, battingOverallDto.Runs,  battingOverallDto.innings, battingOverallDto.Notouts, battingOverallDto.Balls,
          battingOverallDto.Fours, battingOverallDto.Sixes, battingOverallDto.Hundreds, battingOverallDto.Fifties,
          bowling.BowlingBalls,
          bowling.BowlingRuns, bowling.Maidens, bowling.Wickets, bowling.BowlingFours, bowling.BowlingSixes,
          bowling.Wides, bowling.NoBalls);
        result.Add(playerOverallDto);
      }
      
      return result;
    }
  }
}