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

public class PlayerBattingRecordDetailsRepository : BaseEfNoKeyRepository<PlayerBattingCareerRecordDetails>,
    IPlayerBattingRecordDetailsRepository
{
    private readonly ILogger<PlayerBattingRecordDetailsRepository> _logger;

    public PlayerBattingRecordDetailsRepository(AcsDbContext dbContext,
        ILogger<PlayerBattingRecordDetailsRepository> logger) : base(dbContext)
    {
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>>
        GetCompleteBattingIndividualSeries(
            int teamId,
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_series(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, {opponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }


    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingCareerRecordsForTeam(
        int teamId,
        string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_career_records_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , teamId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>>
        GetBattingCareerRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_career_records_against_specified_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , opponentsId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>>
        GetBattingCareerRecordsForTeamAgainstTeam(int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_career_records_for_team_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId, venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }


    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetCompleteBattingCareerRecords(
        string matchType, int groundId, int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int runLimit, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_career_records_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    runLimit,
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingGroundsRecordsForTeam(
        int teamId,
        string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_ground_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , teamId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>>
        GetBattingGroundsRecordsForTeamAgainstTeam(int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_ground_for_team_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId, venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>>
        GetBattingGroundsRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_ground_against_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , opponentsId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetCompleteBattingGroundsRecords(
        string matchType, int groundId, int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int runLimit, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_ground_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    runLimit,
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingHostRecordsForTeam(
        int teamId,
        string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_host_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , teamId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>>
        GetBattingHostRecordsForTeamAgainstTeam(int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_host_for_team_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId, venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingHostRecordsAgainstTeam(
        int opponentsId, string matchType, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_host_against_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , opponentsId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetCompleteBattingHostRecords(
        string matchType, int groundId, int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int runLimit, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_host_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    runLimit,
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingOpponentsRecordsForTeam(
        int teamId,
        string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_opp_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , teamId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>>
        GetBattingOpponentsRecordsForTeamAgainstTeam(int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_opp_for_team_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId, venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>>
        GetBattingOpponentsRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_opp_against_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , opponentsId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>>
        GetCompleteBattingOpponentsRecords(
            string matchType, int groundId, int hostCountryId, int venueId, long startDate,
            long endDate, string season, int matchResult, int runLimit, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_opp_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    runLimit,
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingSeasonRecordsForTeam(
        int teamId,
        string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_season_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , teamId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>>
        GetBattingSeasonRecordsForTeamAgainstTeam(int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_season_for_team_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId, venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>>
        GetBattingSeasonRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_season_against_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , opponentsId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetCompleteBattingSeasonRecords(
        string matchType, int groundId, int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int runLimit, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_season_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    runLimit,
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingYearRecordsForTeam(
        int teamId,
        string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_year_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , teamId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>>
        GetBattingYearRecordsForTeamAgainstTeam(int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_year_for_team_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
                    , teamId, opponentsId
                    , matchType, groundId, hostCountryId, venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingYearRecordsAgainstTeam(
        int opponentsId, string matchType, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_year_against_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , opponentsId, matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , runLimit
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetCompleteBattingYearRecords(
        string matchType, int groundId, int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int runLimit, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerBattingRecordDetails
                .FromSqlRaw(
                    "CALL batting_individual_career_records_by_year_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    runLimit,
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
            return Result.Failure<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }
}