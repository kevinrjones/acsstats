using System.Text.Json;
using AcsCommands.Util;
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
  private int MatchId { get; }


  public ScorecardQuery(int matchId)
  {
    MatchId = matchId;
  }

  internal class ScorecardQueryHandler
    : IRequestHandler<ScorecardQuery, Result<ScorecardDto, Error>>
  {
    
    private readonly QueriesConnectionString _queriesConnectionString;
    private readonly ILogger<ScorecardQueryHandler> _logger;

    private string sql =
      @"select MatchId, Card
               from Scorecards 
               where MatchId =@MatchId";

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
      await using var connection = new MySqlConnection(_queriesConnectionString.Value);
      var matchData = connection.Query<LocalScorecardDto>(sql, new
      {
        MatchId = request.MatchId
      }).First();

      var options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
      var scorecard = JsonSerializer.Deserialize<ScorecardDto>(matchData.Card, options);

      return Result.Success<ScorecardDto, Error>(scorecard);
    }

  }
  
  record LocalScorecardDto(int MatchId, string Card);
  
  
}