using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;

namespace Repository
{
    public interface IIndividualBattingDetailsRepository : IReadOnlyRepository<IndividualBattingDetails>
    {
        Task<Result<IReadOnlyList<IndividualBattingDetails>, Error>> GetCompleteBattingIndividualMatches(int teamId,
            int opponentsId, string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit,
            int sortBy, string sortDirection);

        Task<Result<IReadOnlyList<IndividualBattingDetails>, Error>> GetCompleteBattingIndividualInnings(int teamId,
            int opponentsId, string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit,
            int sortBy, string sortDirection);

    }
}