using System.Collections.Generic;
using System.Threading.Tasks;
using AcsRepository;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;

namespace Services.AcsServices
{
    public class CountriesService : ICountriesService
    {
        private readonly IEfUnitOfWork _unitOfWork;

        public CountriesService(IEfUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<Country>, Error >> GetCountriesForMatchType(string matchType)
        {
            return (await _unitOfWork.GroundsRepository.GetCountryForMatchType(matchType)).Map(r => r);
        }

        public async Task<Result<Country, Error>> getCountryFromId(CountryId id)
        {
            return await _unitOfWork.GroundsRepository.GetCountryFromId(id);
        }
    }
}