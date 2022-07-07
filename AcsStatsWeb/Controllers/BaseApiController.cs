using System.Diagnostics;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsStatsAngular.Models.api;
using AcsTypes.Error;
using AcsTypes.Json;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Services;
using MatchType = AcsTypes.Types.MatchType;

namespace AcsStatsAngular.Controllers;

public class BaseApiController : ControllerBase
{
  private readonly ICountriesService _countriesService;
  private readonly ILogger _logger;
  protected readonly IValidation Validation;
  protected readonly ITeamsService TeamsService;

  public BaseApiController()
  {
  }

  protected BaseApiController(
    ILogger logger) : this(null, null, null, logger)
  {
  }

  protected BaseApiController(ITeamsService teamsService,
    ILogger logger) : this(teamsService, null, null, logger)
  {
  }

  protected BaseApiController(ITeamsService teamsService, ICountriesService countriesService, IValidation validation,
    ILogger logger)
  {
    TeamsService = teamsService;
    _countriesService = countriesService;
    Validation = validation;
    _logger = logger;
  }
  

  // ReSharper disable once InconsistentNaming
  private static T InitializeSharedServiceModel<T>(ApiRecordInputModel recordInputModel)
    where T : SharedModel, new()
  {
   return new T
    {
      MatchType = (MatchType)recordInputModel.MatchType,
      TeamId = (TeamId)recordInputModel.TeamId,
      OpponentsId = (TeamId)recordInputModel.OpponentsId,
      GroundId = (GroundId)recordInputModel.GroundId,
      HostCountryId = (CountryId)recordInputModel.HostCountryId,
      HomeVenue = VenueId.Create(recordInputModel.Venue & 1).Value,
      AwayVenue = VenueId.Create(recordInputModel.Venue & 2).Value,
      NeutralVenue = VenueId.Create(recordInputModel.Venue & 4).Value,
      Limit = (ScoreLimit)recordInputModel.Limit,
      SortDirection = recordInputModel.SortDirection,
      SortOrder = recordInputModel.SortOrder,
      Season = recordInputModel.Season,
      StartDateEpoch = recordInputModel.StartDate,
      EndDateEpoch = recordInputModel.EndDate,
      MatchResult = (MatchResult)recordInputModel.MatchResult,
      StartRow = recordInputModel.StartRow,
      EndRow = recordInputModel.StartRow + recordInputModel.PageSize,
    };
  }

  // ReSharper disable once InconsistentNaming
  protected async Task<IActionResult> Handle<T, TSharedModel>(ApiRecordInputModel recordInputModel
    , Func<TSharedModel, Task<Result<IReadOnlyList<T>, Error>>> action) where TSharedModel : SharedModel, new()
  {
    return await Validate(recordInputModel)
      .Map(InitializeSharedServiceModel<TSharedModel>)
      .Bind(action)
      .MapError(t =>
      {
        _logger.LogDebug(t.Message);
        return t;
      })
      .Finally(result => result.IsSuccess ? CreateOkResult(result.Value) : new BadRequestResult());
  }

  protected async Task<IActionResult> HandleEx<T, TSharedModel>(ApiRecordInputModel recordInputModel
    , Func<TSharedModel, Task<Result<SqlResultEnvelope<IReadOnlyList<T>>, Error>>> action) where TSharedModel : SharedModel, new()
  {
    return await Validate(recordInputModel)
      .Map(InitializeSharedServiceModel<TSharedModel>)
      .Bind(action)
      .MapError(t =>
      {
        _logger.LogDebug(t.Message);
        return t;
      })
      .Finally(result => result.IsSuccess ? CreateOkResultEx(result.Value) : new BadRequestResult());
  }
  // ReSharper disable once InconsistentNaming
  private IActionResult CreateOkResult<T>(IReadOnlyList<T> resultValue)
  {
    return Ok(resultValue);
  }

  private IActionResult CreateOkResultEx<T>(SqlResultEnvelope<IReadOnlyList<T>> resultValue)
  {
    return Ok(resultValue);
  }

  // ReSharper disable once InconsistentNaming
  private Result<ApiRecordInputModel, Error> Validate(ApiRecordInputModel recordInputModel)
  {
    var ids = Validation.ValidateTeamIds(recordInputModel.TeamId, recordInputModel.OpponentsId)
      .Match(r => Result.Success(), e => Result.Failure(e.Message));

    var mr = Validation.ValidateMatchResult(recordInputModel.MatchResult)
      .Match(r => Result.Success(), e => Result.Failure(e.Message));

    var limit = Validation.ValidateLimit(recordInputModel.Limit)
      .Match(r => Result.Success(), e => Result.Failure(e.Message));

    var countryId = Validation.ValidateCountry(recordInputModel.HostCountryId)
      .Match(r => Result.Success(), e => Result.Failure(e.Message));

    var matchType = Validation.ValidateMatchType(recordInputModel.MatchType)
      .Match(r => Result.Success(), e => Result.Failure(e.Message));

    var nVenueId = Validation.ValidateVenueId(recordInputModel.Venue)
      .Match(r => Result.Success(), e => Result.Failure(e.Message));

    var vDates = Validation.ValidateEpochDates(recordInputModel.StartDate, recordInputModel.EndDate)
      .Match(r => Result.Success(), e => Result.Failure(e.Message));


    var r = Result.Combine(ids, mr, limit, countryId, matchType, nVenueId, vDates);

    return r.IsSuccess
      ? Result.Success<ApiRecordInputModel, Error>(recordInputModel)
      : Result.Failure<ApiRecordInputModel, Error>(r.Error);
  }

  protected new IActionResult Ok()
  {
    return base.Ok(Envelope.Ok());
  }

  protected IActionResult Ok<T>(T result)
  {
    return base.Ok(Envelope.Ok(result));
  }

  protected IActionResult Error(Error error)
  {
    var combinedError = error as CombinedError;

    string errorMessage = "";

    if (combinedError == null)
      errorMessage = error.Message;
    else
      errorMessage = string.Join(", ", combinedError.Errors.Map(e => e.Message));

    return BadRequest(Envelope.Error(errorMessage));
  }

  protected IActionResult Error(string errorMessage)
  {
    return BadRequest(Envelope.Error(errorMessage));
  }

  protected IActionResult FromResult(Result result)
  {
    return result.IsSuccess ? Ok() : Error(result.Error);
  }
}