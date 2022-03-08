using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;
using MatchResult = Domain.MatchResult;

namespace Services
{
    public interface IMatchesService
    {
        Task<CSharpFunctionalExtensions.Result<IReadOnlyList<MatchRecordDetails>, AcsTypes.Error.Error>>
            GetHighestInningsForTeam(SharedModel teamModel);

        Task<CSharpFunctionalExtensions.Result<IReadOnlyList<MatchRecordDetails>, AcsTypes.Error.Error>> GetMatchTotals(
            SharedModel teamModel);

        Task<CSharpFunctionalExtensions.Result<IReadOnlyList<MatchResult>, AcsTypes.Error.Error>> GetMatchResults(
            SharedModel teamModel);

        public Task<Result<IReadOnlyList<MatchDate>, Error>> GetDatesForMatchType(string matchType);

        Task<Result<IReadOnlyList<string>, Error>> GetSeriesDatesForMatchType(string matchType);
    }
}