using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsStatsWeb.Models;
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
    public class TeamRecordsController : BaseApiController
    {

        private readonly
            Dictionary<string, Func<BattingBowlingFieldingModel,
                Task<Result<IReadOnlyList<TeamRecordDetailsDto>, Error>>>> _teamRecordDetailsFuncs =
                new();

        private readonly
            Dictionary<string, Func<BattingBowlingFieldingModel,
                Task<Result<IReadOnlyList<MatchRecordDetailsDto>, Error>>>> _matchRecordDetailsFuncs =
                new();
        
        private readonly
            Dictionary<string, Func<BattingBowlingFieldingModel,
                Task<Result<IReadOnlyList<MatchResultDto>, Error>>>> _matchResultFuncs =
                new();

        private readonly
            Dictionary<string, Func<BattingBowlingFieldingModel,
                Task<Result<IReadOnlyList<TeamExtrasDetailsDto>, Error>>>> _teamExtraDetailsFuncs =
                new();

        private readonly
            Dictionary<string, Func<BattingBowlingFieldingModel,
                Task<Result<IReadOnlyList<InningsExtrasDetailsDto>, Error>>>> _inningsExtraDetailsFuncs =
                new();

        public TeamRecordsController(ITeamsService teamsService,
            IMatchesService matchesService,
            ICountriesService countriesService,
            IValidation validation,
            ILogger<TeamRecordsController> logger) : base(
            teamsService, countriesService, validation, logger)
        {

            _teamRecordDetailsFuncs.Add("GetTeamRecords", TeamsService.GetTeamRecords);
            _teamRecordDetailsFuncs.Add("GetTeamSeriesRecords", TeamsService.GetTeamSeriesRecords);
            _teamRecordDetailsFuncs.Add("GetTeamGroundRecords", TeamsService.GetTeamGroundRecords);
            _teamRecordDetailsFuncs.Add("GetTeamHostCountryRecords", TeamsService.GetTeamHostCountryRecords);
            _teamRecordDetailsFuncs.Add("GetTeamOppositionRecords", TeamsService.GetTeamOppositionRecords);
            _teamRecordDetailsFuncs.Add("GetTeamByYearRecords", TeamsService.GetTeamByYearRecords);
            _teamRecordDetailsFuncs.Add("GetTeamBySeasonRecords", TeamsService.GetTeamBySeasonRecords);
            
            _inningsExtraDetailsFuncs.Add("GetTeamInningsExtrasRecords", TeamsService.GetTeamInningsExtrasRecords);
            
            _teamExtraDetailsFuncs.Add("GetTeamOverallExtrasRecords", TeamsService.GetTeamOverallExtrasRecords);
            
            _matchRecordDetailsFuncs.Add("GetHighestInningsForTeam", matchesService.GetHighestInningsForTeam);
            _matchRecordDetailsFuncs.Add("GetMatchTotals", matchesService.GetMatchTotals);
            
            _matchResultFuncs.Add("GetMatchResults", matchesService.GetMatchResults);
        }


        [HttpGet("overall/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetOverallResults(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _teamRecordDetailsFuncs["GetTeamRecords"]);
        }

        [HttpGet("inningsbyinings/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetInningsByInnings(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _matchRecordDetailsFuncs["GetHighestInningsForTeam"]);
        }

        [HttpGet("highesttotals/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetHighestTotals(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _matchRecordDetailsFuncs["GetMatchTotals"]);
        }

        [HttpGet("matchresults/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetMatchResults(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _matchResultFuncs["GetMatchResults"]);
        }

        [HttpGet("series/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetSeriesResults(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _teamRecordDetailsFuncs["GetTeamSeriesRecords"]);
        }

        [HttpGet("grounds/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetGroundResults(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _teamRecordDetailsFuncs["GetTeamGroundRecords"]);
        }

        [HttpGet("host/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetHostResults(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _teamRecordDetailsFuncs["GetTeamHostCountryRecords"]);
        }

        [HttpGet("opposition/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetOppositionResults(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _teamRecordDetailsFuncs["GetTeamOppositionRecords"]);
        }

        [HttpGet("year/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetYearResults(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _teamRecordDetailsFuncs["GetTeamByYearRecords"]);
        }

        [HttpGet("season/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetSeasonResults(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _teamRecordDetailsFuncs["GetTeamBySeasonRecords"]);
        }

        [HttpGet("extras/overall/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetExtrasOverall(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _teamExtraDetailsFuncs["GetTeamOverallExtrasRecords"]);
        }

        [HttpGet("extras/innings/{matchType}/{teamId}/{opponentsId}")]
        public async Task<IActionResult> GetExtrasInnings(
            [FromRoute] ApiRecordInputModel recordInputModel)
        {
            return await Handle(recordInputModel, _inningsExtraDetailsFuncs["GetTeamInningsExtrasRecords"]);
        }
    }
}