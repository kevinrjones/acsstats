using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsStatsWeb.AcsHttpClient;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Services.Remote;

public class RemoteMatchesService : IRemoteMatchesService
{
    private readonly ILogger<RemoteScorecardService> _logger;
    private readonly IHttpClientProxy _httpClientProxy;

    public RemoteMatchesService(ILogger<RemoteScorecardService> logger, IHttpClientProxy httpClientProxy)
    {
        _logger = logger;
        _httpClientProxy = httpClientProxy;
    }

    public async Task<Result<List<string>, Error>> GetSeriesDates(string type)
    {
        var url = $"Matches/seriesdatesformatchtypes?";
        if (type == "women")
        {
            url += "matchtypes=wt&matchtypes=wf&matchtypes=wo&matchtypes=wa&matchtypes=wtt&matchtypes=witt";
        }
        else if (type == "men")
        {
            url += "matchtypes=t&matchtypes=f&matchtypes=o&matchtypes=a&matchtypes=tt&matchtypes=itt";
        }


        return await _httpClientProxy.GetJsonAsync<List<string>>(url);
    }

    public async Task<Result<List<string>, Error>> GetTournamentsForSeason(string type, string season)
    {
        var url = $"Matches/tournamentsforseason/{season}?";
        if (type == "women")
        {
            url += "matchtypes=wt&matchtypes=wf&matchtypes=wo&matchtypes=wa&matchtypes=wtt&matchtypes=witt";
        }
        else if (type == "men")
        {
            url += "matchtypes=t&matchtypes=f&matchtypes=o&matchtypes=a&matchtypes=tt&matchtypes=itt";
        }

        return await _httpClientProxy.GetJsonAsync<List<string>>(url);
    }

    public async Task<Result<List<MatchListDto>, Error>> GetMatchesForTournament(string tournament)
    {
        var url = $"Matches/matchesintournament/{UrlEncoder.Default.Encode(tournament)}";
        return await _httpClientProxy.GetJsonAsync<List<MatchListDto>>(url);
    }

    public async Task<Result<List<MatchListDto>, Error>> FindMatches(MatchSearchModel matchSearchModel)
    {
        var url = $"Matches/findmatches?";

        if (!string.IsNullOrEmpty(matchSearchModel.HomeTeam))
            url += $"homeTeam={matchSearchModel.HomeTeam}&";

        if (!string.IsNullOrEmpty(matchSearchModel.AwayTeam))
            url += $"awayTeam={matchSearchModel.AwayTeam}&";

        if (!string.IsNullOrEmpty(matchSearchModel.StartDate))
            url += $"startDate={matchSearchModel.StartDate}&";

        if (!string.IsNullOrEmpty(matchSearchModel.EndDate))
            url += $"endDate={matchSearchModel.EndDate}&";

        if (matchSearchModel.MatchType != null)
            url += $"matchType={matchSearchModel.MatchType}&";

        url += $"exacthometeammatch={matchSearchModel.ExactHomeTeamMatch}&";
        url += $"exactawayteammatch={matchSearchModel.ExactAwayTeamMatch}&";


        if (matchSearchModel.Venue != null)
            foreach (var venue in matchSearchModel.Venue)
            {
                url += $"venue={venue}&";
            }

        url += $"matchResult={matchSearchModel.MatchResult}&";

        url = url.Trim('&');

        return await _httpClientProxy.GetJsonAsync<List<MatchListDto>>(url);
    }

    public async Task<Result<List<PlayerListDto>, Error>> FindPlayers(PlayerSearchModel playerSearchModel)
    {
        var url = $"Player/findplayers?";

        if (!string.IsNullOrEmpty(playerSearchModel.Name))
            url += $"name={playerSearchModel.Name}&";

        if (!string.IsNullOrEmpty(playerSearchModel.Team))
            url += $"team={playerSearchModel.Team}&";
        
        url += $"exactnamematch={playerSearchModel.ExactNameMatch}&";

        
        if (!string.IsNullOrEmpty(playerSearchModel.DebutDate))
            url += $"debutDate={playerSearchModel.DebutDate}&";

        if (!string.IsNullOrEmpty(playerSearchModel.ActiveUntil))
            url += $"activeUntil={playerSearchModel.ActiveUntil}&";

        url = url.Trim('&');

        return await _httpClientProxy.GetJsonAsync<List<PlayerListDto>>(url);
    }
}