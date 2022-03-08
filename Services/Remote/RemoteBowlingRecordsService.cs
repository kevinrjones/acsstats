using System.Collections.Generic;
using System.Threading.Tasks;
using AcsStatsWeb.AcsHttpClient;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services.Remote
{
    public class RemoteBowlingRecordsService : IRemoteBowlingRecordsService
    {
        private readonly IHttpClientProxy _httpClientProxy;

        public RemoteBowlingRecordsService(IHttpClientProxy httpClientProxy)
        {
            _httpClientProxy = httpClientProxy;
        }
        
        public async Task<Result<List<PlayerBowlingCareerRecordDetails>, Error>> GetOverall(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/overall/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBowlingCareerRecordDetails>>(url);
        }

        public async Task<Result<List<IndividualBowlingDetails>, Error>> GetInningsByInnings(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/inningsbyinnings/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<IndividualBowlingDetails>>(url);
        }

        public async Task<Result<List<IndividualBowlingDetails>, Error>> GetMatchDetails(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/match/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<IndividualBowlingDetails>>(url);
        }

        public async Task<Result<List<PlayerBowlingCareerRecordDetails>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/series/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBowlingCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PlayerBowlingCareerRecordDetails>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/grounds/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBowlingCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PlayerBowlingCareerRecordDetails>, Error>> GetRecordsForHost(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/host/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBowlingCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PlayerBowlingCareerRecordDetails>, Error>> GetRecordsForOpponents(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/opposition/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBowlingCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PlayerBowlingCareerRecordDetails>, Error>> GetRecordsByYear(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/year/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBowlingCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PlayerBowlingCareerRecordDetails>, Error>> GetRecordsBySeason(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/season/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBowlingCareerRecordDetails>>(url);
        }
    }
}