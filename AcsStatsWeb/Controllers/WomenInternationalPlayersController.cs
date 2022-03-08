using System.Linq;
using System.Threading.Tasks;
using AcsStatsWeb.Models;
using AcsTypes.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;

namespace AcsStatsWeb.Controllers
{
    public class WomenInternationalPlayers : Controller
    {
        private readonly ILogger<BattingRecordsController> _logger;
        private readonly IMatchesService _matchesService;
        private readonly IPlayersService _playersService;
        private readonly ITeamsService _teamsService;

        public WomenInternationalPlayers(ILogger<BattingRecordsController> logger, IMatchesService matchesService,
            IPlayersService playersService, ITeamsService teamsService)
        {
            _logger = logger;
            _matchesService = matchesService;
            _playersService = playersService;
            _teamsService = teamsService;
        }
    }
}