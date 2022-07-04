using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsStatsWeb.Models;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.Models;
using Services.Remote;
using MatchResult = AcsTypes.Types.MatchResult;

namespace AcsStatsWeb.Controllers
{
    public class BowlingRecordsController : AcsRecordsController
    {
        private readonly IRemoteBowlingRecordsService _remoteBowlingRecordsService;
        private readonly IGroundsService _groundsService;

        public BowlingRecordsController(ILogger<BowlingRecordsController> logger,
            IRemoteBowlingRecordsService remoteBowlingRecordsService,
            IGroundsService groundsService,
            IValidation validation,
            ITeamsService teamsService, ICountriesService countriesService) : base(teamsService, countriesService,
            validation, logger)
        {
            _remoteBowlingRecordsService = remoteBowlingRecordsService;
            _groundsService = groundsService;
            ViewData["Title"] = "Bowling Records";
        }


        public IActionResult Index()
        {
            ViewBag.Form = null;
            return View("Index");
        }

        public async Task<IActionResult> IndividualBowlingRecords(RecordInputModel recordInputModel)
        {
            ViewBag.Form = "bowling";
            var inputModelValidation = Validate(recordInputModel);

            if (inputModelValidation.IsFailure)
            {
                ModelState.AddModelError("OpponentsId", "Team and opponents cannot be the same");
                return View("Index");
            }


            var matchResult = MatchResult.Create(recordInputModel.MatchResult).Value;
            var maybeResultsModel = await InitializeResultModel<ResultsBowlingModel>(recordInputModel);
            var ground = await _groundsService.GetGround(recordInputModel.GroundId);

            if (maybeResultsModel.IsFailure || ground.IsFailure)
            {
                ModelState.AddModelError("OpponentsId", "Team and opponents cannot be the same");
                return View("Index");
            }

            ResultsBowlingModel resultsModel = maybeResultsModel.Value;

            var requestDates = GetEpochDates(recordInputModel.StartDate, recordInputModel.EndDate);

            
            recordInputModel.UpdateSortOrder(SortOrder.Wickets);
            resultsModel.UpdateMatchResult(recordInputModel.MatchResult);
            resultsModel.Ground = ground.Value.KnownAs;
            var serviceModel =
                InitializeSharedServiceModel<BattingBowlingFieldingModel>(recordInputModel, requestDates, matchResult);

            Result<List<IndividualBowlingDetailsDto>, Error> resultIndBowlingDetails =
                Result.Failure<List<IndividualBowlingDetailsDto>, Error>("Not initialized");
            Result<List<BowlingCareerRecordDto>, Error>
                resultPlayerCareerBowlingDetails =
                    Result.Failure<List<BowlingCareerRecordDto>, Error>("Not initialized");

            var viewName = "Index";

            switch (recordInputModel.Format)
            {
                case 1:
                    viewName = "BowlingOverall";
                    resultsModel.Title = "Overall Figures";
                    resultPlayerCareerBowlingDetails = await _remoteBowlingRecordsService.GetOverall(serviceModel);
                    break;

                case 2:
                    viewName = "IndividualInningsList";
                    resultsModel.Title = "Innings by Innings";
                    resultIndBowlingDetails = await _remoteBowlingRecordsService.GetInningsByInnings(serviceModel);
                    break;

                case 3:
                    viewName = "IndividualMatchList";
                    resultsModel.Title = "Match Totals";
                    resultIndBowlingDetails = await _remoteBowlingRecordsService.GetMatchDetails(serviceModel);
                    break;

                case 4:
                    viewName = "SeriesList";
                    resultsModel.Title = "Series Averages";
                    resultPlayerCareerBowlingDetails =
                        await _remoteBowlingRecordsService.GetRecordsForSeries(serviceModel);
                    break;

                case 5:
                    viewName = "GroundList";
                    resultsModel.Title = "Ground Averages";
                    resultPlayerCareerBowlingDetails =
                        await _remoteBowlingRecordsService.GetRecordsForGrounds(serviceModel);
                    break;

                case 6:
                    viewName = "HostCountryList";
                    resultsModel.Title = "By Host Country";
                    resultPlayerCareerBowlingDetails =
                        await _remoteBowlingRecordsService.GetRecordsForHost(serviceModel);
                    break;

                case 7:
                    viewName = "OppositionList";
                    resultsModel.Title = "By Opposition Team";
                    resultPlayerCareerBowlingDetails =
                        await _remoteBowlingRecordsService.GetRecordsForOpponents(serviceModel);
                    break;

                case 8:
                    viewName = "ByYearList";
                    resultsModel.Title = "By Year of Match Start";
                    resultPlayerCareerBowlingDetails =
                        await _remoteBowlingRecordsService.GetRecordsByYear(serviceModel);
                    break;

                case 9:
                    viewName = "BySeasonList";
                    resultsModel.Title = "By Season";
                    resultPlayerCareerBowlingDetails =
                        await _remoteBowlingRecordsService.GetRecordsBySeason(serviceModel);
                    break;
            }

            switch (recordInputModel.Format)
            {
                case 2:
                case 3:
                    return resultIndBowlingDetails.Tap(record =>
                        {
                            resultsModel.IndividualBowlingDetails = record;
                            resultsModel.MatchType = recordInputModel.MatchType;
                            SetShowTeamsInLists(resultsModel, (TeamId)recordInputModel.TeamId,
                                (TeamId)recordInputModel.OpponentsId);
                        })
                        .OnFailure(error => { ModelState.AddModelError("OpponentsId", error.Message); })
                        .Finally(res =>
                            res.IsSuccess ? View(viewName, resultsModel) : View("Index"));
                case 1:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return resultPlayerCareerBowlingDetails.Tap(record =>
                        {
                            resultsModel.PlayerRecordDetails = record;
                            resultsModel.MatchType = recordInputModel.MatchType;
                            SetShowTeamsInLists(resultsModel, (TeamId)recordInputModel.TeamId,
                                (TeamId)recordInputModel.OpponentsId);
                        })
                        .OnFailure(error => { ModelState.AddModelError("OpponentsId", error.Message); })
                        .Finally(res =>
                            res.IsSuccess ? View(viewName, resultsModel) : View("Index"));

                default:
                    return View("Index");
            }
        }
    }
}