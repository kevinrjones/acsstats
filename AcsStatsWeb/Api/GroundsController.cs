using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;

namespace AcsStatsWeb.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroundsController : BaseApiController
    {
        private readonly IGroundsService _groundsService;

        public GroundsController(IGroundsService groundsService,
            ILogger<GroundsController> logger) : base(logger)
        {
            _groundsService = groundsService;
        }

        // GET: api/Teams/wf
        [HttpGet("{matchType}")]
        public async Task<IActionResult> GetGrounds(string matchType)
        {
            return await (MatchType.Create(matchType)
                    .Bind(async m => (await _groundsService.GetGroundsForMatchType(m))))
                .Match(Ok, (it) => base.Error(it.Message));;
        }
    }
}