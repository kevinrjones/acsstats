using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;

namespace Repository
{
    public interface IIndividualFieldingDetailsRepository : IReadOnlyRepository<IndividualFieldingDetails>
    {
        Task<Result<IReadOnlyList<IndividualFieldingDetails>, Error>> GetCompleteFieldingIndividualInnings(int teamId,
            int opponentsId, string matchType, int groundId, int hostCountryId, int venueId, long startDate,
            long endDate, string season, int matchResult, int wicketsLimit, int sortBy,
            string sortDirection);

        Task<Result<IReadOnlyList<IndividualFieldingDetails>, Error>> GetCompleteFieldingIndividualMatches(int teamId,
            int opponentsId, string matchType, int groundId, int hostCountryId, int venueId, long startDate,
            long endDate, string season, int matchResult, int wicketsLimit, int sortBy,
            string sortDirection);
        
    }
}