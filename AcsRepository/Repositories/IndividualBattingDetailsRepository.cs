using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcsRepository.Interfaces;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// ReSharper disable FormatStringProblem

namespace AcsRepository.Repositories;

public class IndividualBattingDetailsRepository : BaseEfNoKeyRepository<IndividualBattingDetails>,
    IIndividualBattingDetailsRepository
{
    private readonly ILogger<IndividualBattingDetailsRepository> _logger;

    public IndividualBattingDetailsRepository(AcsDbContext dbContext,
        ILogger<IndividualBattingDetailsRepository> logger)
        : base(dbContext)
    {
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<IndividualBattingDetails>, Error>> GetCompleteBattingIndividualMatches(
        int teamId,
        int opponentsId, string matchType, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await
                DbContext.PlayerIndividualBattingDetails
                    .FromSqlRaw(
                        "CALL batting_individual_career_records_by_match(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                        , teamId, opponentsId
                        , matchType, groundId, hostCountryId
                        , venueId
                        , startDate, endDate, season, matchResult
                        , runLimit
                        , sortBy, sortDirection)
                    .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {TeamId}, {opponentsId}, {matchType}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{runLimit}, {sortBy}, {sortDirection} ",
                teamId,
                opponentsId, matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, runLimit, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<IndividualBattingDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<IndividualBattingDetails>, Error>> GetCompleteBattingIndividualInnings(
        int teamId,
        int opponentsId, string matchType, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerIndividualBattingDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_innings(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {TeamId}, {opponentsId}, {matchType}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{runLimit}, {sortBy}, {sortDirection} ",
                teamId,
                opponentsId, matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, runLimit, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<IndividualBattingDetails>, Error>(Errors.UnexpectedError);
        }
    }
}