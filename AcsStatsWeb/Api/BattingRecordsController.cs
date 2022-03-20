using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsStatsWeb.Dtos;
using AcsStatsWeb.Models;
using AcsStatsWeb.Models.api;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.Models;

namespace AcsStatsWeb.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattingRecordsController : BaseApiController
    {
        private readonly IPlayersService _playerService;
        private readonly ILogger<BattingRecordsController> _logger;

        private readonly
            Dictionary<string, Func<BattingBowlingFieldingModel,
                Task<Result<IReadOnlyList<BattingCareerRecordDto>, Error>>>> _careerRecordDetailsServiceFuncs =
                new();

        private readonly
            Dictionary<string, Func<BattingBowlingFieldingModel,
                Task<Result<IReadOnlyList<IndividualBattingDetailsDto>, Error>>>>
            _individualBattingDetailsServiceFuncs =
                new();

        public BattingRecordsController(ITeamsService teamsService,
            IPlayersService playerService,
            ICountriesService countriesService,
            IValidation validation,
            ILogger<BattingRecordsController> logger) : base(
            teamsService, countriesService, validation, logger)
        {
            _playerService = playerService;
            _logger = logger;

            _careerRecordDetailsServiceFuncs.Add("GetBattingCareerRecords", playerService.GetBattingCareerRecords);
            _careerRecordDetailsServiceFuncs.Add("GetBattingIndividualSeries",
                playerService.GetBattingIndividualSeries);
            _careerRecordDetailsServiceFuncs.Add("GetBattingIndividualGrounds",
                playerService.GetBattingIndividualGrounds);
            _careerRecordDetailsServiceFuncs.Add("GetBattingIndividualHost", playerService.GetBattingIndividualHost);
            _careerRecordDetailsServiceFuncs.Add("GetBattingIndividualOpponents",
                playerService.GetBattingIndividualOpponents);
            _careerRecordDetailsServiceFuncs.Add("GetBattingIndividualYear", playerService.GetBattingIndividualYear);
            _careerRecordDetailsServiceFuncs.Add("GetBattingIndividualSeason",
                playerService.GetBattingIndividualSeason);

            _individualBattingDetailsServiceFuncs.Add("GetBattingIndividualInnings",
                playerService.GetBattingIndividualInnings);
            _individualBattingDetailsServiceFuncs.Add("GetBattingIndividualMatches",
                playerService.GetBattingIndividualMatches);
        }


        [HttpGet("overall/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetOverallResults(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetBattingCareerRecords"]);
        }

        [HttpGet("inningsbyinnings/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetInningsByInnings(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _individualBattingDetailsServiceFuncs["GetBattingIndividualInnings"]);
        }

        [HttpGet("match/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetMatchDetails(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _individualBattingDetailsServiceFuncs["GetBattingIndividualMatches"]);
        }

        [HttpGet("series/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GerSeriesRecords(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetBattingIndividualSeries"]);
        }

        [HttpGet("grounds/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetGroundRecords(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetBattingIndividualGrounds"]);
        }

        [HttpGet("host/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetHostRecords(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetBattingIndividualHost"]);
        }

        [HttpGet("opposition/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetOppositionRecords(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetBattingIndividualOpponents"]);
        }

        [HttpGet("year/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetYearRecords(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetBattingIndividualYear"]);
        }

        [HttpGet("season/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetSeasonRecords(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetBattingIndividualSeason"]);
        }
    }
}