using System.Collections.Generic;
using System.Threading.Tasks;
using AcsStatsWeb.AcsHttpClient;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services.Remote
{
    public class RemotePartnershipRecordsService : IRemotePartnershipsRecordsService
    {
        private readonly IHttpClientProxy _httpClientProxy;

        public RemotePartnershipRecordsService(IHttpClientProxy httpClientProxy)
        {
            _httpClientProxy = httpClientProxy;
        }
        
        public async Task<Result<List<PartnershipCareerRecordDetails>, Error>> GetOverall(SharedModel sharedServiceModel)
        {
            var url =
                $"PartnershipRecords/overall/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PartnershipCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PartnershipIndividualRecordDetails>, Error>> GetInningsByInnings(SharedModel sharedServiceModel)
        {
            var url =
                $"PartnershipRecords/inningsbyinnings/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PartnershipIndividualRecordDetails>>(url);
        }

        public async Task<Result<List<PartnershipIndividualRecordDetails>, Error>> GetMatchDetails(SharedModel sharedServiceModel)
        {
            var url =
                $"PartnershipRecords/match/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PartnershipIndividualRecordDetails>>(url);
        }

        public async Task<Result<List<PartnershipCareerRecordDetails>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel)
        {
            var url =
                $"PartnershipRecords/series/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PartnershipCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PartnershipCareerRecordDetails>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel)
        {
            var url =
                $"PartnershipRecords/grounds/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PartnershipCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PartnershipCareerRecordDetails>, Error>> GetRecordsForHost(SharedModel sharedServiceModel)
        {
            var url =
                $"PartnershipRecords/host/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PartnershipCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PartnershipCareerRecordDetails>, Error>> GetRecordsForOpponents(SharedModel sharedServiceModel)
        {
            var url =
                $"PartnershipRecords/opposition/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PartnershipCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PartnershipCareerRecordDetails>, Error>> GetRecordsByYear(SharedModel sharedServiceModel)
        {
            var url =
                $"PartnershipRecords/year/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PartnershipCareerRecordDetails>>(url);
        }

        public async Task<Result<List<PartnershipCareerRecordDetails>, Error>> GetRecordsBySeason(SharedModel sharedServiceModel)
        {
            var url =
                $"PartnershipRecords/season/" +
                $"{sharedServiceModel.MatchType.Value}/" +
                $"{sharedServiceModel.TeamId.Value}/" +
                $"{sharedServiceModel.OpponentsId.Value}" +
                $"{sharedServiceModel.BuildQueryString()}";
            return await _httpClientProxy.GetJsonAsync<List<PartnershipCareerRecordDetails>>(url);
        }
    }
}