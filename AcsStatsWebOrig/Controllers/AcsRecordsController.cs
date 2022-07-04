using System;
using System.Threading.Tasks;
using AcsDto.Models;
using AcsStatsWeb.Models;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.Models;

namespace AcsStatsWeb.Controllers
{
  public abstract class AcsRecordsController : Controller
  {
    private readonly ITeamsService _teamsService;
    private readonly ICountriesService _countriesService;
    private readonly IValidation _validation;
    protected readonly ILogger _logger;

    protected AcsRecordsController(ITeamsService teamsService, ICountriesService countriesService,
      IValidation validation, ILogger logger)
    {
      _teamsService = teamsService;
      _countriesService = countriesService;
      _validation = validation;
      _logger = logger;
    }


    // ReSharper disable once InconsistentNaming
    protected async Task<Result<T, Error>> InitializeResultModel<T>(RecordInputModel recordInputModel) where T : ResultsModel, new()
    {
      var resultsModel = new T
      {
        HomeVenue =  recordInputModel.HomeVenue,
        AwayVenue = recordInputModel.AwayVenue,
        NeutralVenue = recordInputModel.NeutralVenue,
        Limit = recordInputModel.Limit,
        StartDate = recordInputModel.StartDate,
        EndDate = recordInputModel.EndDate,
        Season = recordInputModel.Season,
        Format = recordInputModel.Format
      };

      resultsModel.SetOppositeSortDirection(recordInputModel.SortDirection);


      return await (await _teamsService.GetTeam((TeamId)recordInputModel.TeamId))
        .Tap(t =>
        {
          resultsModel.Team = t.Name;
          resultsModel.TeamId = recordInputModel.TeamId;
        })
        .Bind(async (_) => await _teamsService.GetTeam((TeamId)recordInputModel.OpponentsId))
        .Tap(t =>
        {
          resultsModel.Opponents = t.Name;
          resultsModel.OpponentsId = recordInputModel.OpponentsId;
        })
        .Bind(async (_) => await _countriesService.getCountryFromId((CountryId)recordInputModel.HostCountryId))
        .Tap(g =>
        {
          resultsModel.HostCountryId = recordInputModel.HostCountryId;
          resultsModel.HostCountryName = g.Name;
        }).Bind((_) => Result.Success<T, Error>(resultsModel));
    }

    protected void SetShowTeamsInLists(ResultsModel resultsModel, TeamId teamId, TeamId opponentsId)
    {
      if (teamId.Value == 0 && opponentsId.Value == 0)
      {
        resultsModel.ShowTeamInList = true;
        resultsModel.ShowOpponentsInList = true;
      }
      else if (teamId.Value == 0 && opponentsId.Value != 0)
      {
        resultsModel.ShowTeamInList = true;
        resultsModel.ShowOpponentsInList = false;
      }

      if (teamId.Value != 0 && opponentsId.Value == 0)
      {
        resultsModel.ShowTeamInList = false;
        resultsModel.ShowOpponentsInList = true;
      }

      if (teamId.Value != 0 && opponentsId.Value != 0)
      {
        resultsModel.ShowTeamInList = false;
        resultsModel.ShowOpponentsInList = false;
      }
    }

    protected RequestDates GetEpochDates(string maybeStartDate, string maybeEndDate)
    {
      return _validation.ValidateEpochDates(maybeStartDate, maybeEndDate).Value;
    }

    // ReSharper disable once InconsistentNaming
    protected static T InitializeSharedServiceModel<T>(RecordInputModel recordInputModel,
      RequestDates requestDates, MatchResult matchResult)
      where T : SharedModel, new()
    {
      return new T
      {
        MatchType = (MatchType)recordInputModel.MatchType,
        TeamId = (TeamId)recordInputModel.TeamId,
        OpponentsId = (TeamId)recordInputModel.OpponentsId,
        GroundId = (GroundId)recordInputModel.GroundId,
        HostCountryId = (CountryId)recordInputModel.HostCountryId,
        HomeVenue = (VenueId)recordInputModel.HomeVenue,
        AwayVenue = (VenueId)recordInputModel.AwayVenue,
        NeutralVenue = (VenueId)recordInputModel.NeutralVenue,
        Limit = (ScoreLimit)recordInputModel.Limit,
        SortDirection = recordInputModel.SortDirection,
        SortOrder = recordInputModel.SortOrder,
        Season = recordInputModel.Season,
        StartDateEpoch = requestDates.StartDateEpoch,
        EndDateEpoch = requestDates.EndDateEpoch,
        MatchResult = matchResult,
      };
    }


    protected Result Validate(RecordInputModel recordInputModel)
    {
      var ids = _validation.ValidateTeamIds(recordInputModel.TeamId, recordInputModel.OpponentsId)
        .Match(r => Result.Success(), e => Result.Failure(e.Message));
      
      var mr =_validation.ValidateMatchResult(recordInputModel.MatchResult)
        .Match(r => Result.Success(), e => Result.Failure(e.Message));

      var limit = _validation.ValidateLimit(recordInputModel.Limit)
        .Match(r => Result.Success(), e => Result.Failure(e.Message));
      
      var countryId = _validation.ValidateCountry(recordInputModel.HostCountryId)
        .Match(r => Result.Success(), e => Result.Failure(e.Message));

      var matchType = _validation.ValidateMatchType(recordInputModel.MatchType)
        .Match(r => Result.Success(), e => Result.Failure(e.Message));

      var nVenueId = _validation.ValidateVenueId(recordInputModel.NeutralVenue)
        .Match(r => Result.Success(), e => Result.Failure(e.Message));
      var hVenueId = _validation.ValidateVenueId(recordInputModel.HomeVenue)
        .Match(r => Result.Success(), e => Result.Failure(e.Message));
      var aVenueId = _validation.ValidateVenueId(recordInputModel.AwayVenue)
        .Match(r => Result.Success(), e => Result.Failure(e.Message));

      var vDates = _validation.ValidateEpochDates(recordInputModel.StartDate, recordInputModel.EndDate)
        .Match(r => Result.Success(), e => Result.Failure(e.Message));

      return Result.Combine(ids, mr, limit, countryId,matchType, nVenueId, hVenueId, aVenueId, vDates);
    }

  }
}