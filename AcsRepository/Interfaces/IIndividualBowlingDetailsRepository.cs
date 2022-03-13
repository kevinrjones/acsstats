using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;

namespace AcsRepository.Interfaces
{
    public interface IIndividualBowlingDetailsRepository : IReadOnlyRepository<IndividualBowlingDetails>
    {
        Task<Result<IReadOnlyList<IndividualBowlingDetails>, Error>> GetCompleteBowlingIndividualInnings(int teamId,
            int opponentsId, string matchType, int groundId, int hostCountryId, int venueId, long startDate,
            long endDate, string season, int matchResult, int wicketsLimit, int sortBy,
            string sortDirection);

        Task<Result<IReadOnlyList<IndividualBowlingDetails>, Error>> GetCompleteBowlingIndividualMatches(int teamId,
            int opponentsId, string matchType, int groundId, int hostCountryId, int venueId, long startDate,
            long endDate, string season, int matchResult, int wicketsLimit, int sortBy,
            string sortDirection);

    }
}