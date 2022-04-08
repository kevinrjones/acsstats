using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Services.Remote;

namespace AcsStatsWeb.Controllers;

public class ScorecardController : Controller
{
    private readonly IRemoteScorecardsService _scorecardsService;

    public ScorecardController(IRemoteScorecardsService scorecardsService)
    {
        _scorecardsService = scorecardsService;
    }
    
    public async Task<IActionResult> Index(int id)
    {
        var scorecard = await _scorecardsService.GetScorecard(id);
        
        return scorecard.OnFailure(error => { ModelState.AddModelError("OpponentsId", error.Message); })
            .Finally(res =>
                res.IsSuccess ? View("Index", scorecard.Value) : View("Index"));
    }
}