using System.Collections.Generic;
using System.Threading.Tasks;
using AcsCommands.Query;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;

namespace AcsStatsWeb.Api;

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
      .Match(Ok, (it) => base.Error(it.Message));
  }

  [HttpGet("overall/{id}")]
  public async Task<IActionResult> GetOverallStatsBy(
    [FromRoute] int id)
  {
    return (await _mediator.Send(new PlayerOverallQuery(id)))
      .Match(Ok, (it) => base.Error(it.Message));
  }

  [HttpGet("battingdetails/{id}")]
  public async Task<IActionResult> GetBattingDetailsById(
    [FromRoute] int id)
  {
    return (await _mediator.Send(new BattingDetailsQuery(id)))
      .Bind(ConvertBattingDetailsToByMatchType)
      .Match(Ok, (it) => base.Error(it.Message));
  }

  [HttpGet("bowlingdetails/{id}")]
  public async Task<IActionResult> GetBowlingDetailsById(
    [FromRoute] int id)
  {
    return (await _mediator.Send(new BowlingDetailsQuery(id)))
      .Bind(ConvertBowlingDetailsToByMatchType)
      .Match(Ok, (it) => base.Error(it.Message));
  }

  [HttpGet("findplayers")]
  public async Task<IActionResult> FindPlayers([FromQuery] PlayerSearchModel playerSearchModel)
  {
    var res = await _validation.ValidatePlayerSearchModel(playerSearchModel).Bind(async m =>
        await _playersService.GetPlayersFromSearch(playerSearchModel))
      .Match(Ok, (it) => Error(it.Message));

    return res;
  }


  private Result<Dictionary<string, List<BattingDetailsDto>>, Error> ConvertBattingDetailsToByMatchType(
    IReadOnlyList<BattingDetailsDto> battingDetailsDtos)
  {
    var dict = new Dictionary<string, List<BattingDetailsDto>>();
    foreach (var battingDetailsDto in battingDetailsDtos)
    {
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
    }

    return Result.Success<Dictionary<string, List<BattingDetailsDto>>, Error>(dict);
  }

  private Result<Dictionary<string, List<BowlingDetailsDto>>, Error> ConvertBowlingDetailsToByMatchType(
    IReadOnlyList<BowlingDetailsDto> bowlingDetailsDtos)
  {
    var dict = new Dictionary<string, List<BowlingDetailsDto>>();
    foreach (var bowlingDetailsDto in bowlingDetailsDtos)
    {
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
    }

    return Result.Success<Dictionary<string, List<BowlingDetailsDto>>, Error>(dict);
  }
}