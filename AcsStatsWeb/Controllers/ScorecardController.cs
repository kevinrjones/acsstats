using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsStatsWeb.Models;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Remote;

namespace AcsStatsWeb.Controllers;

public class ScorecardController : Controller
{
    private readonly IRemoteScorecardsService _scorecardsService;
    private readonly IRemoteMatchesService _remoteMatchesService;
    private readonly IValidation _validation;

    public ScorecardController(IRemoteScorecardsService scorecardsService, IRemoteMatchesService remoteMatchesService, IValidation validation)
    {
        _scorecardsService = scorecardsService;
        _remoteMatchesService = remoteMatchesService;
        _validation = validation;
    }

    [HttpGet("[controller]/card/{idTemplate}")]
    public async Task<IActionResult> Get(string idTemplate)
    {
        var scorecardSearchTemplate = ScorecardSearchTemplate.Create(idTemplate);
        
        return await scorecardSearchTemplate
            .Bind(async st => await _scorecardsService.GetScorecard(st))
            .Finally(res =>
                res.IsSuccess ? View("Card", res.Value) : View("Error", new ErrorViewModel(res.Error)));
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("[controller]/search/{type}")]
    public async Task<IActionResult> GetSearch(string type)
    {
        ViewBag.Type = type;
        return View("Search");
    }

    [HttpPost("[controller]/search/{type}")]
    public async Task<IActionResult> PostSearch([FromForm] MatchSearchModel matchSearchModel, [FromRoute] string type)
    {
        ViewBag.Type = type;

        matchSearchModel.StartDate ??= "0001-01-01";
        matchSearchModel.EndDate ??= "9999-12-31";
        var isValid = _validation.ValidateMatchSearchModel(matchSearchModel);
        
        if (isValid.IsFailure)
        {
            var m = isValid.Error as CombinedError;
            if (m == null)
            {
                ModelState.AddModelError(((ModelError)isValid.Error).Key, isValid.Error.Message);
            }
            else
            {
                foreach (var value in m?.Errors)
                {
                    if (value is ModelError maybeError)
                    {
                        ModelState.AddModelError(maybeError.Key, maybeError.Message);
                    }
                }
            }
        }
        
        if (!ModelState.IsValid)
        {
            return View("Search", matchSearchModel);
        }

        var matchesList = await _remoteMatchesService.FindMatches(matchSearchModel);

        return matchesList.OnFailure(error => { ModelState.AddModelError("HomeTeam", error.Message); })
            .Finally(res =>
                res.IsSuccess ? View("MatchList", matchesList.Value) : View("Search"));
    }


    [HttpGet("[controller]/bydecade/{type}")]
    public async Task<IActionResult> ByDecade(string type)
    {
        ViewBag.Type = type;

        return await _remoteMatchesService.GetSeriesDates(type)
            .Bind(ConvertToByDecade)
            .OnFailure(error => { ModelState.AddModelError("OpponentsId", error.Message); })
            .Finally(res =>
                res.IsSuccess ? View("ByDecade", res.Value) : View("Error", new ErrorViewModel(res.Error)));
        ;
    }

    [HttpGet("[controller]/byyear/{type}/{*season}")]
    public async Task<IActionResult> Search(string type, string season)
    {
        ViewBag.Type = type;

        return await _remoteMatchesService.GetTournamentsForSeason(type, season)
            .OnFailure(error => { ModelState.AddModelError("OpponentsId", error.Message); })
            .Finally(res =>
                res.IsSuccess ? View("TournamentList", res.Value) : View("Error", new ErrorViewModel(res.Error)));
        ;
    }

    [HttpGet("[controller]/tournament/{*tournament}")]
    public async Task<IActionResult> GetMatchesInTournament(string tournament)
    {
        var res = await _remoteMatchesService.GetMatchesForTournament(tournament);
        return    res.OnFailure(error => { ModelState.AddModelError("OpponentsId", error.Message); })
            .Finally(res =>
                res.IsSuccess ? View("MatchList", res.Value) : View("Error", new ErrorViewModel(res.Error)));
    }


    private Result<Dictionary<int, List<string>>, Error> ConvertToByDecade(List<string> series)
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