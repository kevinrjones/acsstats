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

public class PartnershipRecordDetailsRepository : BaseEfNoKeyRepository<PartnershipCareerRecordDetails>,
    IPartnershipRecordDetailsRepository
{
    private readonly ILogger<PartnershipRecordDetailsRepository> _logger;

    public PartnershipRecordDetailsRepository(AcsDbContext dbContext,
        ILogger<PartnershipRecordDetailsRepository> logger) : base(dbContext)
    {
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<PartnershipIndividualRecordDetails>, Error>>
        GetCompletePartnershipIndividualMatches(int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await
                DbContext.IndividualPartnershipDetails
                    .FromSqlRaw(
                        "CALL fow_partnership_list_by_match(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PartnershipIndividualRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipIndividualRecordDetails>, Error>>
        GetCompletePartnershipIndividualInnings(int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await
                DbContext.IndividualPartnershipDetails
                    .FromSqlRaw(
                        "CALL fow_partnership_list_by_innings(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PartnershipIndividualRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetPartnershipCareerRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetCompletePartnershipIndividualSeries(
            int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult, int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_series(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetPartnershipCareerRecordsForTeamAgainstTeam(int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_career_records_for_team_against_opponent(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetCompletePartnershipCareerRecords(
        string matchType, int groundId, int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int runLimit, int sortBy, string sortDirection)
    {
        try
        {
            var res = await
                DbContext.PartnershipDetails
                    .FromSqlRaw(
                        "CALL fow_career_records_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)"
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
            _logger.LogCritical(e, "Unable to process this request: {matchType}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipCareerRecordsForTeam(
        int teamId, string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_career_records_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipGroundsRecordsForTeam(
        int teamId, string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_ground_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetPartnershipGroundsRecordsForTeamAgainstTeam(int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_ground_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetPartnershipGroundsRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_ground_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetCompletePartnershipGroundsRecords(
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_ground_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipHostRecordsForTeam(
        int teamId, string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_host_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetPartnershipHostRecordsForTeamAgainstTeam(int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_host_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetPartnershipHostRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_host_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetCompletePartnershipHostRecords(
        string matchType, int groundId, int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int runLimit, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_host_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetPartnershipOpponentsRecordsForTeam(
            int teamId, string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_opposition_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetPartnershipOpponentsRecordsForTeamAgainstTeam(int teamId, int opponentsId, string matchType,
            int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_opposition_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetPartnershipOpponentsRecordsAgainstTeam(int opponentsId, string matchType, int groundId,
            int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_opposition_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetCompletePartnershipOpponentsRecords(
            string matchType, int groundId, int hostCountryId, int venueId,
            long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_opposition_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipSeasonRecordsForTeam(
        int teamId, string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_season_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetPartnershipSeasonRecordsForTeamAgainstTeam(int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_season_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetPartnershipSeasonRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_season_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetCompletePartnershipSeasonRecords(
        string matchType, int groundId, int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int runLimit, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_season_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetPartnershipYearRecordsForTeam(
        int teamId, string matchType, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_year_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetPartnershipYearRecordsForTeamAgainstTeam(int teamId, int opponentsId, string matchType, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult,
            int runLimit,
            int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_year_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>>
        GetPartnershipYearRecordsAgainstTeam(
            int opponentsId, string matchType, int groundId, int hostCountryId,
            int venueId, long startDate, long endDate, string season, int matchResult, int runLimit, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_year_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error>> GetCompletePartnershipYearRecords(
        string matchType, int groundId, int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int runLimit, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.PartnershipDetails
                .FromSqlRaw(
                    "CALL fow_partnership_list_by_year_complete(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
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
            return Result.Failure<IReadOnlyList<PartnershipCareerRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }
}