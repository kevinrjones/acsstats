using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

// ReSharper disable InconsistentNaming

namespace AcsStatsWeb.Controllers
{
    public class BattingRecordsController : AcsRecordsController
    {
        private readonly IGroundsService _groundsService;
        private readonly IRemoteBattingRecordsService _battingRecordsRemoteService;

        private readonly
            Dictionary<string, Func<BattingBowlingFieldingModel,
                Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>>>> _careerRecordDetailsServiceFuncs =
                new();

        public BattingRecordsController(
            IGroundsService groundsService,
            IValidation validation,
            ITeamsService teamsService,
            ICountriesService countriesService,
            ILogger<BattingRecordsController> logger,
            IRemoteBattingRecordsService battingRecordsRemoteService
        ) : base(teamsService, countriesService, validation, logger)
        {
            _battingRecordsRemoteService = battingRecordsRemoteService;
            _groundsService = groundsService;
            ViewData["Title"] = "Batting Records";
        }

        public IActionResult Index()
        {
            ViewBag.Form = null;
            return View("Index");
        }

        public async Task<IActionResult> IndividualBattingRecords(RecordInputModel recordInputModel)
        {
            ViewBag.Form = "batting";

            var inputModelValidation = Validate(recordInputModel);

            if (inputModelValidation.IsFailure)
            {
                ModelState.AddModelError("OpponentsId", "Team and opponents cannot be the same");
                return View("Index");
            }

            var matchResult = MatchResult.Create(recordInputModel.MatchResult).Value;
            var maybeResultsModel = await InitializeResultModel<ResultsBattingModel>(recordInputModel);
            var ground = await _groundsService.getGround(recordInputModel.GroundId);

            if (maybeResultsModel.IsFailure || ground.IsFailure)
            {
                ModelState.AddModelError("OpponentsId", "Team and opponents cannot be the same");
                return View("Index");
            }

            var resultsModel = maybeResultsModel.Value;

            var requestDates = GetEpochDates(recordInputModel.StartDate, recordInputModel.EndDate);


            recordInputModel.UpdateSortOrder(SortOrder.Runs);
            resultsModel.UpdateMatchResult(recordInputModel.MatchResult);
            resultsModel.Ground = ground.Value.KnownAs;
            var serviceModel = InitializeSharedServiceModel<BattingBowlingFieldingModel>(recordInputModel, requestDates, matchResult);


            var resultIndBatingDetails =
                Result.Failure<List<IndividualBattingDetails>, Error>("Not initialized");
            var resultPlayerCareerBattingDetails =
                Result.Failure<List<PlayerBattingCareerRecordDetails>, Error>("Not initialized");

            var viewName = "Index";


            switch (recordInputModel.Format)
            {
                case 1:
                    viewName = "BattingOverall";
                    resultsModel.Title = "Overall Figures";

                    resultPlayerCareerBattingDetails = await _battingRecordsRemoteService.GetOverall(serviceModel)
                        .Tap(record => resultsModel.PlayerRecordDetails = record);
                    break;

                case 2: // innings-by-innings
                    viewName = "IndividualInningsList";
                    resultsModel.Title = "Innings by Innings";

                    resultIndBatingDetails = await _battingRecordsRemoteService.GetInningsByInnings(serviceModel)
                        .Tap(record => resultsModel.IndividualBattingDetails = record);
                    break;

                case 3:
                    viewName = "IndividualMatchList";
                    resultsModel.Title = "Match Totals";
                    resultIndBatingDetails = await _battingRecordsRemoteService.GetMatchDetails(serviceModel)
                        .Tap(record => resultsModel.IndividualBattingDetails = record);
                    break;

                case 4:
                    viewName = "SeriesList";
                    resultsModel.Title = "Series Averages";
                    resultPlayerCareerBattingDetails = await _battingRecordsRemoteService
                        .GetRecordsForSeries(serviceModel)
                        .Tap(record => resultsModel.PlayerRecordDetails = record);
                    break;

                case 5:
                    viewName = "GroundList";
                    resultsModel.Title = "Ground Averages";
                    resultPlayerCareerBattingDetails = await _battingRecordsRemoteService
                        .GetRecordsForGrounds(serviceModel)
                        .Tap(record => resultsModel.PlayerRecordDetails = record);
                    break;

                case 6:
                    viewName = "HostCountryList";
                    resultsModel.Title = "By Host Country";
                    resultPlayerCareerBattingDetails = await _battingRecordsRemoteService
                        .GetRecordsForHost(serviceModel)
                        .Tap(record => resultsModel.PlayerRecordDetails = record);
                    break;

                case 7:
                    viewName = "OppositionList";
                    resultsModel.Title = "By Opposition Team";
                    resultPlayerCareerBattingDetails = await _battingRecordsRemoteService.GetRecordsForOpponents(serviceModel)
                        .Tap(record => resultsModel.PlayerRecordDetails = record);
                    break;

                case 8:
                    viewName = "ByYearList";
                    resultsModel.Title = "By Year of Match Start";
                    resultPlayerCareerBattingDetails = await _battingRecordsRemoteService.GetRecordsByYear(serviceModel)
                        .Map(record => resultsModel.PlayerRecordDetails = record);
                    break;

                case 9:
                    viewName = "BySeasonList";
                    resultsModel.Title = "By Season";
                    resultPlayerCareerBattingDetails = await _battingRecordsRemoteService.GetRecordsBySeason(serviceModel)
                        .Tap(record => resultsModel.PlayerRecordDetails = record);
                    break;
            }


            switch (recordInputModel.Format)
            {
                case 2: // innings-by-innings
                case 3:
                    return resultIndBatingDetails
                        .Tap(record =>
                        {
                            resultsModel.MatchType = recordInputModel.MatchType;
                            SetShowTeamsInLists(resultsModel, (TeamId) recordInputModel.TeamId,
                                (TeamId) recordInputModel.OpponentsId,
                                resultsModel.TeamGrouping == "on");
                        })
                        .Match(t => View(viewName, resultsModel), e =>
                        {
                            ModelState.AddModelError("OpponentsId", e.Message);
                            return View("Index");
                        });


                case 1:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return resultPlayerCareerBattingDetails
                        .Tap(record =>
                        {
                            resultsModel.MatchType = recordInputModel.MatchType;
                            SetShowTeamsInLists(resultsModel, (TeamId) recordInputModel.TeamId,
                                (TeamId) recordInputModel.OpponentsId,
                                resultsModel.TeamGrouping == "on");
                        })
                        .Match(t => View(viewName, resultsModel), e =>
                        {
                            ModelState.AddModelError("OpponentsId", e.Message);
                            return View("Index");
                        });

                default:
                    return View("Index");
            }
        }
    }
}