using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Services;
using MatchType = AcsTypes.Types.MatchType;

namespace AcsStatsAngular.Controllers;

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
    public async Task<IActionResult> GetCountries(string matchType)
    {
        return await MatchType.Create(matchType)
            .Bind(async m => await _countriesService.GetCountriesForMatchType(matchType))
            .Match(Ok, it => Error(it.Message));
        ;
    }
}