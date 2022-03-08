using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;

namespace Repository
{
  public interface IMatchesRepository : IRepository<Match>
  {
    Task<Result<IReadOnlyList<MatchDate>, Error>> GetTeamsForMatchType(string matchType);
    Task<Result<IReadOnlyList<string>, Error>> GetSeriesDatesForMatchType(string matchType);
  }
}