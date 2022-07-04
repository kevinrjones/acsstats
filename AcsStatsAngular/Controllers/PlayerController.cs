using AcsCommands.Query;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace AcsStatsAngular.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly IPlayersService _playersService;
    private readonly IValidation _validation;

    public PlayerController(IMediator mediator
        , IPlayersService playersService
        , IValidation validation
        , ILogger<PlayerController> logger) : base(logger)
    {
        _mediator = mediator;
        _playersService = playersService;
        _validation = validation;
    }

    [HttpGet("biography/{id}")]
    public async Task<IActionResult> GetBiography(
        [FromRoute] int id)
    {
        return (await _mediator.Send(new PlayerBiographyQuery(id)))
            .Match(Ok, it => Error(it.Message));
    }

    [HttpGet("overall/{id}")]
    public async Task<IActionResult> GetOverallStatsBy(
        [FromRoute] int id)
    {
        return (await _mediator.Send(new PlayerOverallQuery(id)))
            .Map(r => SortOverall(r.ToList()))
            .Match(Ok, it => Error(it.Message));
    }

    [HttpGet("battingdetails/{id}")]
    public async Task<IActionResult> GetBattingDetailsById(
        [FromRoute] int id)
    {
        return (await _mediator.Send(new BattingDetailsQuery(id)))
            .Bind(ConvertBattingDetailsToByMatchType)
            .Match(Ok, it => Error(it.Message));
    }

    [HttpGet("bowlingdetails/{id}")]
    public async Task<IActionResult> GetBowlingDetailsById(
        [FromRoute] int id)
    {
        return (await _mediator.Send(new BowlingDetailsQuery(id)))
            .Bind(ConvertBowlingDetailsToByMatchType)
            .Match(Ok, it => Error(it.Message));
    }

    [HttpGet("findplayers")]
    public async Task<IActionResult> FindPlayers([FromQuery] PlayerSearchModel playerSearchModel)
    {
        playerSearchModel.DebutDate ??= "0001-01-01";
        playerSearchModel.ActiveUntil ??= "9999-12-31";
        
        var res = await _validation.ValidatePlayerSearchModel(playerSearchModel).Bind(async m =>
                await _playersService.GetPlayersFromSearch(playerSearchModel))
            .Match(Ok, it => Error(it));

        return res;
    }


    private Result<Dictionary<string, List<BattingDetailsDto>>, Error> ConvertBattingDetailsToByMatchType(
        IReadOnlyList<BattingDetailsDto> battingDetailsDtos)
    {
        var dict = new Dictionary<string, List<BattingDetailsDto>>();
        foreach (var battingDetailsDto in battingDetailsDtos)
            if (dict.TryGetValue(battingDetailsDto.MatchType, out var details))
            {
                details.Add(battingDetailsDto);
            }
            else
            {
                var entry = new List<BattingDetailsDto>();
                entry.Add(battingDetailsDto);
                dict.Add(battingDetailsDto.MatchType, entry);
            }

        return Result.Success<Dictionary<string, List<BattingDetailsDto>>, Error>(dict);
    }

    private Result<Dictionary<string, List<BowlingDetailsDto>>, Error> ConvertBowlingDetailsToByMatchType(
        IReadOnlyList<BowlingDetailsDto> bowlingDetailsDtos)
    {
        var dict = new Dictionary<string, List<BowlingDetailsDto>>();
        foreach (var bowlingDetailsDto in bowlingDetailsDtos)
            if (dict.TryGetValue(bowlingDetailsDto.MatchType, out var details))
            {
                details.Add(bowlingDetailsDto);
            }
            else
            {
                var entry = new List<BowlingDetailsDto>();
                entry.Add(bowlingDetailsDto);
                dict.Add(bowlingDetailsDto.MatchType, entry);
            }

        return Result.Success<Dictionary<string, List<BowlingDetailsDto>>, Error>(dict);
    }
    
    private IReadOnlyList<PlayerOverallDto> SortOverall(List<PlayerOverallDto> originalList)
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