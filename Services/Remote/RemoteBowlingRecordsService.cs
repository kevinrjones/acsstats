using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsDto.Models;
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
        
        public async Task<Result<List<BowlingCareerRecordDto>, Error>> GetOverall(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/overall/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<BowlingCareerRecordDto>>(url);
        }

        public async Task<Result<List<IndividualBowlingDetailsDto>, Error>> GetInningsByInnings(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/inningsbyinnings/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<IndividualBowlingDetailsDto>>(url);
        }

        public async Task<Result<List<IndividualBowlingDetailsDto>, Error>> GetMatchDetails(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/match/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<IndividualBowlingDetailsDto>>(url);
        }

        public async Task<Result<List<BowlingCareerRecordDto>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/series/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<BowlingCareerRecordDto>>(url);
        }

        public async Task<Result<List<BowlingCareerRecordDto>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/grounds/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<BowlingCareerRecordDto>>(url);
        }

        public async Task<Result<List<BowlingCareerRecordDto>, Error>> GetRecordsForHost(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/host/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<BowlingCareerRecordDto>>(url);
        }

        public async Task<Result<List<BowlingCareerRecordDto>, Error>> GetRecordsForOpponents(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/opposition/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<BowlingCareerRecordDto>>(url);
        }

        public async Task<Result<List<BowlingCareerRecordDto>, Error>> GetRecordsByYear(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/year/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<BowlingCareerRecordDto>>(url);
        }

        public async Task<Result<List<BowlingCareerRecordDto>, Error>> GetRecordsBySeason(SharedModel sharedServiceModel)
        {
            var url =
                $"BowlingRecords/season/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<BowlingCareerRecordDto>>(url);
        }
    }
}