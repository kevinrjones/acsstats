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

public class FieldingIndividualCareerRecordsQuery
  : IRequest<Result<IReadOnlyList<IndividualFieldingDetailsDto>, Error>>
{
  private BattingBowlingFieldingModel FieldingModel { get; }
  private string Sql { get; }

  public FieldingIndividualCareerRecordsQuery(BattingBowlingFieldingModel fieldingModel, string sql)
  {
    FieldingModel = fieldingModel;
    Sql = sql;
  }

  internal class FieldingIndividualCareerRecordsQueryHandler
    : IRequestHandler<FieldingIndividualCareerRecordsQuery,
      Result<IReadOnlyList<IndividualFieldingDetailsDto>, Error>>
  {
    private readonly QueriesConnectionString _queriesConnectionString;
    private readonly ILogger<FieldingIndividualCareerRecordsQueryHandler> _logger;

    public FieldingIndividualCareerRecordsQueryHandler(
      QueriesConnectionString queriesConnectionString,
      ILogger<FieldingIndividualCareerRecordsQueryHandler> logger)
    {
      _queriesConnectionString = queriesConnectionString;
      _logger = logger;
    }

    public async Task<Result<IReadOnlyList<IndividualFieldingDetailsDto>, Error>> Handle(
      FieldingIndividualCareerRecordsQuery request, CancellationToken cancellationToken)
    {
      try
      {
        await using var connection = new MySqlConnection(_queriesConnectionString.Value);
        var result = (IReadOnlyList<IndividualFieldingDetails>)connection
          .Query<IndividualFieldingDetails>(request.Sql, new
          {
            team_id = request.FieldingModel.TeamId.Value,
            opponents_id = request.FieldingModel.OpponentsId.Value,
            match_type = request.FieldingModel.MatchType.Value,
            ground_id = request.FieldingModel.GroundId.Value,
            homecountry_id = request.FieldingModel.HostCountryId.Value,
            homeOrAway = request.FieldingModel.ToVenue(),
            startDate = request.FieldingModel.StartDateEpoch,
            endDate = request.FieldingModel.EndDateEpoch,
            season = request.FieldingModel.Season,
            matchResult = request.FieldingModel.MatchResult.Value,
            dismissals_limit = request.FieldingModel.Limit.Value,
            sort_by = (int)request.FieldingModel.SortOrder,
            sort_direction = request.FieldingModel.SortDirectionAsString(),
                    start_row = request.FieldingModel.StartRow,
                    page_size = request.FieldingModel.Rows
          }, commandType: CommandType.StoredProcedure).ToList();
        return Result.Success<IReadOnlyList<IndividualFieldingDetails>, Error>(result).ToDto();
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
          (int)request.FieldingModel.SortOrder, request.FieldingModel.SortDirectionAsString());
        return Result.Failure<IReadOnlyList<IndividualFieldingDetailsDto>, Error>(Errors.GetUnexpectedError(e.Message));
      }
    }
  }
}

