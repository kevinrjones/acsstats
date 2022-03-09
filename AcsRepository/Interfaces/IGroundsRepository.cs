using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;

namespace AcsRepository.Interfaces
{
  public interface IGroundsRepository : IRepository<Ground>
  {
    Task<Result<IReadOnlyList<GroundsWithCodes>, Error>> GetGroundsForMatchType(string matchType);
    Task<Result<IReadOnlyList<Country>, Error>> GetCountryForMatchType(string matchType);
    Task<Result<Country, Error>> GetCountryFromId(int id);

  }
}