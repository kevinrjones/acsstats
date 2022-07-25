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
public class BowlingRecordsController : BaseApiController
{
    private readonly
        Dictionary<string, Func<BattingBowlingFieldingModel,
            Task<Result<SqlResultEnvelope<IReadOnlyList<BowlingCareerRecordDto>>, Error>>>> _careerRecordDetailsServiceFuncs =
            new();

    private readonly
        Dictionary<string, Func<BattingBowlingFieldingModel,
            Task<Result<SqlResultEnvelope<IReadOnlyList<IndividualBowlingDetailsDto>>, Error>>>> _individualBowlingDetailsServiceFuncs =
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
        _careerRecordDetailsServiceFuncs.Add("GetBowlingIndividualOpponents",
            playerService.GetBowlingIndividualOpponents);
        _careerRecordDetailsServiceFuncs.Add("GetBowlingIndividualYear", playerService.GetBowlingIndividualYear);
        _careerRecordDetailsServiceFuncs.Add("GetBowlingIndividualSeason", playerService.GetBowlingIndividualSeason);

        _individualBowlingDetailsServiceFuncs.Add("GetBowlingIndividualInnings",
            playerService.GetBowlingIndividualInnings);
        _individualBowlingDetailsServiceFuncs.Add("GetBowlingIndividualMatches",
            playerService.GetBowlingIndividualMatches);
    }


    [HttpGet("overall/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetOverallResults(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _careerRecordDetailsServiceFuncs["GetBowlingCareerRecords"]);
    }

    [HttpGet("inningsbyinnings/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetInningsByInnings(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _individualBowlingDetailsServiceFuncs["GetBowlingIndividualInnings"]);
    }

    [HttpGet("match/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetMatchDetails(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _individualBowlingDetailsServiceFuncs["GetBowlingIndividualMatches"]);
    }

    [HttpGet("series/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetSeriesRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _careerRecordDetailsServiceFuncs["GetBowlingIndividualSeries"]);
    }

    [HttpGet("grounds/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetGroundRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _careerRecordDetailsServiceFuncs["GetBowlingIndividualGrounds"]);
    }

    [HttpGet("host/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetHostRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _careerRecordDetailsServiceFuncs["GetBowlingIndividualHost"]);
    }

    [HttpGet("opposition/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetOppositionRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _careerRecordDetailsServiceFuncs["GetBowlingIndividualOpponents"]);
    }

    [HttpGet("year/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetYearRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _careerRecordDetailsServiceFuncs["GetBowlingIndividualYear"]);
    }

    [HttpGet("season/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetSeasonRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await HandleEx(recordInputModel, _careerRecordDetailsServiceFuncs["GetBowlingIndividualSeason"]);
    }
}