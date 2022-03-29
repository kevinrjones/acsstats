using System.Collections.Generic;
using System.Threading.Tasks;
using AcsCommands.Query;
using AcsDto.Dtos;
using AcsRepository;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using MediatR;

namespace Services.AcsServices
{
    public class CountriesService : ICountriesService
    {
        private readonly IMediator _mediator;

        public CountriesService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<IReadOnlyList<CountryDto>, Error >> GetCountriesForMatchType(string matchType)
        {
            var m = MatchType.Create(matchType);

            if (m.IsSuccess)
            {
                return await _mediator.Send(new CountryForMatchTypeQuery(m.Value));
            }

            return Result.Failure<IReadOnlyList<CountryDto>, Error>(m.Error);
        }

        public async Task<Result<CountryDto, Error>> getCountryFromId(CountryId id)
        {
            return await _mediator.Send(new CountryQuery(id));
        }
    }
}