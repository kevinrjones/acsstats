using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcsCommands;
using AcsCommands.Query;
using AcsStatsWeb.Dtos;
using AcsTypes.Types;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using CSharpFunctionalExtensions;
using MediatR;

namespace AcsStatsWeb.Api
{
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

        // GET: api/Teams/wf
        [HttpGet("{matchType}")]
        public async Task<IActionResult> GetTeams(string matchType)
        {
            return await MatchType.Create(matchType)
                .Map(m => new TeamsQuery(m))
                .Bind(async q => await _mediator.Send(q))
                .Match(Ok, (it) => base.Error(it.Message));
        }
    }
}