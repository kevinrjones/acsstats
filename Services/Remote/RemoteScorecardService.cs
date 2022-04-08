using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsStatsWeb.AcsHttpClient;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Services.Remote;

public class RemoteScorecardService : IRemoteScorecardsService
{
    class LocalScorecardDto
    {
        public List<string> Notes { get; set; }
        public PersonDto[] Debuts { get; set; }
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
        var url = $"Scorecard/{scorecardId}";
        var res = await _httpClientProxy.GetJsonAsync<LocalScorecardDto>(url);

        return res.Map(it => new ScorecardDto(it.Notes, it.Debuts, it.Header, it.Innings));
    }
}