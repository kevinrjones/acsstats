using System;
using System.Globalization;
using AcsDto.Models;
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

    public Result<RequestDates, Error> ValidateEpochDates(string maybeStartDate, string maybeEndDate, string format)
    {
        return EpochDateType.Create(maybeStartDate, format)
            .Bind(s => EpochDateType.Create(maybeEndDate, format)
                .Bind(e => Result.Success<RequestDates, Error>(new RequestDates
                {
                    StartDateEpoch = s,
                    EndDateEpoch = e
                })));
    }

    public Result<RequestDates, Error> ValidateEpochDates(long maybeStartDate, long maybeEndDate)
    {
        if (maybeEndDate < maybeStartDate)
        {
            return Result.Failure<RequestDates, Error>(Errors.InvalidDateError(new DateTime(maybeStartDate).ToString(),
                new DateTime(maybeEndDate).ToString()));
        }

        return Result.Success<RequestDates, Error>(new RequestDates
        {
            StartDateEpoch = EpochDateType.Create(maybeStartDate).Value,
            EndDateEpoch = EpochDateType.Create(maybeEndDate).Value
        });
    }

    public Result<bool, Error> ValidateMatchSearchModel(MatchSearchModel matchSearchModel)
    {
        string format = "yyyy-MM-dd";

        var startEpochDate = EpochDateType.Create(matchSearchModel.StartDate, format);
        var endEpochDate = EpochDateType.Create(matchSearchModel.StartDate, format);
        
        var sResult = Result.SuccessIf<MatchSearchModel, Error>(startEpochDate.IsSuccess, matchSearchModel,
            Errors.ModelError("StartDate", "Date is not valid"));
        var eResult = Result.SuccessIf<MatchSearchModel, Error>(endEpochDate.IsSuccess, matchSearchModel,
            Errors.ModelError("EndDate", "Date is not valid"));
        Result<MatchSearchModel, Error> hTeamError = Result.Success<MatchSearchModel, Error>(matchSearchModel);
        Result<MatchSearchModel, Error> aTeamError = Result.Success<MatchSearchModel, Error>(matchSearchModel);

        if (string.IsNullOrEmpty(matchSearchModel.AwayTeam) && string.IsNullOrEmpty(matchSearchModel.HomeTeam))
        {
            hTeamError =
                Result.Failure<MatchSearchModel, Error>(Errors.ModelError("HomeTeam",
                    "Team and opponents cannot be empty"));
            aTeamError =
                Result.Failure<MatchSearchModel, Error>(Errors.ModelError("AwayTeam",
                    "Team and opponents cannot be empty"));
        }

        if (!string.IsNullOrEmpty(matchSearchModel.HomeTeam) && matchSearchModel.HomeTeam.Length < 3)
        {
            hTeamError =
                Result.Failure<MatchSearchModel, Error>(Errors.ModelError("HomeTeam",
                    "Must have at least 3 characters for the search"));
        }

        if (!string.IsNullOrEmpty(matchSearchModel.AwayTeam) && matchSearchModel.AwayTeam.Length < 3)
        {
            aTeamError =
                Result.Failure<MatchSearchModel, Error>(Errors.ModelError("AwayTeam",
                    "Must have at least 3 characters for the search"));
        }

        return Result.Combine(sResult, eResult, hTeamError, aTeamError);
    }
}