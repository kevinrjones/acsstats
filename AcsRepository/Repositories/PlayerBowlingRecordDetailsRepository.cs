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

public class PlayerBowlingRecordDetailsRepository : BaseEfNoKeyRepository<PlayerBowlingCareerRecordDetails>,
    IPlayerBowlingRecordDetailsRepository
{
    private readonly ILogger<PlayerBowlingRecordDetailsRepository> _logger;

    public PlayerBowlingRecordDetailsRepository(AcsDbContext dbContext,
        ILogger<PlayerBowlingRecordDetailsRepository> logger) : base(dbContext)
    {
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetCompleteBowlingCareerRecords(
        string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult, int wicketsLimit,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_career_records_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    wicketsLimit,
                    sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>>
        GetCompleteBowlingIndividualSeries(
            int teamId,
            int opponentsId, string matchType, int groundId, int hostCountryId, int venueId, long startDate,
            long endDate, string season, int matchResult, int wicketsLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            return await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_series(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, {opponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingCareerRecordsForTeam(
        int teamId,
        string matchType, int groundId, int hostCountryId,
        int venueId,
        long startDate, long endDate, string season,
        int matchResult, int wicketsLimit,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_career_records_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , teamId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>>
        GetBowlingCareerRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_career_records_against_specified_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , opponentsId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {opponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>>
        GetBowlingCareerRecordsForTeamAgainstTeam(
            int teamId, int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_career_records_for_team_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId, venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, {opponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingGroundsRecordsForTeam(
        int teamId,
        string matchType, int groundId, int hostCountryId,
        int venueId,
        long startDate, long endDate, string season,
        int matchResult, int wicketsLimit,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_ground_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , teamId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>>
        GetBowlingGroundsRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_ground_against_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , opponentsId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {opponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>>
        GetBowlingGroundsRecordsForTeamAgainstTeam(
            int teamId, int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_ground_for_team_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId, venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, {opponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }


    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetCompleteBowlingGroundsRecords(
        string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult, int wicketsLimit,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_ground_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    wicketsLimit,
                    sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingHostRecordsForTeam(
        int teamId,
        string matchType, int groundId, int hostCountryId,
        int venueId,
        long startDate, long endDate, string season,
        int matchResult, int wicketsLimit,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_host_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , teamId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingHostRecordsAgainstTeam(
        int opponentsId, string matchType, int groundId, int hostCountryId,
        int venueId,
        long startDate, long endDate, string season,
        int matchResult, int wicketsLimit,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_host_against_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , opponentsId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {opponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>>
        GetBowlingHostRecordsForTeamAgainstTeam(
            int teamId, int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_host_for_team_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId, venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, {opponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetCompleteBowlingHostRecords(
        string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult, int wicketsLimit,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_host_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    wicketsLimit,
                    sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingOpponentsRecordsForTeam(
        int teamId,
        string matchType, int groundId, int hostCountryId,
        int venueId,
        long startDate, long endDate, string season,
        int matchResult, int wicketsLimit,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_opp_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , teamId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>>
        GetBowlingOpponentsRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_opp_against_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , opponentsId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {opponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>>
        GetBowlingOpponentsRecordsForTeamAgainstTeam(
            int teamId, int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_opp_for_team_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId, venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, {opponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>>
        GetCompleteBowlingOpponentsRecords(
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_opp_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    wicketsLimit,
                    sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingSeasonRecordsForTeam(
        int teamId,
        string matchType, int groundId, int hostCountryId,
        int venueId,
        long startDate, long endDate, string season,
        int matchResult, int wicketsLimit,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_season_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , teamId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>>
        GetBowlingSeasonRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_season_against_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , opponentsId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {opponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>>
        GetBowlingSeasonRecordsForTeamAgainstTeam(
            int teamId, int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_season_for_team_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId, venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, {opponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }


    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetCompleteBowlingSeasonRecords(
        string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult, int wicketsLimit,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_season_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    wicketsLimit,
                    sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingYearRecordsForTeam(
        int teamId,
        string matchType, int groundId, int hostCountryId,
        int venueId,
        long startDate, long endDate, string season,
        int matchResult, int wicketsLimit,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_year_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , teamId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingYearRecordsAgainstTeam(
        int opponentsId, string matchType, int groundId, int hostCountryId,
        int venueId,
        long startDate, long endDate, string season,
        int matchResult, int wicketsLimit,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_year_against_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , opponentsId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {opponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>>
        GetBowlingYearRecordsForTeamAgainstTeam(
            int teamId, int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId,
            long startDate, long endDate, string season,
            int matchResult, int wicketsLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_year_for_team_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId, venueId
                    , startDate, endDate, season, matchResult
                    , wicketsLimit
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, {opponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }


    public async Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetCompleteBowlingYearRecords(
        string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult, int wicketsLimit,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBowlingRecordDetails
                .FromSqlRaw(
                    "CALL bowling_individual_career_records_by_year_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    wicketsLimit,
                    sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }
}