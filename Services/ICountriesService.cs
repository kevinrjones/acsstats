using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;

namespace Services
{
    public interface ICountriesService
    {
        public Task<Result<IReadOnlyList<CountryDto>, Error >> GetCountriesForMatchType(string matchType);
        public Task<Result<CountryDto, Error>> getCountryFromId(CountryId id);

        
    }
}