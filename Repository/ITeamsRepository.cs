using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;

namespace Repository
{
    public interface ITeamsRepository : IRepository<Team>
    {
        Task<Result<IReadOnlyList<Team>, Error>> GetTeamsForMatchType(string matchType);
    }
}