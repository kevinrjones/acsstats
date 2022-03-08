using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcsStatsWeb.Models.api;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.Models;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace AcsStatsWeb.Api
{
  [Route("api/[controller]")]
  [ApiController]
  public class FieldingRecordsController : BaseApiController
  {
    private readonly
      Dictionary<string, Func<BattingBowlingFieldingModel,
        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>>> _careerRecordDetailsServiceFuncs =
        new();

    private readonly
      Dictionary<string, Func<BattingBowlingFieldingModel,
        Task<Result<IReadOnlyList<IndividualFieldingDetails>, Error>>>> _individualFieldingDetailsServiceFuncs =
        new();

    public FieldingRecordsController(ITeamsService teamsService,
      IPlayersService playerService,
      ICountriesService countriesService,
      IValidation validation,
      ILogger<FieldingRecordsController> logger) : base(
      teamsService, countriesService, validation, logger)
    {

      _careerRecordDetailsServiceFuncs.Add("GetFieldingCareerRecords", playerService.GetFieldingCareerRecords);
      _careerRecordDetailsServiceFuncs.Add("GetFieldingCareerRecordsBySeries",
        playerService.GetFieldingCareerRecordsBySeries);
      _careerRecordDetailsServiceFuncs.Add("GetFieldingCareerRecordsByGround",
        playerService.GetFieldingCareerRecordsByGround);
      _careerRecordDetailsServiceFuncs.Add("GetFieldingCareerRecordsByHost",
        playerService.GetFieldingCareerRecordsByHost);
      _careerRecordDetailsServiceFuncs.Add("GetFieldingCareerRecordsByOpposition",
        playerService.GetFieldingCareerRecordsByOpposition);
      _careerRecordDetailsServiceFuncs.Add("GetFieldingCareerRecordsByYear",
        playerService.GetFieldingCareerRecordsByYear);
      _careerRecordDetailsServiceFuncs.Add("GetFieldingCareerRecordsBySeason",
        playerService.GetFieldingCareerRecordsBySeason);

      _individualFieldingDetailsServiceFuncs.Add("GetFieldingIndividualInnings",
        playerService.GetFieldingIndividualInnings);
      _individualFieldingDetailsServiceFuncs.Add("GetFieldingIndividualMatches",
        playerService.GetFieldingIndividualMatches);
    }


    [HttpGet("overall/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetOverallResults(
      [FromRoute] ApiRecordInputModel recordInputModel)
    {
      return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetFieldingCareerRecords"]);
    }

    [HttpGet("inningsbyinnings/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetInningsByInnings(
      [FromRoute] ApiRecordInputModel recordInputModel)
    {
      return await Handle(recordInputModel,
        _individualFieldingDetailsServiceFuncs["GetFieldingIndividualInnings"]);
    }

    [HttpGet("match/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetMatchDetails(
      [FromRoute] ApiRecordInputModel recordInputModel)
    {
      return await Handle(recordInputModel,
        _individualFieldingDetailsServiceFuncs["GetFieldingIndividualMatches"]);
    }

    [HttpGet("series/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetSeriesRecords(
      [FromRoute] ApiRecordInputModel recordInputModel)
    {
      return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetFieldingCareerRecordsBySeries"]);
    }

    [HttpGet("grounds/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetGroundRecords(
      [FromRoute] ApiRecordInputModel recordInputModel)
    {
      return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetFieldingCareerRecordsByGround"]);
    }

    [HttpGet("host/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetHostRecords(
      [FromRoute] ApiRecordInputModel recordInputModel)
    {
      return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetFieldingCareerRecordsByHost"]);
    }

    [HttpGet("opposition/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetOppositionRecords(
      [FromRoute] ApiRecordInputModel recordInputModel)
    {
      return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetFieldingCareerRecordsByOpposition"]);
    }

    [HttpGet("year/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetYearRecords(
      [FromRoute] ApiRecordInputModel recordInputModel)
    {
      return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetFieldingCareerRecordsByYear"]);
    }

    [HttpGet("season/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetSeasonRecords(
      [FromRoute] ApiRecordInputModel recordInputModel)
    {
      return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetFieldingCareerRecordsBySeason"]);
    }


  }
}