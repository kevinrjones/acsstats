using System.Collections.Generic;
using System.Threading.Tasks;
using AcsCommands.Query;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;

namespace AcsStatsWeb.Api
{
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
                .Map(m => new GetGroundsQuery(m))
                .Bind(async q => await _mediator.Send(q))
                .Match(Ok, (it) => base.Error(it.Message));
            
            // return await (MatchType.Create(matchType)
            //         .Bind(async m => (await _groundsService.GetGroundsForMatchType(m))))
            //     .Match(Ok, (it) => base.Error(it.Message));
        }
    }
}