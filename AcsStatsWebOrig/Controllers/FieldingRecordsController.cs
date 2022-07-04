using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class FieldingRecordsController : AcsRecordsController
    {
        private readonly IPlayersService _playersService;
        private readonly IGroundsService _groundsService;
        private readonly IRemoteFieldingRecordsService _fieldingRecordsRemoteService;

        public FieldingRecordsController(ILogger<FieldingRecordsController> logger,
            IValidation validation,
            IRemoteFieldingRecordsService fieldingRecordsRemoteService,
            IPlayersService playersService,
            IGroundsService groundsService,
            ITeamsService teamsService, ICountriesService countriesService) : base(teamsService, countriesService,
            validation, logger)
        {
            _fieldingRecordsRemoteService = fieldingRecordsRemoteService;
            _playersService = playersService;
            _groundsService = groundsService;
            ViewData["Title"] = "Fielding Records";
        }


        public IActionResult Index()
        {
            ViewBag.Form = null;
            return View("Index");
        }

        public async Task<IActionResult> IndividualFieldingRecords(RecordInputModel recordInputModel)
        {
            ViewBag.Form = "fielding";
            var inputModelValidation = Validate(recordInputModel);

            if (inputModelValidation.IsFailure)
            {
                ModelState.AddModelError("OpponentsId", "Team and opponents cannot be the same");
                return View("Index");
            }


            var matchResult = MatchResult.Create(recordInputModel.MatchResult).Value;
            var maybeResultsModel = await InitializeResultModel<ResultsFieldingModel>(recordInputModel);
            var ground = await _groundsService.GetGround(recordInputModel.GroundId);

            if (maybeResultsModel.IsFailure || ground.IsFailure)
            {
                ModelState.AddModelError("OpponentsId", "Team and opponents cannot be the same");
                return View("Index");
            }

            ResultsFieldingModel resultsModel = maybeResultsModel.Value;

            var requestDates = GetEpochDates(recordInputModel.StartDate, recordInputModel.EndDate);


            recordInputModel.UpdateSortOrder(SortOrder.Dismissals);
            
            resultsModel.UpdateMatchResult(recordInputModel.MatchResult);
            resultsModel.Ground = ground.Value.KnownAs;
            
            var serviceModel =
                InitializeSharedServiceModel<BattingBowlingFieldingModel>(recordInputModel, requestDates, matchResult);
            Result<List<IndividualFieldingDetails>, Error> resultIndFieldingDetails =
                Result.Failure<List<IndividualFieldingDetails>, Error>("Not initialized");
            Result<List<PlayerFieldingCareerRecordDetails>, Error>
                resultPlayerCareerFieldingDetails =
                    Result.Failure<List<PlayerFieldingCareerRecordDetails>, Error>("Not initialized");

            var viewName = "Index";
            
            switch (recordInputModel.Format)
            {
                case 1:
                    viewName = "FieldingOverall";
                    resultsModel.Title = "Overall Figures";
                    resultPlayerCareerFieldingDetails = await  _fieldingRecordsRemoteService.GetOverall(serviceModel);
                    break;

                case 2: // innings-by-innings
                    viewName = "IndividualFieldingList";
                    resultsModel.Title = "Innings by Innings";
                    resultIndFieldingDetails = await _fieldingRecordsRemoteService.GetInningsByInnings(serviceModel);
                    break;

                case 3:
                    viewName = "IndividualMatchList";
                    resultsModel.Title = "Match Totals";
                    resultIndFieldingDetails = await _fieldingRecordsRemoteService.GetMatchDetails(serviceModel);
                    break;

                case 4:
                    viewName = "SeriesList";
                    resultsModel.Title = "Series Averages";
                    resultPlayerCareerFieldingDetails = await _fieldingRecordsRemoteService.GetRecordsForSeries(serviceModel);
                    break;

                case 5:
                    viewName = "GroundList";
                    resultsModel.Title = "Ground Averages";
                    resultPlayerCareerFieldingDetails = await _fieldingRecordsRemoteService.GetRecordsForGrounds(serviceModel);
                    break;

                case 6:
                    viewName = "HostCountryList";
                    resultsModel.Title = "By Host Country";
                    resultPlayerCareerFieldingDetails = await _fieldingRecordsRemoteService.GetRecordsForHost(serviceModel);
                    break;

                case 7:
                    viewName = "OppositionList";
                    resultsModel.Title = "By Opposition Team";
                    resultPlayerCareerFieldingDetails = await _fieldingRecordsRemoteService.GetRecordsForOpponents(serviceModel);
                    break;

                case 8:
                    viewName = "ByYearList";
                    resultsModel.Title = "By Year of Match Start";
                    resultPlayerCareerFieldingDetails = await _fieldingRecordsRemoteService.GetRecordsByYear(serviceModel);
                    break;

                case 9:
                    viewName = "BySeasonList";
                    resultsModel.Title = "By Season";
                    resultPlayerCareerFieldingDetails = await _fieldingRecordsRemoteService.GetRecordsBySeason(serviceModel);
                    break;
            }

            switch (recordInputModel.Format)
            {
                case 2: // innings-by-innings
                case 3:
                    return resultIndFieldingDetails
                        .Tap(record =>
                        {
                            resultsModel.IndividualFieldingDetails = record;
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
                    return resultPlayerCareerFieldingDetails
                        .Tap(record =>
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