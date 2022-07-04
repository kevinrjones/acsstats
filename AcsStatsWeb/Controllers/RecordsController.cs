using AcsDto.Dtos;
using AcsDto.Models;
using AcsStatsAngular.Models.api;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace AcsStatsAngular.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecordsController : BaseApiController
{
    private readonly Dictionary<string, string?> MatchTypeLookup = new Dictionary<string, string?>
    {
        {"wt", "Women's Tests"},
        {"wo", "Women's One Day International"},
        {"witt", "Women's International T20"},
        {"wf", "Women's First Class"},
        {"wa", "Women's List A"},
        {"wtt", "Women's T20"},
        {"t", "Men's Tests"},
        {"o", "Men's One Day International"},
        {"itt","Men's International T20"},
        {"f", "Men's First Class"},
        {"a", "Men's List A"},
        {"tt", "Men's T20"},
    };

    private readonly ILogger<RecordsController> _logger;
    private readonly IPlayersService _playerService;
    private readonly ICountriesService _countriesService;
    private readonly IGroundsService _groundsService;

    public RecordsController(ITeamsService teamsService,
        IPlayersService playerService,
        ICountriesService countriesService,
        IGroundsService groundsService,
        IValidation validation,
        ILogger<RecordsController> logger) : base(
        teamsService, countriesService, validation, logger)
    {
        _playerService = playerService;
        _countriesService = countriesService;
        _groundsService = groundsService;
        _logger = logger;
    }


    [HttpGet("summary/{matchType}/{teamId}/{opponentsId}/{groundId}/{hostCountryId}")]
    public async Task<IActionResult> GetSummary([FromRoute] SummaryInput summaryInput)
    {
        Result<SummaryInput, Error> validationResult = Validate(summaryInput);

        SummaryResult summaryResult = new SummaryResult();

        // todo: matchtype -> matchtype
        return await validationResult
            .Bind(r => TeamsService.GetTeam((TeamId) r.TeamId))
            .Tap(s => summaryResult.Team = s.Name)
            .Bind(_ => TeamsService.GetTeam((TeamId) summaryInput.OpponentsId))
            .Tap(s => summaryResult.Opponents = s.Name)
            .Bind(_ => _countriesService.getCountryFromId((CountryId) summaryInput.HostCountryId))
            .Tap(s => summaryResult.HostCountry = s.Name)
            .Bind(_ => _groundsService.GetGround((GroundId) summaryInput.GroundId))
            .Tap(s => summaryResult.Ground = s.KnownAs)
            .Bind(_ => GetMatchType(summaryInput.MatchType))
            .Tap(m => summaryResult.MatchType = m)
            .Finally(result => result.IsSuccess ? Ok(summaryResult) : new BadRequestResult());
        ;
    }

    private Result<string, Error> GetMatchType(string summaryInputMatchType)
    {
        MatchTypeLookup.TryGetValue(summaryInputMatchType, out var matchType);
        if (matchType == null)
            return Result.Failure<string, Error>(Errors.InvalidMatchTypeError());
        return Result.Success<string, Error>(matchType);
    }

    private Result<SummaryInput, Error> Validate(SummaryInput summaryInput)
    {
        var ids = Validation.ValidateTeamIds(summaryInput.TeamId, summaryInput.OpponentsId)
            .Match(r => Result.Success(), e => Result.Failure(e.Message));

        var countryId = Validation.ValidateCountry(summaryInput.HostCountryId)
            .Match(r => Result.Success(), e => Result.Failure(e.Message));

        var groundId = Validation.ValidateGround(summaryInput.GroundId)
            .Match(r => Result.Success(), e => Result.Failure(e.Message));

        var matchType = Validation.ValidateMatchType(summaryInput.MatchType)
            .Match(r => Result.Success(), e => Result.Failure(e.Message));

        var r = Result.Combine(ids, groundId, countryId, matchType);

        return r.IsSuccess
            ? Result.Success<SummaryInput, Error>(summaryInput)
            : Result.Failure<SummaryInput, Error>(r.Error);
    }

    public class SummaryInput
    {
        public int TeamId { get; set; }
        public int OpponentsId { get; set; }
        public string MatchType { get; set; }
        public int GroundId { get; set; }
        public int HostCountryId { get; set; }
    }

    public class SummaryResult
    {
        public string Team { get; set; }
        public string Opponents { get; set; }
        public string MatchType { get; set; }
        public string Ground { get; set; }
        public string HostCountry { get; set; }
    }
}