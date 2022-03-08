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

public class GroundsRepository : BaseEfRepository<Ground, int>, IGroundsRepository
{
    private readonly ILogger<GroundsRepository> _logger;
    private readonly AcsDbContext _acsDbContext;

    public GroundsRepository(AcsDbContext dbContext, ILogger<GroundsRepository> logger) : base(dbContext)
    {
        _acsDbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<GroundsWithCodes>, Error>> GetGroundsForMatchType(string matchType)
    {
        try
        {
            var res = await (from g in Entities
                join cc in _acsDbContext.CountryCodes
                    on g.CountryName equals cc.Country
                orderby g.CountryName, g.KnownAs
                where g.MatchType == matchType
                select new GroundsWithCodes
                {
                    Id = g.Id, MatchType = matchType, Code = cc.Code, CountryName = cc.Country,
                    GroundId = g.GroundId, KnownAs = g.KnownAs
                }).ToListAsync();

            return Result.Success<IReadOnlyList<GroundsWithCodes>, Error>(res);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {MatchType}", matchType);
            return Result.Failure<IReadOnlyList<GroundsWithCodes>, Error>(Errors.UnexpectedError);
        }
    }
    
    public async Task<Result<IReadOnlyList<Country>, Error>> GetCountryForMatchType(string matchType)
    {
        try
        {
            var res = await Entities
                .Where(g => g.MatchType == matchType)
                .Select(g => new Country {Id = g.CountryId, Name = g.CountryName, MatchType = matchType}).Distinct()
                .ToListAsync();

            return Result.Success<IReadOnlyList<Country>, Error>(res);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {MatchType}", matchType);
            return Result.Failure<IReadOnlyList<Country>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<Country, Error>> GetCountryFromId(int id)
    {
        try
        {
            var ground = Entities.First(g => g.CountryId == id);
            return new Country {Id = id, Name = ground.CountryName, MatchType = ""};
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {id}", id);
            return Result.Failure<Country, Error>(Errors.UnexpectedError);
        }
    }
}