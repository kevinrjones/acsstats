using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;

namespace AcsRepository.Interfaces
{
    public interface ITeamsRepository : IRepository<Team>
    {
        // Task<Result<IReadOnlyList<Team>, Error>> GetTeamsForMatchType(string matchType);
    }
}