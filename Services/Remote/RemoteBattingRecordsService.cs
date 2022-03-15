using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsStatsWeb.AcsHttpClient;
using AcsStatsWeb.Dtos;
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
        
        public async Task<Result<List<PlayerBattingRecordDto>, Error>> GetOverall(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/overall/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBattingRecordDto>>(url);
        }

        public async Task<Result<List<IndividualBattingDetailsDto>, Error>> GetInningsByInnings(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/inningsbyinnings/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<IndividualBattingDetailsDto>>(url);
        }

        public async Task<Result<List<IndividualBattingDetailsDto>, Error>> GetMatchDetails(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/match/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<IndividualBattingDetailsDto>>(url);
        }

        public async Task<Result<List<PlayerBattingRecordDto>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/series/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBattingRecordDto>>(url);
        }

        public async Task<Result<List<PlayerBattingRecordDto>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/grounds/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBattingRecordDto>>(url);
        }

        public async Task<Result<List<PlayerBattingRecordDto>, Error>> GetRecordsForHost(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/host/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBattingRecordDto>>(url);
        }

        public async Task<Result<List<PlayerBattingRecordDto>, Error>> GetRecordsForOpponents(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/opposition/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBattingRecordDto>>(url);
        }

        public async Task<Result<List<PlayerBattingRecordDto>, Error>> GetRecordsByYear(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/year/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBattingRecordDto>>(url);
        }

        public async Task<Result<List<PlayerBattingRecordDto>, Error>> GetRecordsBySeason(SharedModel sharedServiceModel)
        {
            var url =
                $"BattingRecords/season/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PlayerBattingRecordDto>>(url);
        }
    }
}