using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsTypes.Error;
using CSharpFunctionalExtensions;

namespace Services.Remote;

public interface IRemoteMatchesService
{
    Task<Result<List<string>, Error>> GetSeriesDates(string type);
    Task<Result<List<string>, Error>>  GetTournamentsForSeason(string type, string season);
    Task<Result<List<MatchListDto>, Error>> GetMatchesForTournament(string tournament);
    Task<Result<List<MatchListDto>, Error>> FindMatches(MatchSearchModel matchSearchModel);
}