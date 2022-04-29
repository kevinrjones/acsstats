using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AcsCommands.Query;
using AcsDto.Dtos;
using AcsStatsWeb.Models.api;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AcsStatsWeb.Api;

[Route("api/[controller]")]
[ApiController]
public class ScorecardController : BaseApiController
{
    private readonly IMediator _mediator;

    public ScorecardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("byid/{matchId}")]
    public async Task<IActionResult> GetCardById(
        [FromRoute] int matchId)
    {
        return (await _mediator.Send(new ScorecardQuery(matchId)))
            .Map(it =>
            {
                var homeTeamScores = GetScoreStrings(it, it.Header.HomeTeam.Key);
                var awayTeamScores = GetScoreStrings(it, it.Header.AwayTeam.Key);

                var header = it.Header with {AwayTeamScores = awayTeamScores, HomeTeamScores = homeTeamScores};
                return it with {Header = header};
            })
            .Match(Ok, (it) => base.Error(it.Message));
    }

    [HttpGet("{urlTemplate}")]
    public async Task<IActionResult> GetCardByTemplate(
        [FromRoute] string urlTemplate)
    {
        var template = ScorecardSearchTemplate.Create(HttpUtility.UrlDecode(urlTemplate));

        return await template.Bind(async t =>
                await _mediator.Send(new ScorecardQuery(HttpUtility.UrlDecode(t.HomeTeam)
                    , HttpUtility.UrlDecode(t.AwayTeam)
                    , HttpUtility.UrlDecode(t.Date))))
            .Map(it =>
            {
                var homeTeamScores = GetScoreStrings(it, it.Header.HomeTeam.Key);
                var awayTeamScores = GetScoreStrings(it, it.Header.AwayTeam.Key);

                var header = it.Header with {AwayTeamScores = awayTeamScores, HomeTeamScores = homeTeamScores};
                return it with {Header = header};
            })
            .Match(Ok, (it) => base.Error(it.Message));
    }

    private static string[] GetScoreStrings(ScorecardDto scorecard, int key)
    {
        return scorecard.Innings
            .Filter(i => i.Team.Key == key)
            .Map(i =>
            {
                if (i.Total.Declared)
                    return $"{i.Total.Total}/{i.Total.Wickets} d";
                else if (i.Total.Wickets < 10)
                    return $"{i.Total.Total} for {i.Total.Wickets}";
                else
                    return i.Total.Total.ToString();
            }).ToArray();
    }
}