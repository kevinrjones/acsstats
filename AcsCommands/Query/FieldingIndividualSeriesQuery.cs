using System.Data;
using AcsCommands.Util;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsRepository.Util;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class FieldingIndividualSeriesQuery : IRequest<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>>
{
  private BattingBowlingFieldingModel FieldingModel { get; }

  public FieldingIndividualSeriesQuery(BattingBowlingFieldingModel fieldingModel)
  {
    FieldingModel = fieldingModel;
  }

  internal class FieldingIndividualSeriesQueryHandler
    : IRequestHandler<FieldingIndividualSeriesQuery, Result<IReadOnlyList<FieldingCareerRecordDto>, Error>>
  {
    private readonly QueriesConnectionString _queriesConnectionString;
    private readonly ILogger<FieldingIndividualSeriesQueryHandler> _logger;

    public FieldingIndividualSeriesQueryHandler(
      QueriesConnectionString queriesConnectionString,
      ILogger<FieldingIndividualSeriesQueryHandler> logger)
    {
      _queriesConnectionString = queriesConnectionString;
      _logger = logger;
    }

    public async Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>> Handle(FieldingIndividualSeriesQuery request, CancellationToken cancellationToken)
    {
            var sql = "fielding_individual_career_records_by_series";
            
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = (IReadOnlyList<PlayerFieldingCareerRecordDetails>) connection
                    .Query<PlayerFieldingCareerRecordDetails>(sql, new
                    {
                        team_id = request.FieldingModel.TeamId.Value,
                        opponents_id = request.FieldingModel.OpponentsId.Value,
                        match_type = request.FieldingModel.MatchType.Value,
                        match_subtype = request.FieldingModel.MatchSubType.Value,
                        ground_id = request.FieldingModel.GroundId.Value,
                        homecountry_id = request.FieldingModel.HostCountryId.Value,
                        homeOrAway = request.FieldingModel.ToVenue(),
                        startDate = request.FieldingModel.StartDateEpoch,
                        endDate = request.FieldingModel.EndDateEpoch,
                        season = request.FieldingModel.Season,
                        matchResult = request.FieldingModel.MatchResult.Value,
                        dismissals_limit = request.FieldingModel.Limit.Value,
                        sort_by = (int) request.FieldingModel.SortOrder,
                        sort_direction = request.FieldingModel.SortDirectionAsString(),
                    start_row = request.FieldingModel.StartRow,
                    page_size = request.FieldingModel.Rows
                    }, commandType: CommandType.StoredProcedure).ToList();
                return Result.Success<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>(result).ToDto();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {matchType}, {teamId}, {opponentsId}, " +
                                       "{groundId}, {hostCountryId},{venueId}, {startDate}, {endDate}, {season}, {matchResult}, " +
                                       "{sortBy}, {sortDirection} ",
                    request.FieldingModel.MatchType.Value, request.FieldingModel.TeamId.Value,
                    request.FieldingModel.OpponentsId.Value, request.FieldingModel.GroundId.Value,
                    request.FieldingModel.HostCountryId.Value, request.FieldingModel.ToVenue(),
                    request.FieldingModel.StartDateEpoch,
                    request.FieldingModel.EndDateEpoch, request.FieldingModel.Season,
                    request.FieldingModel.MatchResult.Value,
                    (int) request.FieldingModel.SortOrder, request.FieldingModel.SortDirectionAsString());
                return Result.Failure<IReadOnlyList<FieldingCareerRecordDto>, Error>(Errors.GetUnexpectedError(e.Message));
            }
    }
  }
}