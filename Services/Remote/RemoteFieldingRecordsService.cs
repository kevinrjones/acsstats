using System.Collections.Generic;
using System.Threading.Tasks;
using AcsStatsWeb.AcsHttpClient;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services.Remote;

public class RemoteFieldingRecordsService : IRemoteFieldingRecordsService
{
    private readonly IHttpClientProxy _httpClientProxy;

    public RemoteFieldingRecordsService(IHttpClientProxy httpClientProxy)
    {
        _httpClientProxy = httpClientProxy;
    }
        
    public async Task<Result<List<PlayerFieldingCareerRecordDetails>, Error>> GetOverall(SharedModel sharedServiceModel)
    {
        var url =
            $"FieldingRecords/overall/" +
            $"{sharedServiceModel.MatchType.Value}/" +
            $"{sharedServiceModel.TeamId.Value}/" +
            $"{sharedServiceModel.OpponentsId.Value}" +
            $"{sharedServiceModel.BuildQueryString()}";
        return await _httpClientProxy.GetJsonAsync<List<PlayerFieldingCareerRecordDetails>>(url);
    }

    public async Task<Result<List<IndividualFieldingDetails>, Error>> GetInningsByInnings(SharedModel sharedServiceModel)
    {
        var url =
            $"FieldingRecords/inningsbyinnings/" +
            $"{sharedServiceModel.MatchType.Value}/" +
            $"{sharedServiceModel.TeamId.Value}/" +
            $"{sharedServiceModel.OpponentsId.Value}" +
            $"{sharedServiceModel.BuildQueryString()}";
        return await _httpClientProxy.GetJsonAsync<List<IndividualFieldingDetails>>(url);
    }

    public async Task<Result<List<IndividualFieldingDetails>, Error>> GetMatchDetails(SharedModel sharedServiceModel)
    {
        var url =
            $"FieldingRecords/match/" +
            $"{sharedServiceModel.MatchType.Value}/" +
            $"{sharedServiceModel.TeamId.Value}/" +
            $"{sharedServiceModel.OpponentsId.Value}" +
            $"{sharedServiceModel.BuildQueryString()}";
        return await _httpClientProxy.GetJsonAsync<List<IndividualFieldingDetails>>(url);
    }

    public async Task<Result<List<PlayerFieldingCareerRecordDetails>, Error>> GetRecordsForSeries(SharedModel sharedServiceModel)
    {
        var url =
            $"FieldingRecords/series/" +
            $"{sharedServiceModel.MatchType.Value}/" +
            $"{sharedServiceModel.TeamId.Value}/" +
            $"{sharedServiceModel.OpponentsId.Value}" +
            $"{sharedServiceModel.BuildQueryString()}";
        return await _httpClientProxy.GetJsonAsync<List<PlayerFieldingCareerRecordDetails>>(url);
    }

    public async Task<Result<List<PlayerFieldingCareerRecordDetails>, Error>> GetRecordsForGrounds(SharedModel sharedServiceModel)
    {
        var url =
            $"FieldingRecords/grounds/" +
            $"{sharedServiceModel.MatchType.Value}/" +
            $"{sharedServiceModel.TeamId.Value}/" +
            $"{sharedServiceModel.OpponentsId.Value}" +
            $"{sharedServiceModel.BuildQueryString()}";
        return await _httpClientProxy.GetJsonAsync<List<PlayerFieldingCareerRecordDetails>>(url);
    }

    public async Task<Result<List<PlayerFieldingCareerRecordDetails>, Error>> GetRecordsForHost(SharedModel sharedServiceModel)
    {
        var url =
            $"FieldingRecords/host/" +
            $"{sharedServiceModel.MatchType.Value}/" +
            $"{sharedServiceModel.TeamId.Value}/" +
            $"{sharedServiceModel.OpponentsId.Value}" +
            $"{sharedServiceModel.BuildQueryString()}";
        return await _httpClientProxy.GetJsonAsync<List<PlayerFieldingCareerRecordDetails>>(url);
    }

    public async Task<Result<List<PlayerFieldingCareerRecordDetails>, Error>> GetRecordsForOpponents(SharedModel sharedServiceModel)
    {
        var url =
            $"FieldingRecords/opposition/" +
            $"{sharedServiceModel.MatchType.Value}/" +
            $"{sharedServiceModel.TeamId.Value}/" +
            $"{sharedServiceModel.OpponentsId.Value}" +
            $"{sharedServiceModel.BuildQueryString()}";
        return await _httpClientProxy.GetJsonAsync<List<PlayerFieldingCareerRecordDetails>>(url);
    }

    public async Task<Result<List<PlayerFieldingCareerRecordDetails>, Error>> GetRecordsByYear(SharedModel sharedServiceModel)
    {
        var url =
            $"FieldingRecords/year/" +
            $"{sharedServiceModel.MatchType.Value}/" +
            $"{sharedServiceModel.TeamId.Value}/" +
            $"{sharedServiceModel.OpponentsId.Value}" +
            $"{sharedServiceModel.BuildQueryString()}";
        return await _httpClientProxy.GetJsonAsync<List<PlayerFieldingCareerRecordDetails>>(url);
    }

    public async Task<Result<List<PlayerFieldingCareerRecordDetails>, Error>> GetRecordsBySeason(SharedModel sharedServiceModel)
    {
        var url =
            $"FieldingRecords/season/" +
            $"{sharedServiceModel.MatchType.Value}/" +
            $"{sharedServiceModel.TeamId.Value}/" +
            $"{sharedServiceModel.OpponentsId.Value}" +
            $"{sharedServiceModel.BuildQueryString()}";
        return await _httpClientProxy.GetJsonAsync<List<PlayerFieldingCareerRecordDetails>>(url);
    }
}