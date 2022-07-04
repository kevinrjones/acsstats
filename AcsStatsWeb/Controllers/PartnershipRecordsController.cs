using AcsDto.Dtos;
using AcsDto.Models;
using AcsStatsAngular.Models.api;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace AcsStatsAngular.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartnershipRecordsController : BaseApiController
{
    private readonly
        Dictionary<string, Func<PartnershipModel,
            Task<Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error>>>> _careerRecordDetailsServiceFuncs =
            new();

    private readonly
        Dictionary<string, Func<PartnershipModel,
            Task<Result<IReadOnlyList<PartnershipIndividualRecordDetailsDto>, Error>>>>
        _individualPartnershipDetailsServiceFuncs =
            new();

    public PartnershipRecordsController(ITeamsService teamsService,
        IPartnershipService partnershipService,
        ICountriesService countriesService,
        IValidation validation,
        ILogger<PartnershipRecordsController> logger) : base(
        teamsService, countriesService, validation, logger)
    {
        _careerRecordDetailsServiceFuncs.Add("GetPartnershipCareerRecords",
            partnershipService.GetPartnershipCareerRecords);
        _careerRecordDetailsServiceFuncs.Add("GetPartnershipIndividualSeries",
            partnershipService.GetPartnershipIndividualSeries);
        _careerRecordDetailsServiceFuncs.Add("GetPartnershipIndividualGrounds",
            partnershipService.GetPartnershipIndividualGrounds);
        _careerRecordDetailsServiceFuncs.Add("GetPartnershipIndividualHost",
            partnershipService.GetPartnershipIndividualHost);
        _careerRecordDetailsServiceFuncs.Add("GetPartnershipIndividualOpponents",
            partnershipService.GetPartnershipIndividualOpponents);
        _careerRecordDetailsServiceFuncs.Add("GetPartnershipIndividualYear",
            partnershipService.GetPartnershipIndividualYear);
        _careerRecordDetailsServiceFuncs.Add("GetPartnershipIndividualSeason",
            partnershipService.GetPartnershipIndividualSeason);

        _individualPartnershipDetailsServiceFuncs.Add("GetPartnershipIndividualInnings",
            partnershipService.GetPartnershipIndividualInnings);
        _individualPartnershipDetailsServiceFuncs.Add("GetPartnershipIndividualMatches",
            partnershipService.GetPartnershipIndividualMatches);
    }


    [HttpGet("overall/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetOverallResults(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetPartnershipCareerRecords"]);
    }


    [HttpGet("inningsbyinnings/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetInningsByInnings(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await Handle(recordInputModel,
            _individualPartnershipDetailsServiceFuncs["GetPartnershipIndividualInnings"]);
    }

    [HttpGet("match/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetMatchDetails(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await Handle(recordInputModel,
            _individualPartnershipDetailsServiceFuncs["GetPartnershipIndividualMatches"]);
    }

    [HttpGet("series/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetSeriesRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetPartnershipIndividualSeries"]);
    }

    [HttpGet("grounds/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetGroundRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetPartnershipIndividualGrounds"]);
    }

    [HttpGet("host/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetHostRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetPartnershipIndividualHost"]);
    }

    [HttpGet("opposition/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetOppositionRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetPartnershipIndividualOpponents"]);
    }

    [HttpGet("year/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetYearRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetPartnershipIndividualYear"]);
    }

    [HttpGet("season/{matchType}/{teamId}/{opponentsId}")]
    public async Task<IActionResult> GetSeasonRecords(
        [FromRoute] ApiRecordInputModel recordInputModel)
    {
        return await Handle(recordInputModel, _careerRecordDetailsServiceFuncs["GetPartnershipIndividualSeason"]);
    }


    // ReSharper disable once InconsistentNaming
    private IActionResult PossiblyMergeMultipleTeams(
        Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error> partnershipRecordDetails)
    {
        var prdList = partnershipRecordDetails.Value.ToList();
        var newPrdList = new List<PartnershipCareerRecordDetails>();
        if (prdList.Any())
        {
            newPrdList.Add(prdList[0]);
            for (var i = 1; i < prdList.Count; i++)
                if (prdList[i].PlayerIds == prdList[i - 1].PlayerIds &&
                    prdList[i].CountryName != prdList[i - 1].CountryName)
                    prdList[i - 1].Team = $"{prdList[i].Team}, {prdList[i - 1].Team}";
                else
                    newPrdList.Add(prdList[i]);
        }

        return Ok(newPrdList);
    }
}