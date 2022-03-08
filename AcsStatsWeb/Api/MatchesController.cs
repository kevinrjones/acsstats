using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class MatchesController : BaseApiController
    {
        private readonly IMatchesService _matchesService;
        private readonly ILogger<MatchesController> _logger;

        public MatchesController(IMatchesService matchesService,
            ILogger<MatchesController> logger) : base(logger)
        {
            _matchesService = matchesService;
            _logger = logger;
        }

        // GET: api/dates/wf
        [HttpGet("dates/{matchType}")]
        public async Task<IActionResult> GetDates(string matchType)
        {
            var res = (await _matchesService.GetDatesForMatchType(matchType))
                .Match(Ok, (it) => base.Error(it.Message));

            return res;
        }

        [HttpGet("seriesdates/{matchType}")]
        public async Task<IActionResult> GetSeriesDates(string matchType)
        {
            var res = (await _matchesService.GetSeriesDatesForMatchType(matchType))
                .Match(Ok, (it) => Error(it.Message));

            return res;
        }
    }
}