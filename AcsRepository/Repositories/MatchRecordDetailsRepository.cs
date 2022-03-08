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

public class MatchRecordDetailsRepository : BaseEfNoKeyRepository<MatchRecordDetails>,
    IMatchRecordDetailsRepository
{
    private readonly ILogger<MatchRecordDetailsRepository> _logger;

    public MatchRecordDetailsRepository(AcsDbContext dbContext, ILogger<MatchRecordDetailsRepository> logger) :
        base(dbContext)
    {
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetInningsByInnings(string matchType,
        int minimum,
        int groundId,
        int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.MatchRecordDetails
                .FromSqlRaw(
                    "CALL team_records_highest_innings_overall(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)"
                    , matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<MatchRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetInningsByInningsForTeam(string matchType,
        int teamId,
        int minimum, int groundId,
        int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.MatchRecordDetails
                .FromSqlRaw(
                    "CALL team_records_highest_innings_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , matchType, teamId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}," +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<MatchRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetInningsByInningsAgainstOpponents(
        string matchType,
        int opponentsId,
        int minimum, int groundId,
        int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.MatchRecordDetails
                .FromSqlRaw(
                    "CALL team_records_highest_innings_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)",
                    matchType, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {OpponentsId}," +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<MatchRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetInningsByInningsForTeamAgainstOpponents(
        string matchType,
        int teamId,
        int opponentsId, int minimum, int groundId,
        int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.MatchRecordDetails
                .FromSqlRaw(
                    "CALL team_records_highest_innings_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)",
                    matchType, teamId, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();
            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId},{OpponentsId}," +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<MatchRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetMatchTotalsHigherThan(string matchType,
        int minimum,
        int groundId,
        int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.MatchRecordDetails
                .FromSqlRaw(
                    "CALL team_records_match_totals_overall(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)"
                    , matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}," +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<MatchRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetMatchTotalsHigherThanForTeam(
        string matchType,
        int teamId,
        int minimum, int groundId,
        int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.MatchRecordDetails
                .FromSqlRaw(
                    "CALL team_records_match_totals_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , matchType, teamId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}," +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<MatchRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetMatchTotalsHigherThanAgainstOpponents(
        string matchType,
        int opponentsId,
        int minimum, int groundId,
        int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.MatchRecordDetails
                .FromSqlRaw(
                    "CALL team_records_match_totals_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)",
                    matchType, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {OpponentsId}," +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<MatchRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<MatchRecordDetails>, Error>> GetMatchTotalsHigherThanForTeamAgainstOpponents(
        string matchType,
        int teamId,
        int opponentsId, int minimum, int groundId,
        int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.MatchRecordDetails
                .FromSqlRaw(
                    "CALL team_records_match_totals_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)",
                    matchType, teamId, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();
            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<MatchRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<MatchResult>, Error>> GetMatchResults(string matchType,
        int minimum,
        int groundId,
        int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.MatchResult
                .FromSqlRaw(
                    "CALL team_records_results_overall(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)"
                    , matchType, groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection)
                .ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}" +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<MatchResult>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<MatchResult>, Error>> GetMatchResultsForTeam(string matchType, int teamId,
        int minimum,
        int groundId,
        int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.MatchResult
                .FromSqlRaw(
                    "CALL team_records_results_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , matchType, teamId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<MatchResult>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<MatchResult>, Error>> GetMatchResultsAgainstOpponents(string matchType,
        int opponentsId, int minimum, int groundId,
        int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.MatchResult
                .FromSqlRaw(
                    "CALL team_records_results_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)",
                    matchType, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to proce ss this request: {matchType}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<MatchResult>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<MatchResult>, Error>> GetMatchResultsForTeamAgainstOpponents(
        string matchType,
        int teamId,
        int opponentsId, int minimum, int groundId,
        int hostCountryId, int venueId,
        long startDate, long endDate, string season,
        int matchResult,
        int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.MatchResult
                .FromSqlRaw(
                    "CALL team_records_results_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)",
                    matchType, teamId, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();
            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<MatchResult>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOverall(string matchType, int minimum,
        int groundId,
        int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_overall(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();
            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOverallForTeam(string matchType, int teamId,
        int minimum,
        int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , matchType, teamId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOverallAgainstOpponents(string matchType,
        int opponentsId,
        int minimum, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)",
                    matchType, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOverallForTeamAgainstOpponents(
        string matchType,
        int teamId,
        int opponentsId, int minimum, int groundId,
        int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)",
                    matchType, teamId, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();
            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{minimum}, {sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, minimum, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }


    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetSeriesOverall(string matchType, int groundId,
        int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_series_overall(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)",
                    matchType
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();
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
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetSeriesOverallForTeam(string matchType,
        int teamId,
        int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_series_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)"
                    , matchType, teamId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId},  " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetSeriesOverallAgainstOpponents(
        string matchType,
        int opponentsId,
        int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_series_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetSeriesOverallForTeamAgainstOpponents(
        string matchType,
        int teamId,
        int opponentsId, int groundId,
        int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_series_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)",
                    matchType, teamId, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();
            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetGroundOverall(string matchType, int groundId,
        int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_ground_overall(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)",
                    matchType
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();
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
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetGroundOverallForTeam(string matchType,
        int teamId,
        int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_ground_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)"
                    , matchType, teamId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetGroundOverallAgainstOpponents(
        string matchType,
        int opponentsId,
        int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_ground_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetGroundOverallForTeamAgainstOpponents(
        string matchType,
        int teamId,
        int opponentsId, int groundId,
        int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_ground_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)",
                    matchType, teamId, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();
            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetHostCountryOverall(string matchType,
        int groundId,
        int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_host_overall(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)",
                    matchType
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();
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
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetHostCountryOverallForTeam(string matchType,
        int teamId,
        int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_host_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)"
                    , matchType, teamId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetHostCountryOverallAgainstOpponents(
        string matchType,
        int opponentsId, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_host_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetHostCountryOverallForTeamAgainstOpponents(
        string matchType,
        int teamId, int opponentsId, int groundId,
        int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_host_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)",
                    matchType, teamId, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();
            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOppositionOverall(string matchType,
        int groundId,
        int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_opp_overall(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)",
                    matchType
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();
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
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOppositionOverallForTeam(string matchType,
        int teamId,
        int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_opp_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)"
                    , matchType, teamId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}" +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOppositionOverallAgainstOpponents(
        string matchType,
        int opponentsId, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_opp_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetOppositionOverallForTeamAgainstOpponents(
        string matchType,
        int teamId, int opponentsId, int groundId,
        int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_opp_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)",
                    matchType, teamId, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();
            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetByYearOverall(string matchType, int groundId,
        int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_startyear_overall(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)",
                    matchType
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();
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
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetByYearOverallForTeam(string matchType,
        int teamId,
        int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_startyear_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)"
                    , matchType, teamId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetByYearOverallAgainstOpponents(
        string matchType,
        int opponentsId, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_startyear_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetByYearOverallForTeamAgainstOpponents(
        string matchType,
        int teamId, int opponentsId, int groundId,
        int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_startyear_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)",
                    matchType, teamId, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();
            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetBySeasonOverall(string matchType,
        int groundId,
        int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_season_overall(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)",
                    matchType
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();
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
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetBySeasonOverallForTeam(string matchType,
        int teamId,
        int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_season_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)"
                    , matchType, teamId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetBySeasonOverallAgainstOpponents(
        string matchType,
        int opponentsId, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_season_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamRecordDetails>, Error>> GetBySeasonOverallForTeamAgainstOpponents(
        string matchType,
        int teamId, int opponentsId, int groundId,
        int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamRecordDetails
                .FromSqlRaw(
                    "CALL team_records_by_season_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)",
                    matchType, teamId, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult
                    , sortBy, sortDirection
                ).ToListAsync();
            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamRecordDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamExtrasDetails>, Error>> GetOverallExtrasOverall(string matchType,
        int minimum,
        int groundId,
        int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamExtrasDetails
                .FromSqlRaw(
                    "CALL team_records_total_extras_overall(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();
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
            return Result.Failure<IReadOnlyList<TeamExtrasDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamExtrasDetails>, Error>> GetOverallExtrasOverallForTeam(string matchType,
        int teamId,
        int minimum, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamExtrasDetails
                .FromSqlRaw(
                    "CALL team_records_total_extras_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , matchType, teamId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamExtrasDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamExtrasDetails>, Error>> GetOverallExtrasOverallAgainstOpponents(
        string matchType,
        int opponentsId, int minimum, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamExtrasDetails
                .FromSqlRaw(
                    "CALL team_records_total_extras_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)",
                    matchType, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamExtrasDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<TeamExtrasDetails>, Error>> GetOverallExtrasOverallForTeamAgainstOpponents(
        string matchType,
        int teamId, int opponentsId, int minimum, int groundId,
        int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult, int sortBy,
        string sortDirection)
    {
        try
        {
            var res = await DbContext.TeamExtrasDetails
                .FromSqlRaw(
                    "CALL team_records_total_extras_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)",
                    matchType, teamId, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();
            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<TeamExtrasDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<InningsExtrasDetails>, Error>> GetInningsExtrasOverall(string matchType,
        int minimum,
        int groundId,
        int hostCountryId, int venueId, long startDate,
        long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.InningsExtrasDetails
                .FromSqlRaw(
                    "CALL team_records_innings_extras_overall(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)",
                    matchType
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();
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
            return Result.Failure<IReadOnlyList<InningsExtrasDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<InningsExtrasDetails>, Error>> GetInningsExtrasOverallForTeam(
        string matchType,
        int teamId,
        int minimum, int groundId, int hostCountryId, int venueId,
        long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.InningsExtrasDetails
                .FromSqlRaw(
                    "CALL team_records_innings_extras_for_team(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)"
                    , matchType, teamId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<InningsExtrasDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<InningsExtrasDetails>, Error>> GetInningsExtrasOverallAgainstOpponents(
        string matchType,
        int opponentsId, int minimum, int groundId, int hostCountryId,
        int venueId, long startDate, long endDate, string season, int matchResult, int sortBy, string sortDirection)
    {
        try
        {
            var res = await DbContext.InningsExtrasDetails
                .FromSqlRaw(
                    "CALL team_records_innings_extras_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)",
                    matchType, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();

            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<InningsExtrasDetails>, Error>(Errors.UnexpectedError);
        }
    }

    public async Task<Result<IReadOnlyList<InningsExtrasDetails>, Error>>
        GetInningsExtrasOverallForTeamAgainstOpponents(
            string matchType,
            int teamId, int opponentsId, int minimum, int groundId,
            int hostCountryId, int venueId, long startDate, long endDate, string season, int matchResult, int sortBy,
            string sortDirection)
    {
        try
        {
            var res = await DbContext.InningsExtrasDetails
                .FromSqlRaw(
                    "CALL team_records_innings_extras_for_team_against_opponents(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12)",
                    matchType, teamId, opponentsId
                    , groundId, hostCountryId
                    , venueId
                    , startDate, endDate, season, matchResult, minimum
                    , sortBy, sortDirection
                ).ToListAsync();
            return res;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Unable to process this request: {matchType}, {TeamId}, {OpponentsId}, " +
                                   "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                   "{sortBy}, {sortDirection} ",
                matchType, teamId, opponentsId, groundId, hostCountryId,
                venueId, startDate, endDate, season, matchResult, sortBy, sortDirection
            );
            return Result.Failure<IReadOnlyList<InningsExtrasDetails>, Error>(Errors.UnexpectedError);
        }
    }
}