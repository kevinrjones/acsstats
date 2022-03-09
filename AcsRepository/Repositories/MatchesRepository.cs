using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcsRepository.Interfaces;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AcsRepository.Repositories;

public class MatchesRepository : BaseEfRepository<Match>, IMatchesRepository
{
    private readonly ILogger<MatchesRepository> _logger;

    public MatchesRepository(AcsDbContext dbContext, ILogger<MatchesRepository> logger) : base(dbContext)
    {
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<MatchDate>, Error>> GetTeamsForMatchType(string matchType)
    {
        try
        {
            var firstMatch = Entities.Where(m => m.MatchType == matchType).OrderBy(m => m.MatchStartDateAsOffset)
                .First();
            var lastMatch = Entities.Where(m => m.MatchType == matchType).OrderBy(m => m.MatchStartDateAsOffset).Last();

            var res = await Task.FromResult(new List<MatchDate>
            {
                new()
                {
                    Date = firstMatch.MatchStartDate, Id = firstMatch.Id, MatchType = matchType,
                    DateOffset = firstMatch.MatchStartDateAsOffset
                },
                new()
                {
                    Date = lastMatch.MatchStartDate, Id = lastMatch.Id, MatchType = matchType,
                    DateOffset = lastMatch.MatchStartDateAsOffset
                }
            });
            return Result.Success<IReadOnlyList<MatchDate>, Error>(res);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {MatchType}", matchType);
            return Result.Failure<IReadOnlyList<MatchDate>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<string>, Error>> GetSeriesDatesForMatchType(string matchType)
    {
        try
        {
            var res = await Entities
                .Where(g => g.MatchType == matchType)
                .Select(g => g.SeriesDate).Distinct().ToListAsync();
            return Result.Success<IReadOnlyList<string>, Error>(res);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {MatchType}", matchType);
            return Result.Failure<IReadOnlyList<string>, Error>(Errors.UnexpectedError);
        }
    }
}