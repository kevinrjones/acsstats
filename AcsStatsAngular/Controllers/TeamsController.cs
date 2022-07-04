using AcsCommands.Query;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services;
using MatchType = AcsTypes.Types.MatchType;

namespace AcsStatsAngular.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamsController : BaseApiController
{
    private readonly IMediator _mediator;

    public TeamsController(IMediator mediator,
        ITeamsService teamsService,
        ILogger<TeamsController> logger) : base(
        teamsService, logger)
    {
        _mediator = mediator;
    }

    // GET: api/teams/wf
    [HttpGet("{matchType}")]
    public async Task<IActionResult> GetTeams(string matchType)
    {
        return await MatchType.Create(matchType)
            .Map(m => new TeamsQuery(m))
            .Bind(async q => await _mediator.Send(q))
            .Match(Ok, it => Error(it.Message));
    }
}