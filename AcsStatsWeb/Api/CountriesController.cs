using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;

namespace AcsStatsWeb.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : BaseApiController
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService,
            ILogger<CountriesController> logger) : base(logger)
        {
            _countriesService = countriesService;
        }

        // GET: api/Teams/wf
        [HttpGet("{matchType}")]
        public async Task<IActionResult> GetGrounds(string matchType)
        {
            return await (MatchType.Create(matchType)
                    .Bind(async m => (await _countriesService.GetCountriesForMatchType(matchType))))
                .Match(Ok, (it) => base.Error(it.Message));;
        }
    }
}