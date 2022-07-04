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
    public class TeamRecordsController : AcsRecordsController
    {
        private readonly IGroundsService _groundsService;
        private readonly IRemoteTeamsService _remoteTeamsService;

        public TeamRecordsController(ILogger<TeamRecordsController> logger,
            IValidation validation,
            IGroundsService groundsService,
            ITeamsService teamsService,
            IRemoteTeamsService remoteTeamsService,
            ICountriesService countriesService) : base(teamsService, countriesService, validation, logger)
        {
            _groundsService = groundsService;
            _remoteTeamsService = remoteTeamsService;
            ViewData["Title"] = "Team Records";
        }


        public IActionResult Index()
        {
            ViewBag.Form = null;
            return View("Index");
        }

        public async Task<IActionResult> TeamRecords(TeamRecordInputModel recordInputModel)
        {
            // if(recordInputModel.Limit == null) {recordInputModel.limit = 0}
            // 0 is the special case of 'all teams'
            ViewBag.Form = "team";
            var inputModelValidation = Validate(recordInputModel);

            if (inputModelValidation.IsFailure)
            {
                ModelState.AddModelError("OpponentsId", "Team and opponents cannot be the same");
                return View("Index");
            }


            var matchResult = MatchResult.Create(recordInputModel.MatchResult).Value;
            var maybeResultsModel = await InitializeResultModel<TeamResultsModel>(recordInputModel);
            var ground = await _groundsService.GetGround(recordInputModel.GroundId);

            if (maybeResultsModel.IsFailure || ground.IsFailure)
            {
                ModelState.AddModelError("OpponentsId", "Team and opponents cannot be the same");
                return View("Index");
            }

            TeamResultsModel resultsModel = maybeResultsModel.Value;

            var requestDates = GetEpochDates(recordInputModel.StartDate, recordInputModel.EndDate);


            recordInputModel.UpdateSortOrder(SortOrder.Runs);

            recordInputModel.UpdateSortOrder(SortOrder.Runs);
            resultsModel.UpdateMatchResult(recordInputModel.MatchResult);
            resultsModel.Ground = ground.Value.KnownAs;
            
            
            SharedModel sharedServiceModel =
                InitializeSharedServiceModel<SharedModel>(recordInputModel, requestDates, matchResult);

            Result<List<MatchRecordDetails>, Error> matchRecordDetails =
                Result.Failure<List<MatchRecordDetails>, Error>("Not initialized");

            Result<List<TeamRecordDetails>, Error> teamRecordDetails =
                Result.Failure<List<TeamRecordDetails>, Error>("Not initialized");

            Result<List<TeamExtrasDetails>, Error> teamExtrasDetails =
                Result.Failure<List<TeamExtrasDetails>, Error>("Not initialized");

            Result<List<InningsExtrasDetails>, Error> inningsExtrasDetails =
                Result.Failure<List<InningsExtrasDetails>, Error>("Not initialized");

            Result<List<Domain.MatchResult>, Error> resultDetails =
                Result.Failure<List<Domain.MatchResult>, Error>("Not initialized");

            string viewName;

            switch (recordInputModel.Format)
            {
                case 1:
                    viewName = "TeamRecords";
                    resultsModel.Title = "Overall Figures";

                    teamRecordDetails = await ground
                        .Bind(async (_) =>
                            await _remoteTeamsService.GetTeamRecords(sharedServiceModel));
                    break;

                case 2:
                    viewName = "HighestTotalsResults";
                    resultsModel.Title = "Innings by Innings List";

                    matchRecordDetails = await ground
                        .Bind(async (_) =>
                            await _remoteTeamsService.GetInningsByInnings(sharedServiceModel));
                    break;

                case 3:
                    viewName = "HighestTotalsResults";
                    resultsModel.Title = "Highest Match Totals for Team";

                    matchRecordDetails = await ground
                        .Bind(async (_) =>
                            await _remoteTeamsService.GetHighestTotals(sharedServiceModel));
                    break;

                case 4:
                    viewName = "MatchResults";
                    resultsModel.Title = "Match Results";

                    resultDetails = await ground
                        .Bind(async (_) =>
                            await _remoteTeamsService.GetMatchResults(sharedServiceModel));
                    break;

                case 5:
                    viewName = "SeriesRecords";
                    resultsModel.Title = "Series Averages";

                    teamRecordDetails = await ground
                        .Bind(async (_) =>
                            await _remoteTeamsService.GetRecordsForSeries(sharedServiceModel));
                    break;

                case 6:
                    viewName = "GroundRecords";
                    resultsModel.Title = "Ground Averages";

                    teamRecordDetails = await ground
                        .Bind(async (_) =>
                            await _remoteTeamsService.GetRecordsForGrounds(sharedServiceModel));
                    break;

                case 7:
                    viewName = "HostCountry";
                    resultsModel.Title = "By Host Country";

                    teamRecordDetails = await ground
                        .Bind(async (_) =>
                            await _remoteTeamsService.GetRecordsForHost(sharedServiceModel));
                    break;

                case 8:
                    viewName = "TeamRecords";
                    resultsModel.Title = "By Opposition Team";

                    teamRecordDetails = await ground
                        .Bind(async (_) =>
                            await _remoteTeamsService.GetRecordsForOpposition(sharedServiceModel));
                    break;

                case 9:
                    viewName = "ByStartYear";
                    resultsModel.Title = "By Year of Match Start";

                    teamRecordDetails = await ground
                        .Bind(async (_) =>
                            await _remoteTeamsService.GetRecordsForYear(sharedServiceModel));
                    break;

                case 10:
                    viewName = "BySeason";
                    resultsModel.Title = "By Season";

                    teamRecordDetails = await ground
                        .Bind(async (_) =>
                            await _remoteTeamsService.GetRecordsForSeason(sharedServiceModel));
                    break;

                case 11:
                    viewName = "ExtrasRecords";
                    resultsModel.Title = "Overall Extras";

                    teamExtrasDetails = await ground
                        .Bind(async (_) =>
                            await _remoteTeamsService.GetOverallExtrasForteam(sharedServiceModel));
                    break;

                case 12:
                    viewName = "InningsExtrasRecords";
                    resultsModel.Title = "Extras by Innings";

                    inningsExtrasDetails = await ground
                        .Bind(async (_) =>
                            await _remoteTeamsService.GetInningsExtrasForteam(sharedServiceModel));
                    break;


                default:
                    return View("Index");
            }

            // todo: Set an error if one of the results is in error (e;g; teamrecorddetails) 
            switch (recordInputModel.Format)
            {
                case 1:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    return teamRecordDetails
                        .Tap(record =>
                        {
                            resultsModel.TeamRecordDetails = record;
                            resultsModel.MatchType = recordInputModel.MatchType;
                            SetShowTeamsInLists(resultsModel, (TeamId)recordInputModel.TeamId,
                                (TeamId)recordInputModel.OpponentsId);
                        })
                        .OnFailure(e => ModelState.AddModelError("OpponentsId", e.Message))
                        .Finally(res =>
                            res.IsSuccess ? View(viewName, resultsModel) : View("Index"));

                case 2:
                case 3:
                    return matchRecordDetails
                        .Tap(record =>
                        {
                            resultsModel.MatchRecordDetails = record;
                            resultsModel.MatchType = recordInputModel.MatchType;
                            SetShowTeamsInLists(resultsModel, (TeamId)recordInputModel.TeamId,
                                (TeamId)recordInputModel.OpponentsId);
                        })
                        .OnFailure(e => ModelState.AddModelError("OpponentsId", e.Message))
                        .Finally(res =>
                            res.IsSuccess ? View(viewName, resultsModel) : View("Index"));

                case 4:
                    return resultDetails
                        .Tap(record =>
                        {
                            resultsModel.ResultDetails = record;
                            resultsModel.MatchType = recordInputModel.MatchType;
                            SetShowTeamsInLists(resultsModel, (TeamId)recordInputModel.TeamId,
                                (TeamId)recordInputModel.OpponentsId);
                        })
                        .OnFailure(e => ModelState.AddModelError("OpponentsId", e.Message))
                        .Finally(res =>
                            res.IsSuccess ? View(viewName, resultsModel) : View("Index"));

                case 11:
                    return teamExtrasDetails
                        .Tap(record =>
                        {
                            resultsModel.TeamExtrasDetails = record;
                            resultsModel.MatchType = recordInputModel.MatchType;
                            SetShowTeamsInLists(resultsModel, (TeamId)recordInputModel.TeamId,
                                (TeamId)recordInputModel.OpponentsId);
                        })
                        .OnFailure(e => ModelState.AddModelError("OpponentsId", e.Message))
                        .Finally(res =>
                            res.IsSuccess ? View(viewName, resultsModel) : View("Index"));
                case 12:
                    return inningsExtrasDetails
                        .Tap(record =>
                        {
                            resultsModel.InningsExtrasDetails = record;
                            resultsModel.MatchType = recordInputModel.MatchType;
                            SetShowTeamsInLists(resultsModel, (TeamId)recordInputModel.TeamId,
                                (TeamId)recordInputModel.OpponentsId);
                        })
                        .OnFailure(e => ModelState.AddModelError("OpponentsId", e.Message))
                        .Finally(res =>
                            res.IsSuccess ? View(viewName, resultsModel) : View("Index"));

                default:
                    return View("Index");
            }
        }
    }
}