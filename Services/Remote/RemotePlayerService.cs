using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsStatsWeb.AcsHttpClient;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Services.Remote;

public class RemotePlayerService : IRemotePlayerService
{
    class LocalPlayerBiographyDto
    {
        public List<NameDetail> NameDetails { get; set; }
        
    }

    private readonly ILogger<RemoteScorecardService> _logger;
    private readonly IHttpClientProxy _httpClientProxy;

    public RemotePlayerService(ILogger<RemoteScorecardService> logger, IHttpClientProxy httpClientProxy)
    {
        _logger = logger;
        _httpClientProxy = httpClientProxy;
    }

    public async Task<Result<PlayerBiographyDto, Error>> GetPlayerBiography(int id)
    {
        var url = $"player/biography/{id}";
        var res = await _httpClientProxy.GetJsonAsync<LocalPlayerBiographyDto>(url);
        return res.Map(it => new PlayerBiographyDto(it.NameDetails));
    }

    public async Task<Result<List<PlayerOverallDto>, Error>> GetPlayerOverall(int id)
    {
        var url = $"player/overall/{id}";
        var res= await _httpClientProxy.GetJsonAsync<List<PlayerOverallDto>>(url);
        return res;
    }

    public async Task<Result<Dictionary<string, List<BattingDetailsDto>>, Error>> GetPlayerBattingDetails(int id)
    {
        var url = $"player/battingdetails/{id}";
        return await _httpClientProxy.GetJsonAsync<Dictionary<string, List<BattingDetailsDto>>>(url);
    }

    public async Task<Result<Dictionary<string, List<BowlingDetailsDto>>, Error>> GetPlayerBowlingDetails(int id)
    {
        var url = $"player/bowlingdetails/{id}";
        return await _httpClientProxy.GetJsonAsync<Dictionary<string, List<BowlingDetailsDto>>>(url);
    }
}