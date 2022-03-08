using System;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;

namespace Services.AcsServices;

public class Validation : IValidation
{
  public Result<TeamId, Error> ValidateTeamIds(int teamId, int opponentsId)
  {
    var teamIdResult = ValidateTeamId(teamId);
    var opponentsIdResult = ValidateTeamId(opponentsId);

    Result<TeamId, Error> teamComparison;

    if (teamId == opponentsId && teamId != 0)
    {
      teamComparison = Result.Failure<TeamId, Error>("Team Ids cannot be the same");
    }
    else
    {
      teamComparison = Result.Success<TeamId, Error>(TeamId.Create(0).Value);
    }

    // alternative 
    var r1 = Result
      .Combine(teamIdResult, opponentsIdResult, teamComparison)
      .Bind(t => TeamId.Create(0));

    Result<TeamId, Error> result =
      teamIdResult.Bind(_ => opponentsIdResult.Bind(_ => teamComparison.Map(t => t)));

    return result;
  }

  public Result<MatchResult, Error> ValidateMatchResult(int[] matchResult)
  {
    return MatchResult.Create(matchResult);
  }
  public Result<MatchResult, Error> ValidateMatchResult(int matchResult)
  {
    return MatchResult.Create(matchResult);
  }
  public Result<TeamId, Error> ValidateTeamId(int id)
  {
    return TeamId.Create(id);
  }
  
  public Result<ScoreLimit, Error> ValidateLimit(int limit)
  {
    return ScoreLimit.Create(limit);
  }

  public Result<CountryId, Error> ValidateCountry(int id)
  {
    return CountryId.Create(id);
  }

  public Result<MatchType, Error> ValidateMatchType(string type)
  {
    return MatchType.Create(type);
  }

  public Result<VenueId, Error> ValidateVenueId(int id)
  {
    return VenueId.Create(id);
  }

  public Result<RequestDates,Error> ValidateEpochDates(string maybeStartDate, string maybeEndDate)
  {
    if (!DateTime.TryParse(maybeStartDate, out var startDate))
    {
      return Result.Failure<RequestDates, Error>(Errors.InvalidDateError(maybeStartDate));
    }

    if (!DateTime.TryParse(maybeEndDate, out var endDate))
    {
      return Result.Failure<RequestDates, Error>(Errors.InvalidDateError(maybeStartDate));
    }

    if (endDate < startDate)
    {
      return Result.Failure<RequestDates, Error>(Errors.InvalidDateError(maybeStartDate, maybeEndDate));
    }

    return Result.Success<RequestDates, Error>(new RequestDates{StartDateEpoch = (long)(startDate - new DateTime(1970, 1, 1))
      .TotalSeconds, EndDateEpoch = (long)(endDate - new DateTime(1970, 1, 1)).TotalSeconds});
  }

  public Result<RequestDates, Error> ValidateEpochDates(long maybeStartDate, long maybeEndDate)
  {
    if (maybeEndDate < maybeStartDate)
    {
      return Result.Failure<RequestDates, Error>(Errors.InvalidDateError(new DateTime(maybeStartDate).ToString(), new DateTime(maybeEndDate).ToString()));
    }
    
    return Result.Success<RequestDates, Error>(new RequestDates{StartDateEpoch = maybeStartDate, EndDateEpoch = maybeEndDate});

  }
}