using System.Web;
using AcsDto.Models;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace AcsStatsAngular.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MatchesController : BaseApiController
{
    private readonly ILogger<MatchesController> _logger;
    private readonly IMatchesService _matchesService;
    private readonly IValidation _validation;

    public MatchesController(IMatchesService matchesService,
        IValidation validation,
        ILogger<MatchesController> logger) : base(logger)
    {
        _matchesService = matchesService;
        _validation = validation;
        _logger = logger;
    }

    // GET: api/matches/dates/wf
    [HttpGet("dates/{matchType}")]
    public async Task<IActionResult> GetDates(string matchType)
    {
        var res = (await _matchesService.GetDatesForMatchType(matchType))
            .Match(Ok, it => Error(it.Message));

        return res;
    }

    [HttpGet("seriesdates/{matchType}")]
    public async Task<IActionResult> GetSeriesDates(string matchType)
    {
        var res = (await _matchesService.GetSeriesDatesForMatchType(matchType))
            .Match(Ok, it => Error(it.Message));

        return res;
    }

    [HttpGet("seriesdatesformatchtypes")]
    public async Task<IActionResult> GetSeriesDatesForMatchTypes([FromQuery] string[] matchTypes)
    {
        var res = (await _matchesService.GetSeriesDatesForMatchTypes(matchTypes))
            .Bind(ConvertToByDecade)
            .Match(Ok, it => Error(it.Message));

        return res;
    }

    [HttpGet("tournamentsforseason/{*season}")]
    public async Task<IActionResult> GetTournamentsForSeason(string season, [FromQuery] string[] matchtypes)
    {
        var res = (await _matchesService.GetTournamentsForSeason(matchtypes, HttpUtility.UrlDecode(season)))
            .Match(Ok, it => Error(it.Message));

        return res;
    }

    [HttpGet("matchesintournament/{*tournament}")]
    public async Task<IActionResult> GetMatchesInTournament(string tournament)
    {
        var res = (await _matchesService.GetMatchesInTournament(HttpUtility.UrlDecode(tournament)))
            .Match(Ok, it => Error(it.Message));

        return res;
    }

    [HttpGet("findmatches")]
    public async Task<IActionResult> FindMatches([FromQuery] MatchSearchModel matchSearchModel)
    {
        matchSearchModel.StartDate ??= "0001-01-01";
        matchSearchModel.EndDate ??= "9999-12-31";

        var res = await _validation.ValidateMatchSearchModel(matchSearchModel).Bind(async m =>
                await _matchesService.GetMatchesFromSearch(matchSearchModel))
            .Match(Ok, it => Error(it.Message));

        return res;
    }

    [HttpGet("matchdates/{homeTeamId}/{awayTeamId}/{matchType}")]
    public async Task<IActionResult> GetMatchDatesForTeams(int homeTeamId, int awayTeamId, string matchType)
    {
        var res = (await _matchesService.GetSeriesDatesForMatches(homeTeamId, awayTeamId, matchType))
            .Match(Ok, it => Error(it.Message));

        return res;
    }
    
    private Result<Dictionary<int, List<string>>, Error> ConvertToByDecade(IReadOnlyList<string> series)
    {
        var dict = new Dictionary<int, List<string>>();

        foreach (var year in series)
        {
            if (!int.TryParse(year[..3], out var yearStart))
            {
                return Result.Failure<Dictionary<int, List<string>>, Error>(Errors.GetUnexpectedError("Unable to parse years"));
            }

            var decade = yearStart * 10;

            if (dict.TryGetValue(decade, out var entry))
            {
                entry.Add(year);
            }
            else
            {
                entry = new List<string>();
                entry.Add(year);
                dict.Add(decade, entry);
            }
        }

        return Result.Success<Dictionary<int, List<string>>, Error>(dict);
    }
}