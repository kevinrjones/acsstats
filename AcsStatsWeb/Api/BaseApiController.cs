using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Models;
using AcsStatsWeb.Models.api;
using AcsTypes.Error;
using AcsTypes.Json;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.Models;

namespace AcsStatsWeb.Api
{
    public class BaseApiController : ControllerBase
    {
        protected readonly ITeamsService TeamsService;
        private readonly ICountriesService _countriesService;
        private readonly ILogger _logger;
        private readonly IValidation _validation;

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
            _validation = validation;
            _logger = logger;
        }

        // ReSharper disable once InconsistentNaming
        protected async Task<Result<T, Error>> InitializeResultModel<T>(ApiRecordInputModel recordInputModel)
            where T : ApiResultsModel, new()
        {
            var resultsModel = new T
            {
                HomeVenue = recordInputModel.Venue & 1,
                AwayVenue =  recordInputModel.Venue & 2,
                NeutralVenue = recordInputModel.Venue & 4,
                StartDate = recordInputModel.StartDate,
                EndDate = recordInputModel.EndDate,
                Season = recordInputModel.Season,
            };


            return  await (await TeamsService.GetTeam((TeamId)recordInputModel.TeamId))
                .Tap(t =>
                {
                    resultsModel.Team = t.Name;
                    resultsModel.TeamId = recordInputModel.TeamId;
                })
                .Bind(async (_) => await TeamsService.GetTeam((TeamId)recordInputModel.OpponentsId))
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
            };
        }
        
        // ReSharper disable once InconsistentNaming
        protected async Task<IActionResult> Handle<T, TSharedModel>(ApiRecordInputModel recordInputModel
            , Func<TSharedModel, Task<Result<IReadOnlyList<T>, Error>>> action) where TSharedModel: SharedModel, new()
        {
            return await Validate(recordInputModel)
                .Map(InitializeSharedServiceModel<TSharedModel>)
                .Bind(action)
                .MapError(t =>
                {
                    _logger.LogDebug(t.Message);
                    return t;
                })
                .Finally( result => result.IsSuccess ? CreateOkResult(result.Value) : new BadRequestResult());
        }

        // ReSharper disable once InconsistentNaming
        private IActionResult CreateOkResult<T>(IReadOnlyList<T> resultValue)
        {
            return Ok(resultValue);
        }

        // ReSharper disable once InconsistentNaming
        private Result<ApiRecordInputModel, Error> Validate(ApiRecordInputModel recordInputModel)
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

            var nVenueId = _validation.ValidateVenueId(recordInputModel.Venue)
                .Match(r => Result.Success(), e => Result.Failure(e.Message));

            var vDates = _validation.ValidateEpochDates(recordInputModel.StartDate, recordInputModel.EndDate)
                .Match(r => Result.Success(), e => Result.Failure(e.Message));

            
            var r = Result.Combine(ids, mr, limit, countryId,matchType, nVenueId, vDates);
            
            return r.IsSuccess ? Result.Success<ApiRecordInputModel, Error>(recordInputModel) : Result.Failure<ApiRecordInputModel, Error>(r.Error);
        }
        
        protected new IActionResult Ok()
        {
            return base.Ok(Envelope.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
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
}