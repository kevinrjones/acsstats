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

namespace AcsStatsWeb.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : BaseApiController
    {
        private readonly Messages _messages;

        public TeamsController(Messages messages,
            ITeamsService teamsService,
            ILogger<TeamsController> logger) : base(
            teamsService, logger)
        {
            _messages = messages;
        }

        // GET: api/Teams/wf
        [HttpGet("{matchType}")]
        public async Task<IActionResult> GetTeams(string matchType)
        {
            return await (MatchType.Create(matchType)
                    .Map(m => new GetTeamsQuery(m))
                    .Bind(async m => (await _messages.Dispatch(m))))
                .Match(Ok, (error) => Error(error.Message));
            
        }
    }
}