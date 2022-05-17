using AcsDto.Models;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
// ReSharper disable InconsistentNaming

namespace Services;

public interface IValidation
{
     public Result<TeamId, Error> ValidateTeamId(int id);
     public Result<TeamId, Error> ValidateTeamIds(int teamId, int opponentsId);
     public Result<MatchResult, Error> ValidateMatchResult(int[] matchResult);
     public Result<MatchResult, Error> ValidateMatchResult(int matchResult);
     public Result<ScoreLimit, Error> ValidateLimit(int limit);
     public Result<CountryId, Error> ValidateCountry(int id);
     public Result<MatchType, Error> ValidateMatchType(string type);
     public Result<VenueId, Error> ValidateVenueId(int id);

     public Result<RequestDates, Error> ValidateEpochDates(string startDate, string endDate, string format = "dd MMMM yyyy");
     public Result<RequestDates, Error> ValidateEpochDates(long startDate, long endDate);
     public Result<bool, Error>  ValidateMatchSearchModel(MatchSearchModel matchSearchModel);
     public Result<bool, Error>  ValidatePlayerSearchModel(PlayerSearchModel playerSearchModel);
}
