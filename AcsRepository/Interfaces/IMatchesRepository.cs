using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;

namespace AcsRepository.Interfaces
{
  public interface IMatchesRepository : IRepository<Match>
  {
    Task<Result<IReadOnlyList<MatchDate>, Error>> GetTeamsForMatchType(string matchType);
    Task<Result<IReadOnlyList<string>, Error>> GetSeriesDatesForMatchType(string matchType);
  }
}