using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository;

// ReSharper disable FormatStringProblem

namespace AcsRepository.Repositories;

public class IndividualBowlingDetailsRepository : BaseEfNoKeyRepository<IndividualBowlingDetails>,
    IIndividualBowlingDetailsRepository
{
    private readonly ILogger<IndividualBowlingDetailsRepository> _logger;

    public IndividualBowlingDetailsRepository(AcsDbContext dbContext,
        ILogger<IndividualBowlingDetailsRepository> logger)
        : base(dbContext)
    {
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<IndividualBowlingDetails>, Error>> GetCompleteBowlingIndividualInnings(
        int teamId,
        int opponentsId, string matchType, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int wicketsLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            return await DbContext.PlayerIndividualBowlingDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_innings(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {TeamId}, {opponentsId}, {matchType}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{wicketsLimit}, {sortBy}, {sortDirection} ",
                teamId,
                opponentsId, matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, wicketsLimit, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<IndividualBowlingDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<IndividualBowlingDetails>, Error>> GetCompleteBowlingIndividualMatches(
        int teamId,
        int opponentsId, string matchType, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int wicketsLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            return await DbContext.PlayerIndividualBowlingDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_match(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {TeamId}, {opponentsId}, {matchType}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{wicketsLimit}, {sortBy}, {sortDirection} ",
                teamId,
                opponentsId, matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, wicketsLimit, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<IndividualBowlingDetails>, Error>(Errors.UnexpectedError);
        }
    }
}