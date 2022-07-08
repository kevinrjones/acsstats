using System.Diagnostics;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsStatsAngular.Models.api;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace AcsStatsAngular.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BattingRecordsController : BaseApiController
{
    private readonly
        Dictionary<string, Func<BattingBowlingFieldingModel,
            Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>>>> _careerRecordDetailsServiceFuncs =
            new();

    private readonly
        Dictionary<string, Func<BattingBowlingFieldingModel,
            Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>>>> _careerRecordDetailsServiceFuncsEx =
            new();


    private readonly
        Dictionary<string, Func<BattingBowlingFieldingModel,
            Task<Result<SqlResultEnvelope<IReadOnlyList<IndividualBattingDetailsDto>>, Error>>>>
        _individualBattingDetailsServiceFuncs =
            new();

    private readonly ILogger<BattingRecordsController> _logger;
    private readonly IPlayersService _playerService;

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
        return await HandleEx(recordInputModel, _careerRecordDetailsServiceFuncs["GetBattingCareerRecords"]);
    }

    [HttpGet("inningsbyinnings/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetInningsByInnings(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _individualBattingDetailsServiceFuncs["GetBattingIndividualInnings"]);
    }

    [HttpGet("match/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetMatchDetails(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _individualBattingDetailsServiceFuncs["GetBattingIndividualMatches"]);
    }

    [HttpGet("series/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetSeriesRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _careerRecordDetailsServiceFuncs["GetBattingIndividualSeries"]);
    }

    [HttpGet("grounds/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetGroundRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _careerRecordDetailsServiceFuncs["GetBattingIndividualGrounds"]);
    }

    [HttpGet("host/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetHostRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _careerRecordDetailsServiceFuncs["GetBattingIndividualHost"]);
    }

    [HttpGet("opposition/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetOppositionRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _careerRecordDetailsServiceFuncs["GetBattingIndividualOpponents"]);
    }

    [HttpGet("year/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetYearRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _careerRecordDetailsServiceFuncs["GetBattingIndividualYear"]);
    }

    [HttpGet("season/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetSeasonRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _careerRecordDetailsServiceFuncs["GetBattingIndividualSeason"]);
    }
}