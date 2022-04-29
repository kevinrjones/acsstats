using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using AcsDto.Dtos;
using AcsStatsWeb.AcsHttpClient;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Services.Remote;

public class RemoteScorecardService : IRemoteScorecardsService
{
    class LocalScorecardDto
    {
        public List<string> Notes { get; set; }
        public List<DebutDto> Debuts { get; set; }
        public ScorecardHeaderDto Header { get; set; }
        public List<InningDto> Innings { get; set; }
    }

    private readonly ILogger<RemoteScorecardService> _logger;
    private readonly IHttpClientProxy _httpClientProxy;

    public RemoteScorecardService(ILogger<RemoteScorecardService> logger, IHttpClientProxy httpClientProxy)
    {
        _logger = logger;
        _httpClientProxy = httpClientProxy;
    }

    public async Task<Result<ScorecardDto, Error>> GetScorecard(int scorecardId)
    {
        var url = $"Scorecard/byid/{scorecardId}";
        var res = await _httpClientProxy.GetJsonAsync<LocalScorecardDto>(url);

        return res.Map(it => new ScorecardDto(it.Notes, it.Debuts, it.Header, it.Innings));
    }

    public async Task<Result<ScorecardDto, Error>> GetScorecard(ScorecardSearchTemplate scorecardUrlTemplate)
    {
        var url = $"Scorecard/{HttpUtility.UrlEncode(scorecardUrlTemplate.HomeTeam)}-v-{HttpUtility.UrlEncode(scorecardUrlTemplate.AwayTeam)}-{HttpUtility.UrlEncode(scorecardUrlTemplate.Date)}";

        var res = await _httpClientProxy.GetJsonAsync<LocalScorecardDto>(url);

        return res.Map(it => new ScorecardDto(it.Notes, it.Debuts, it.Header, it.Innings));
    }

}