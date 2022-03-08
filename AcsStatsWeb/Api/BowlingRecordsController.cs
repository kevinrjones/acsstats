using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcsStatsWeb.Models.api;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.Models;

namespace AcsStatsWeb.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BowlingRecordsController : BaseApiController
    {
        private readonly
            Dictionary<string, Func<BattingBowlingFieldingModel,
                Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>>>> _careerRecordDetailsServiceFuncs =
                new();

        private readonly
            Dictionary<string, Func<BattingBowlingFieldingModel,
                Task<Result<IReadOnlyList<IndividualBowlingDetails>, Error>>>> _individualBowlingDetailsServiceFuncs =
                new();

        public BowlingRecordsController(ITeamsService teamsService,
            IPlayersService playerService,
            ICountriesService countriesService,
            IValidation validation,
            ILogger<BowlingRecordsController> logger) : base(
            teamsService, countriesService, validation, logger)
        {
            _careerRecordDetailsServiceFuncs.Add("GetBowlingCareerRecords", playerService.GetBowlingCareerRecords);
            _careerRecordDetailsServiceFuncs.Add("GetBowlingIndividualSeries", playerService.GetBowlingIndividualSeries);
            _careerRecordDetailsServiceFuncs.Add("GetBowlingIndividualGrounds", playerService.GetBowlingIndividualGrounds);
            _careerRecordDetailsServiceFuncs.Add("GetBowlingIndividualHost", playerService.GetBowlingIndividualHost);
            _careerRecordDetailsServiceFuncs.Add("GetBowlingIndividualOpponents", playerService.GetBowlingIndividualOpponents);
            _careerRecordDetailsServiceFuncs.Add("GetBowlingIndividualYear", playerService.GetBowlingIndividualYear);
            _careerRecordDetailsServiceFuncs.Add("GetBowlingIndividualSeason", playerService.GetBowlingIndividualSeason);

            _individualBowlingDetailsServiceFuncs.Add("GetBowlingIndividualInnings", playerService.GetBowlingIndividualInnings);
            _individualBowlingDetailsServiceFuncs.Add("GetBowlingIndividualMatches", playerService.GetBowlingIndividualMatches);
        }


        [HttpGet("overall/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetOverallResults(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetBowlingCareerRecords"]);
        }

        [HttpGet("inningsbyinnings/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetInningsByInnings(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _individualBowlingDetailsServiceFuncs["GetBowlingIndividualInnings"]);
        }

        [HttpGet("match/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetMatchDetails(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _individualBowlingDetailsServiceFuncs["GetBowlingIndividualMatches"]);
        }

        [HttpGet("series/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetSeriesRecords(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetBowlingIndividualSeries"]);
        }

        [HttpGet("grounds/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetGroundRecords(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetBowlingIndividualGrounds"]);
        }

        [HttpGet("host/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetHostRecords(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetBowlingIndividualHost"]);
        }

        [HttpGet("opposition/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetOppositionRecords(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetBowlingIndividualOpponents"]);
        }

        [HttpGet("year/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetYearRecords(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetBowlingIndividualYear"]);
        }

        [HttpGet("season/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetSeasonRecords(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetBowlingIndividualSeason"]);
        }
    }
}