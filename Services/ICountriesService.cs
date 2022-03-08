using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;

namespace Services
{
    public interface ICountriesService
    {
        public Task<Result<IReadOnlyList<Country>, Error >> GetCountriesForMatchType(string matchType);
        public Task<Result<Country, Error>> getCountryFromId(CountryId id);

        
    }
}