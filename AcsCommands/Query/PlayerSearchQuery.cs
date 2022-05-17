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

public class PlayerSearchQuery : IRequest<Result<IReadOnlyList<PlayerListDto>, Error>>
{
  private string OtherNamePart { get; }
  private string SortNamePart { get; }
  public string TeamName { get; }
  public string DebutDate { get; }
  public string ActiveUntilDate { get; }

  public bool ExactNameMatch { get; set; }

  public PlayerSearchQuery(string? otherNamePart
    , string? sortNamePart
    , string? team
    , EpochDateType debutDate
    , EpochDateType activeUntilDate
    , bool exactNameMatch)
  {
    OtherNamePart = otherNamePart ?? "";
    TeamName = team ?? "";
    SortNamePart = sortNamePart ?? "";
    ExactNameMatch = exactNameMatch;
    DebutDate = debutDate.EpochDate.ToString();
    ActiveUntilDate = activeUntilDate.EpochDate.ToString();
  }


  internal class PlayerSearchQueryHandler
    : IRequestHandler<PlayerSearchQuery, Result<IReadOnlyList<PlayerListDto>, Error>>
  {
    private readonly QueriesConnectionString _queriesConnectionString;
    private readonly ILogger<PlayerSearchQueryHandler> _logger;

    public PlayerSearchQueryHandler(QueriesConnectionString queriesConnectionString,
      ILogger<PlayerSearchQueryHandler> logger)
    {
      _queriesConnectionString = queriesConnectionString;
      _logger = logger;
    }

    public async Task<Result<IReadOnlyList<PlayerListDto>, Error>> Handle(PlayerSearchQuery request,
      CancellationToken cancellationToken)
    {
      string sql =
        @"select P.id, P.FullName, teams.teams, P.Debut, P.ActiveUntil
            from players P
           join
       (select pt.playerid, GROUP_CONCAT(t.name SEPARATOR ', ') as teams
        from playersteams pt
                 join teams t on pt.teamid = t.id
        group by pt.playerid) as teams on teams.playerid = P.Id
        where ((@sortnamepart = '' or SortNamePart like @sortnamepart)
          and (@othernamepart = '' or OtherNamePart like @othernamepart)
                   or(@fullname = '' or SortNamePart like @fullname))
        and (@teamname = '' or teams.teams like @teamname)
        and P.Debut >= @debutdate
        and P.ActiveUntil <= @activeuntildate";

      try
      {
        var likeOtherNamePart =
          request.ExactNameMatch ? request.OtherNamePart : $"%{request.OtherNamePart}%";
        var likeSortNamePart =
          request.ExactNameMatch ? request.SortNamePart : $"%{request.SortNamePart}%";
        var fullName =
          request.ExactNameMatch ? $"{request.OtherNamePart} {request.SortNamePart}" : $"%{request.OtherNamePart} {request.SortNamePart}%";


        await using var connection = new MySqlConnection(_queriesConnectionString.Value);
        var result = connection.Query<LocalPlayerListDto>(sql, new
        {
          SortNamePart = likeSortNamePart,
          OtherNamePart = likeOtherNamePart,
          FullName = fullName,
          request.TeamName,
          request.DebutDate,
          request.ActiveUntilDate,
        }).ToList();

        return Result.Success<IReadOnlyList<PlayerListDto>, Error>(result.Map(ml =>
        {
          var debut = EpochDateType.Create(ml.Debut);
          var activeUntil = EpochDateType.Create(ml.ActiveUntil);

          if (debut.IsFailure || activeUntil.IsFailure)
          {
            throw new Exception("Unable to parse dates");
          }

          return new PlayerListDto(ml.Id, ml.FullName, ml.Teams, ((DateTime)debut.Value).ToLongDateString(),
            ((DateTime)activeUntil.Value).ToLongDateString());
        }).ToList());
      }
      catch (Exception e)
      {
        _logger.LogCritical(e,
          "Unable to process this request: {OtherNamePart}, {SortNamePart}, {TeamName}, {DebutDate}, {ActiveUntilDate}",
          request.OtherNamePart, request.SortNamePart,
          request.TeamName, request.DebutDate, request.ActiveUntilDate);
        return Result.Failure<IReadOnlyList<PlayerListDto>, Error>(Errors.GetUnexpectedError(e.Message));
      }
    }
  }

  public record LocalPlayerListDto(int Id, string FullName, string Teams, long Debut,
    long ActiveUntil);
}