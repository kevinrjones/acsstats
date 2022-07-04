using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcsDto.Dtos;
using CSharpFunctionalExtensions;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Services.Remote;

namespace AcsStatsWeb.Controllers;

public class PlayerController : Controller
{
    private readonly IRemotePlayerService _remotePlayerService;

    public PlayerController(IRemotePlayerService remotePlayerService)
    {
        _remotePlayerService = remotePlayerService;
    }

    // GET
    [HttpGet("[controller]/{id}")]
    public async Task<IActionResult> Index(int id)
    {
        PlayerBiographyDto playerBiography = new PlayerBiographyDto(new List<NameDetail>());
        List<PlayerOverallDto> playerOverall = new List<PlayerOverallDto>();
        Dictionary<string, List<BattingDetailsDto>> battingDetails = new Dictionary<string, List<BattingDetailsDto>>();
        Dictionary<string, List<BowlingDetailsDto>> bowlingDetails = new Dictionary<string, List<BowlingDetailsDto>>();

        await _remotePlayerService.GetPlayerBiography(id).OnSuccessTry(o => playerBiography = o);
        await _remotePlayerService.GetPlayerOverall(id).OnSuccessTry(p => playerOverall = SortOverall(p));
        await _remotePlayerService.GetPlayerBattingDetails(id).OnSuccessTry(bd => battingDetails = bd);
        await _remotePlayerService.GetPlayerBowlingDetails(id).OnSuccessTry(bd => bowlingDetails = bd);
        
        PlayerRecordComplete playerRecordComplete =
            new PlayerRecordComplete(playerBiography, playerOverall, battingDetails, bowlingDetails);

        return View(playerRecordComplete);
    }

    private List<PlayerOverallDto> SortOverall(List<PlayerOverallDto> originalList)
    {
        var listOfSortedMatchType = new List<string>
            {"wt", "wo", "witt", "wf", "wa", "wtt", "t", "o", "itt", "f", "a", "tt"};

        var sortedList = new List<PlayerOverallDto>();

        foreach (var matchType in listOfSortedMatchType)
        {
            var listOfMatchType = originalList.Where(l => l.MatchType == matchType).ToList();
            PlayerOverallDto totals = null;
            if (listOfMatchType.Count > 0)
            {
                totals = listOfMatchType.Reduce((a, b) => a with
                {
                    Runs = a.Runs + b.Runs,
                    Innings = a.Innings + b.Innings,
                    Notouts = a.Notouts + b.Notouts,
                    Balls = a.Balls + b.Balls,
                    Fours = a.Fours + b.Fours,
                    Sixes = a.Sixes + b.Sixes,
                    Hundreds = a.Hundreds + b.Hundreds,
                    Fifties = a.Fifties + b.Fifties,
                    BowlingBalls = a.BowlingBalls + b.BowlingBalls,
                    BowlingRuns = a.BowlingRuns + b.BowlingRuns,
                    Maidens = a.Maidens + b.Maidens,
                    Wickets = a.Wickets + b.Wickets,
                    BowlingFours = a.BowlingFours + b.BowlingFours,
                    BowlingSixes = a.BowlingSixes + b.BowlingSixes,
                    Wides = a.Wides + b.Wides,
                    NoBalls = a.NoBalls + b.NoBalls
                });
                totals = totals with {Team = "Total"};
            }


            sortedList.AddRange(originalList.Where(i => i.MatchType == matchType));
            if (totals != null)
                sortedList.Add(totals);
        }


        return sortedList;
    }
}