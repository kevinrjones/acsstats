using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository;

namespace AcsRepository.Repositories;

public class TeamsRepository : BaseEfRepository<Team, int>, ITeamsRepository
{
    private readonly ILogger<TeamsRepository> _logger;

    public TeamsRepository(AcsDbContext dbContext, ILogger<TeamsRepository> logger) : base(dbContext)
    {
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<Team>, Error>> GetTeamsForMatchType(string matchType)
    {
        try
        {
            var res = await Entities.Select(e => e)
                .Where(e => e.MatchType == matchType)
                .Distinct()
                .OrderBy(t => t.Name)
                .ToListAsync();
            return Result.Success<IReadOnlyList<Team>, Error>(res);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}",
                matchType
            );
            return Result.Failure<IReadOnlyList<Team>, Error>(Errors.UnexpectedError);
        }
    }
}