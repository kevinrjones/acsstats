using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Models;
using AcsStatsWeb.AcsHttpClient;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.Extensions.Logging;
using Services.Models;

namespace Services.Remote
{
    public class RemoteTeamsService : BaseRemoteService, IRemoteTeamsService
    {
        private readonly IHttpClientProxy _httpClientProxy;

        public RemoteTeamsService(IHttpClientProxy httpClientProxy, ILogger<BaseRemoteService> logger) : base(logger)
        {
            _httpClientProxy = httpClientProxy;
        }

        
        public async Task<Result<List<TeamRecordDetails>, Error>> GetTeamRecords(SharedModel sharedServiceModel)
        {
            var url = BuildUrl(sharedServiceModel, "TeamRecords/overall");
            return await _httpClientProxy.GetJsonAsync<List<TeamRecordDetails>>(url);
        }

        public async Task<Result<List<MatchRecordDetails>, Error>> GetInningsByInnings(SharedModel sharedServiceModel)
        {
            var url = BuildUrl(sharedServiceModel, "TeamRecords/inningsbyinings");
            return await _httpClientProxy.GetJsonAsync<List<MatchRecordDetails>>(url);
        }

        public async Task<Result<List<MatchRecordDetails>, Error>> GetHighestTotals(SharedModel sharedServiceModel)
        {
            var url = BuildUrl(sharedServiceModel, "TeamRecords/highesttotals");            
            return await _httpClientProxy.GetJsonAsync<List<MatchRecordDetails>>(url);
        }

        public async Task<Result<List<MatchResult>, Error>> GetMatchResults(SharedModel sharedServiceModel)
        {
            var url = BuildUrl(sharedServiceModel, "TeamRecords/matchresults");
            return await _httpClientProxy.GetJsonAsync<List<MatchResult>>(url);
        }

        public async Task<Result<List<TeamRecordDetails>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel)
        {
            var url = BuildUrl(sharedServiceModel, "TeamRecords/series");
            return await _httpClientProxy.GetJsonAsync<List<TeamRecordDetails>>(url);
        }

        public async Task<Result<List<TeamRecordDetails>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel)
        {
            var url = BuildUrl(sharedServiceModel, "TeamRecords/grounds");
            return await _httpClientProxy.GetJsonAsync<List<TeamRecordDetails>>(url);
        }

        public async Task<Result<List<TeamRecordDetails>, Error>> GetRecordsForHost(SharedModel sharedServiceModel)
        {
            var url = BuildUrl(sharedServiceModel, "TeamRecords/host");
            return await _httpClientProxy.GetJsonAsync<List<TeamRecordDetails>>(url);
        }

        public async Task<Result<List<TeamRecordDetails>, Error>> GetRecordsForOpposition(SharedModel sharedServiceModel)
        {
            var url = BuildUrl(sharedServiceModel, "TeamRecords/opposition");
            return await _httpClientProxy.GetJsonAsync<List<TeamRecordDetails>>(url);
        }

        public async Task<Result<List<TeamRecordDetails>, Error>> GetRecordsForYear(SharedModel sharedServiceModel)
        {
            var url = BuildUrl(sharedServiceModel, "TeamRecords/year");
            return await _httpClientProxy.GetJsonAsync<List<TeamRecordDetails>>(url);
        }

        public async Task<Result<List<TeamRecordDetails>, Error>> GetRecordsForSeason(SharedModel sharedServiceModel)
        {
            var url = BuildUrl(sharedServiceModel, "TeamRecords/season");
            return await _httpClientProxy.GetJsonAsync<List<TeamRecordDetails>>(url);

        }

        public async Task<Result<List<TeamExtrasDetails>, Error>> GetOverallExtrasForteam(SharedModel sharedServiceModel)
        {
            var url = BuildUrl(sharedServiceModel, "TeamRecords/extras/overall");
            return await _httpClientProxy.GetJsonAsync<List<TeamExtrasDetails>>(url);
        }

        public async Task<Result<List<InningsExtrasDetails>, Error>> GetInningsExtrasForteam(SharedModel sharedServiceModel)
        {
            var url = BuildUrl(sharedServiceModel, "TeamRecords/extras/innings");
            return await _httpClientProxy.GetJsonAsync<List<InningsExtrasDetails>>(url);
        }
    }
}