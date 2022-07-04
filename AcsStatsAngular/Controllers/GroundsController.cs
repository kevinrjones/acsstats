using AcsCommands.Query;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MatchType = AcsTypes.Types.MatchType;

namespace AcsStatsAngular.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroundsController : BaseApiController
{
    private readonly IMediator _mediator;

    public GroundsController(IMediator mediator,
        ILogger<GroundsController> logger) : base(logger)
    {
        _mediator = mediator;
    }

    // GET: api/Teams/wf
    [HttpGet("{matchType}")]
    public async Task<IActionResult> GetGrounds(string matchType)
    {
        return await MatchType.Create(matchType)
            .Map(m => new GroundsQuery(m))
            .Bind(async q => await _mediator.Send(q))
            .Match(Ok, it => Error(it.Message));
    }
}