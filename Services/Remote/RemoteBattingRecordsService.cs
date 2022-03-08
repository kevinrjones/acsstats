using System.Collections.Generic;
using System.Threading.Tasks;
using AcsStatsWeb.AcsHttpClient;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services.Remote
{
    public class RemoteBattingRecordsService : IRemoteBattingRecordsService
    {
        private readonly IHttpClientProxy _httpClientProxy;

        public RemoteBattingRecordsService(IHttpClientProxy httpClientProxy)
        {
            _httpClientProxy = httpClientProxy;
        }
        
        public async Task<Result<List<PlayerBattingCareerRecordDetails>, Error>> GetOverall(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/overall/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBattingCareerRecordDetails>>(url);
        }

        public async Task<Result<List<IndividualBattingDetails>, Error>> GetInningsByInnings(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/inningsbyinnings/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<IndividualBattingDetails>>(url);
        }

        public async Task<Result<List<IndividualBattingDetails>, Error>> GetMatchDetails(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/match/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<IndividualBattingDetails>>(url);
        }

        public async Task<Result<List<PlayerBattingCareerRecordDetails>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/series/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBattingCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PlayerBattingCareerRecordDetails>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/grounds/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBattingCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PlayerBattingCareerRecordDetails>, Error>> GetRecordsForHost(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/host/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBattingCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PlayerBattingCareerRecordDetails>, Error>> GetRecordsForOpponents(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/opposition/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBattingCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PlayerBattingCareerRecordDetails>, Error>> GetRecordsByYear(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/year/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBattingCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PlayerBattingCareerRecordDetails>, Error>> GetRecordsBySeason(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/season/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBattingCareerRecordDetails>>(url);
        }
    }
}