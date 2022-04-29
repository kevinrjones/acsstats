using System.Threading.Tasks;
using System.Web;
using AcsDto.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;

namespace AcsStatsWeb.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : BaseApiController
    {
        private readonly IMatchesService _matchesService;
        private readonly IValidation _validation;
        private readonly ILogger<MatchesController> _logger;

        public MatchesController(IMatchesService matchesService,
            IValidation validation,
            ILogger<MatchesController> logger) : base(logger)
        {
            _matchesService = matchesService;
            _validation = validation;
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

        [HttpGet("seriesdatesformatchtypes")]
        public async Task<IActionResult> GetSeriesDatesForMatchTypes([FromQuery] string[] matchTypes)
        {
            var res = (await _matchesService.GetSeriesDatesForMatchTypes(matchTypes))
                .Match(Ok, (it) => Error(it.Message));

            return res;
        }

        [HttpGet("tournamentsforseason")]
        public async Task<IActionResult> GetTournamentsForSeason([FromQuery] string[] matchtypes,
            [FromQuery] string season)
        {
            var res = (await _matchesService.GetTournamentsForSeason(matchtypes, season))
                .Match(Ok, (it) => Error(it.Message));

            return res;
        }

        [HttpGet("matchesintournament/{tournament}")]
        public async Task<IActionResult> GetMatchesInTournament(string tournament)
        {
            var res = (await _matchesService.GetMatchesInTournament(HttpUtility.UrlDecode(tournament)))
                .Match(Ok, (it) => Error(it.Message));

            return res;
        }

        [HttpGet("findmatches")]
        public async Task<IActionResult> FindMatches([FromQuery]MatchSearchModel matchSearchModel)
        {
            var res = await _validation.ValidateMatchSearchModel(matchSearchModel).Bind(async m =>
                    await _matchesService.GetMatchesFromSearch(matchSearchModel))
                .Match(Ok, (it) => Error(it.Message));

            return res;
        }

        [HttpGet("matchdates/{homeTeamId}/{awayTeamId}/{matchType}")]
        public async Task<IActionResult> GetMatchDatesForTeams(int homeTeamId, int awayTeamId, string matchType)
        {
            var res = (await _matchesService.GetSeriesDatesForMatches(homeTeamId, awayTeamId, matchType))
                .Match(Ok, (it) => Error(it.Message));

            return res;
        }
    }
}