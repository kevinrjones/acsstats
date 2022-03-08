using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcsStatsWeb.Api.cqrs;
using AcsTypes.Types;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using CSharpFunctionalExtensions;

namespace AcsStatsWeb.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : BaseApiController
    {
        private readonly Messages _messages;

        public TeamsController(Messages messages,
            ITeamsService teamsService,
            ILogger<TeamsController> logger): base(
            teamsService, logger)
        {
            _messages = messages;
        }

        // GET: api/Teams/wf
        [HttpGet("{matchType}")]
        public async Task<IActionResult> GetTeams(string matchType)
        {
            return await (MatchType.Create(matchType)
                .Bind(async m => (await TeamsService.GetTeamsForMatchType(m))))
                .Match(Ok, (it) => Error(it.Message));;
        }
    }
}