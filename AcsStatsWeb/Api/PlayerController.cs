using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AcsCommands.Query;
using AcsDto.Dtos;
using AcsStatsWeb.Models.api;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AcsStatsWeb.Api;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : BaseApiController
{
    private readonly IMediator _mediator;

    public PlayerController(IMediator mediator)
    {
        _mediator = mediator;
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

    private Result<Dictionary<string, List<BattingDetailsDto>>, Error> ConvertBattingDetailsToByMatchType(IReadOnlyList<BattingDetailsDto> battingDetailsDtos) 
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
    
    private Result<Dictionary<string, List<BowlingDetailsDto>>, Error> ConvertBowlingDetailsToByMatchType(IReadOnlyList<BowlingDetailsDto> bowlingDetailsDtos) 
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