using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;

namespace Services
{
  public interface IGroundsService
  {
    public Task<Result<IEnumerable<GroundsWithCodes>, Error>> GetGroundsForMatchType(MatchType matchType);
    public Task<Result<Ground, Error>> getGround(int id);
  }
}