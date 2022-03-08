using System;
using System.Collections.Generic;
using System.Linq;
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

namespace AcsStatsWeb.Controllers
{
    public class PartnershipRecordsController : AcsRecordsController
    {
        private readonly IRemotePartnershipsRecordsService _remotePartnershipsRecordsService;
        private readonly IGroundsService _groundsService;

        public PartnershipRecordsController(ILogger<PartnershipRecordsController> logger,
            IValidation validation,
            IRemotePartnershipsRecordsService remotePartnershipsRecordsService,
            IGroundsService groundsService,
            ITeamsService teamsService, ICountriesService countriesService) : base(teamsService, countriesService,
            validation, logger)
        {
            _remotePartnershipsRecordsService = remotePartnershipsRecordsService;
            _groundsService = groundsService;
            ViewData["Title"] = "Bowling Records";
        }


        public IActionResult Index()
        {
            ViewBag.Form = null;
            return View("Index");
        }

        public async Task<IActionResult> IndividualPartnershipRecords(RecordInputModel recordInputModel)
        {
            ViewBag.Form = "partnerships";
            var inputModelValidation = Validate(recordInputModel);

            if (inputModelValidation.IsFailure)
            {
                ModelState.AddModelError("OpponentsId", "Team and opponents cannot be the same");
                return View("Index");
            }


            var matchResult = MatchResult.Create(recordInputModel.MatchResult).Value;
            var maybeResultsModel = await InitializeResultModel<ResultsPartnershipModel>(recordInputModel);
            var ground = await _groundsService.getGround(recordInputModel.GroundId);

            if (maybeResultsModel.IsFailure || ground.IsFailure)
            {
                ModelState.AddModelError("OpponentsId", "Team and opponents cannot be the same");
                return View("Index");
            }

            ResultsPartnershipModel resultsModel = maybeResultsModel.Value;

            var requestDates = GetEpochDates(recordInputModel.StartDate, recordInputModel.EndDate);


            recordInputModel.UpdateSortOrder(SortOrder.Runs);

            recordInputModel.UpdateSortOrder(SortOrder.Runs);
            resultsModel.UpdateMatchResult(recordInputModel.MatchResult);
            resultsModel.Ground = ground.Value.KnownAs;
            
            var serviceModel =
                InitializeSharedServiceModel<PartnershipModel>(recordInputModel, requestDates, matchResult);

            Result<IEnumerable<PartnershipCareerRecordDetailsDisplay>, Error> resultPartnershipDetails =
                Result.Failure<IEnumerable<PartnershipCareerRecordDetailsDisplay>, Error>("Not initialized");
            Result<IEnumerable<PartnershipIndividualRecordDetailsDisplay>, Error> resultPlayerCareerPartnershipDetails =
                Result.Failure<IEnumerable<PartnershipIndividualRecordDetailsDisplay>, Error>("Not initialized");

            var viewName = "Index";

            switch (recordInputModel.Format)
            {
                case 1:
                    viewName = "PartnershipOverall";
                    resultsModel.Title = "Overall Figures";
                    resultPartnershipDetails = await _remotePartnershipsRecordsService.GetOverall(serviceModel)
                        .Map(value => value.Map(it => new PartnershipCareerRecordDetailsDisplay(it)));
                    break;

                case 2:
                    viewName = "IndividualInningsList";
                    resultsModel.Title = "Innings by Innings";
                    resultPlayerCareerPartnershipDetails = await _remotePartnershipsRecordsService.GetInningsByInnings(serviceModel)
                        .Map(value => value.Map(it => new PartnershipIndividualRecordDetailsDisplay(it)));
                    break;

                case 3:
                    viewName = "IndividualMatchList";
                    resultsModel.Title = "Match Totals";
                    resultPlayerCareerPartnershipDetails = await _remotePartnershipsRecordsService.GetMatchDetails(serviceModel)
                        .Map(value => value.Map(it => new PartnershipIndividualRecordDetailsDisplay(it)));
                    break;

                case 4:
                    viewName = "SeriesList";
                    resultsModel.Title = "Series Averages";
                    resultPartnershipDetails = await _remotePartnershipsRecordsService.GetRecordsForSeries(serviceModel)
                        .Map(value => value.Map(it => new PartnershipCareerRecordDetailsDisplay(it)));
                    break;

                case 5:
                    viewName = "GroundList";
                    resultsModel.Title = "Ground Averages";
                    resultPartnershipDetails = await _remotePartnershipsRecordsService.GetRecordsForGrounds(serviceModel)
                        .Map(value => value.Map(it => new PartnershipCareerRecordDetailsDisplay(it)));
                    break;

                case 6:
                    viewName = "HostCountryList";
                    resultsModel.Title = "By Host Country";
                    resultPartnershipDetails = await _remotePartnershipsRecordsService.GetRecordsForHost(serviceModel)
                        .Map(value => value.Map(it => new PartnershipCareerRecordDetailsDisplay(it)));
                    break;

                case 7:
                    viewName = "OppositionList";
                    resultsModel.Title = "By Opposition Team";
                    resultPartnershipDetails = await _remotePartnershipsRecordsService.GetRecordsForOpponents(serviceModel)
                        .Map(value => value.Map(it => new PartnershipCareerRecordDetailsDisplay(it)));
                    break;

                case 8:
                    viewName = "ByYearList";
                    resultsModel.Title = "By Year of Match Start";
                    resultPartnershipDetails = await _remotePartnershipsRecordsService.GetRecordsByYear(serviceModel)
                        .Map(value => value.Map(it => new PartnershipCareerRecordDetailsDisplay(it)));
                    break;

                case 9:
                    viewName = "BySeasonList";
                    resultsModel.Title = "By Season";
                    resultPartnershipDetails = await _remotePartnershipsRecordsService.GetRecordsBySeason(serviceModel)
                        .Map(value => value.Map(
                            it => { return new PartnershipCareerRecordDetailsDisplay(it); }));
                    break;
            }

            switch (recordInputModel.Format)
            {
                case 2:
                case 3:
                    return resultPlayerCareerPartnershipDetails.Tap(record =>
                        {
                            resultsModel.IndividualPartnershipDetails = record.ToList();
                            resultsModel.MatchType = recordInputModel.MatchType;
                            SetShowTeamsInLists(resultsModel, (TeamId)recordInputModel.TeamId,
                                (TeamId)recordInputModel.OpponentsId,
                                resultsModel.TeamGrouping == "on");
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
                    return resultPartnershipDetails.Tap(record =>
                        {
                            resultsModel.PartnershipDetails = record.ToList();
                            resultsModel.MatchType = recordInputModel.MatchType;
                            SetShowTeamsInLists(resultsModel, (TeamId)recordInputModel.TeamId,
                                (TeamId)recordInputModel.OpponentsId,
                                resultsModel.TeamGrouping == "on");
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