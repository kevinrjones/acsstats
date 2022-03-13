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

//todo: add error handling for all repository code
// todo: have code throw an exception and see what the result is (and logging)
public class PlayerFieldingRecordDetailsRepository : BaseEfNoKeyRepository<PlayerFieldingCareerRecordDetails>,
    IPlayerFieldingRecordDetailsRepository
{
    private readonly ILogger<PlayerFieldingRecordDetailsRepository> _logger;

    public PlayerFieldingRecordDetailsRepository(AcsDbContext dbContext,
        ILogger<PlayerFieldingRecordDetailsRepository> logger) : base(dbContext)
    {
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetCompleteFieldingCareerRecords(
        string matchType, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int dismissalsLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    dismissalsLimit,
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsForTeam(
        int teamId,
        string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_against_specified_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsForTeamAgainstTeam(
            int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_for_team_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsBySeries(
        string matchType, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int dismissalsLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_series_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    dismissalsLimit,
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsBySeriesForTeam(int teamId,
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_series_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsBySeriesAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_series_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsBySeriesForTeamAgainstTeam(int teamId, int opponentsId, string matchType,
            int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_series_for_team_vs_opposition(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByGround(
        string matchType, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int dismissalsLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_ground_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    dismissalsLimit,
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsByGroundForTeam(int teamId,
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_ground_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsByGroundAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_ground_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsByGroundForTeamAgainstTeam(int teamId, int opponentsId, string matchType,
            int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_ground_for_team_vs_opposition(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByHost(
        string matchType, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int dismissalsLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_host_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    dismissalsLimit,
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsByHostForTeam(
            int teamId,
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_host_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsByHostAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_host_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsByHostForTeamAgainstTeam(int teamId, int opponentsId, string matchType,
            int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_host_for_team_vs_opposition(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsByOpposition(
            string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int dismissalsLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_opp_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    dismissalsLimit,
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsByOppositionForTeam(
            int teamId,
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_opp_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsByOppositionAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_opp_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsByOppositionForTeamAgainstTeam(int teamId, int opponentsId, string matchType,
            int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_opp_for_team_vs_opposition(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByYear(
        string matchType, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int dismissalsLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_year_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    dismissalsLimit,
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsByYearForTeam(
            int teamId,
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_year_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsByYearAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_year_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsByYearForTeamAgainstTeam(int teamId, int opponentsId, string matchType,
            int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_year_for_team_vs_opposition(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsBySeason(
        string matchType, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int dismissalsLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_season_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, groundId, hostCountryId, venueId,
                    startDate, endDate, season, matchResult,
                    dismissalsLimit,
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsBySeasonForTeam(int teamId,
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_season_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsBySeasonAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_season_vs_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>>
        GetFieldingCareerRecordsBySeasonForTeamAgainstTeam(int teamId, int opponentsId, string matchType,
            int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PlayerFieldingRecordDetails
                .FromSqlRaw(
                    "CALL fielding_career_records_by_season_for_team_vs_opposition(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }
}