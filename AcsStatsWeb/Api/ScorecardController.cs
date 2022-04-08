using System.Linq;
using System.Threading.Tasks;
using AcsCommands.Query;
using AcsDto.Dtos;
using AcsStatsWeb.Models.api;
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

    [HttpGet("{matchId}")]
    public async Task<IActionResult> GetOverallResults(
        [FromRoute] int matchId)
    {
        return (await _mediator.Send(new ScorecardQuery(matchId)))
            .Map(it =>
            {
                var homeTeamScores = GetScoreStrings(it, it.Header.HomeTeam.Key);
                var awayTeamScores = GetScoreStrings(it, it.Header.AwayTeam.Key);

                var header = new ScorecardHeaderDto(it.Header.Toss, it.Header.Where, it.Header.Result, it.Header.TestNo,
                    it.Header.Scorers, it.Header.Umpires, it.Header.AwayTeam, awayTeamScores,
                    it.Header.DayNight,
                    it.Header.HomeTeam, homeTeamScores, it.Header.TvUmpires, it.Header.MatchDate,
                    it.Header.MatchType,
                    it.Header.MatchTitle, it.Header.SeriesDate, it.Header.CloseOfPlay, it.Header.BallsPerOer,
                    it.Header.MatchReferee,
                    it.Header.MatchDesignator);
                return new ScorecardDto(it.Notes, it.Debuts, header, it.Innings);
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