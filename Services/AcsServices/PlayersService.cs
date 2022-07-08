using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcsCommands.Query;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsRepository;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using MediatR;
using Services.Models;

namespace Services.AcsServices
{
  public class PlayersService : IPlayersService
  {
    private readonly IMediator _mediator;

    public PlayersService(IMediator mediator)
    {
      _mediator = mediator;
    }

    public async Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> GetBattingCareerRecords(
      BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsForTeamVsOpponentsQuery(model,
          "batting_career_records_for_team_vs_opponent"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(
          new BattingRecordsForTeamQuery(model, "batting_career_records_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsVsOpponentsQuery(model,
          "batting_career_records_against_specified_opponent"));
      }

      return await _mediator.Send(
        new BattingRecordsCompleteQuery(model, "batting_career_records_complete"));
    }

    public async Task<Result<SqlResultEnvelope<IReadOnlyList<IndividualBattingDetailsDto>>, Error>> GetBattingIndividualInnings(
      BattingBowlingFieldingModel model)
    {
      return await _mediator.Send(
        new BattingIndividualCareerRecordsQuery(model, "batting_individual_career_records_by_innings"));
    }

    public async Task<Result<SqlResultEnvelope<IReadOnlyList<IndividualBattingDetailsDto>>, Error>> GetBattingIndividualMatches(
      BattingBowlingFieldingModel model)
    {
      return await _mediator.Send(
        new BattingIndividualCareerRecordsQuery(model, "batting_individual_career_records_by_match"));
    }

    public async Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> GetBattingIndividualSeries(
      BattingBowlingFieldingModel model)
    {
      return await _mediator.Send(new BattingIndividualSeriesQuery(model));
    }

    public async Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> GetBattingIndividualGrounds(
      BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsForTeamVsOpponentsQuery(model,
          "batting_individual_career_records_by_ground_for_team_vs_opponent"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsForTeamQuery(model,
          "batting_individual_career_records_by_ground_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsVsOpponentsQuery(model,
          "batting_individual_career_records_by_ground_against_opponent"));
      }

      return await _mediator.Send(new BattingRecordsCompleteQuery(model,
        "batting_individual_career_records_by_ground_complete"));
    }

    public async Task<Result<IReadOnlyList<BowlingCareerRecordDto>, Error>> GetBowlingCareerRecords(
      BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsForTeamVsOpponentsQuery(model,
          "bowling_career_records_for_team_vs_opponent"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsForTeamQuery(model,
          "bowling_career_records_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsVsOpponentsQuery(model,
          "bowling_career_records_against_specified_opponent"));
      }

      return await _mediator.Send(new BowlingRecordsCompleteQuery(model,
        "bowling_career_records_complete"));
    }


    public async Task<Result<IReadOnlyList<IndividualBowlingDetailsDto>, Error>> GetBowlingIndividualInnings(
      BattingBowlingFieldingModel model)
    {
      return await _mediator.Send(
        new BowlingIndividualCareerRecordsQuery(model, "bowling_individual_career_records_by_innings"));
    }

    public async Task<Result<IReadOnlyList<IndividualBowlingDetailsDto>, Error>> GetBowlingIndividualMatches(
      BattingBowlingFieldingModel model)
    {
      return await _mediator.Send(
        new BowlingIndividualCareerRecordsQuery(model, "bowling_individual_career_records_by_match"));
    }

    public async Task<Result<IReadOnlyList<BowlingCareerRecordDto>, Error>> GetBowlingIndividualSeries(
      BattingBowlingFieldingModel model)
    {
      return await _mediator.Send(new BowlingIndividualSeriesQuery(model));
    }

    public async Task<Result<IReadOnlyList<BowlingCareerRecordDto>, Error>> GetBowlingIndividualGrounds(
      BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsForTeamVsOpponentsQuery(model,
          "bowling_individual_career_records_by_ground_for_team_vs_opponent"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsForTeamQuery(model,
          "bowling_individual_career_records_by_ground_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsVsOpponentsQuery(model,
          "bowling_individual_career_records_by_ground_against_opponent"));
      }

      return await _mediator.Send(new BowlingRecordsCompleteQuery(model,
        "bowling_individual_career_records_by_ground_complete"));
    }

    public async Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> GetBattingIndividualHost(
      BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsForTeamVsOpponentsQuery(model,
          "batting_individual_career_records_by_host_for_team_vs_opponent"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsForTeamQuery(model,
          "batting_individual_career_records_by_host_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsVsOpponentsQuery(model,
          "batting_individual_career_records_by_host_against_opponent"));
      }

      return await _mediator.Send(new BattingRecordsCompleteQuery(model,
        "batting_individual_career_records_by_host_complete"));
    }

    public async Task<Result<IReadOnlyList<BowlingCareerRecordDto>, Error>> GetBowlingIndividualHost(
      BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsForTeamVsOpponentsQuery(model,
          "bowling_individual_career_records_by_host_for_team_vs_opponent"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsForTeamQuery(model,
          "bowling_individual_career_records_by_host_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsVsOpponentsQuery(model,
          "bowling_individual_career_records_by_host_against_opponent"));
      }

      return await _mediator.Send(new BowlingRecordsCompleteQuery(model,
        "bowling_individual_career_records_by_host_complete"));
    }

    public async Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> GetBattingIndividualOpponents(
      BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsForTeamVsOpponentsQuery(model,
          "batting_individual_career_records_by_opp_for_team_vs_opponent"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsForTeamQuery(model,
          "batting_individual_career_records_by_opp_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsVsOpponentsQuery(model,
          "batting_individual_career_records_by_opp_against_opponent"));
      }

      return await _mediator.Send(new BattingRecordsCompleteQuery(model,
        "batting_individual_career_records_by_opp_complete"));
    }

    public async Task<Result<IReadOnlyList<BowlingCareerRecordDto>, Error>>
      GetBowlingIndividualOpponents(
        BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsForTeamVsOpponentsQuery(model,
          "bowling_individual_career_records_by_opp_against_opponent"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsForTeamQuery(model,
          "bowling_individual_career_records_by_opp_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsVsOpponentsQuery(model,
          "bowling_individual_career_records_by_opp_for_team_vs_opponent"));
      }

      return await _mediator.Send(new BowlingRecordsCompleteQuery(model,
        "bowling_individual_career_records_by_opp_complete"));
    }

    public async Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> GetBattingIndividualSeason(
      BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsForTeamVsOpponentsQuery(model,
          "batting_individual_career_records_by_season_for_team_vs_opponent"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsForTeamQuery(model,
          "batting_individual_career_records_by_season_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsVsOpponentsQuery(model,
          "batting_individual_career_records_by_season_against_opponent"));
      }

      return await _mediator.Send(new BattingRecordsCompleteQuery(model,
        "batting_individual_career_records_by_season_complete"));
    }

    public async Task<Result<IReadOnlyList<BowlingCareerRecordDto>, Error>> GetBowlingIndividualSeason(
      BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsForTeamVsOpponentsQuery(model,
          "bowling_individual_career_records_by_season_against_opponent"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsForTeamQuery(model,
          "bowling_individual_career_records_by_season_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsVsOpponentsQuery(model,
          "bowling_individual_career_records_by_season_for_team_vs_opponent"));
      }

      return await _mediator.Send(new BowlingRecordsCompleteQuery(model,
        "bowling_individual_career_records_by_season_complete"));
    }

    public async Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> GetBattingIndividualYear(
      BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsForTeamVsOpponentsQuery(model,
          "batting_individual_career_records_by_year_for_team_vs_opponent"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsForTeamQuery(model,
          "batting_individual_career_records_by_year_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BattingRecordsVsOpponentsQuery(model,
          "batting_individual_career_records_by_year_against_opponent"));
      }

      return await _mediator.Send(new BattingRecordsCompleteQuery(model,
        "batting_individual_career_records_by_year_complete"));
    }

    public async Task<Result<IReadOnlyList<BowlingCareerRecordDto>, Error>> GetBowlingIndividualYear(
      BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsForTeamVsOpponentsQuery(model,
          "bowling_individual_career_records_by_year_against_opponent"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsForTeamQuery(model,
          "bowling_individual_career_records_by_year_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new BowlingRecordsVsOpponentsQuery(model,
          "bowling_individual_career_records_by_year_for_team_vs_opponent"));
      }

      return await _mediator.Send(new BowlingRecordsCompleteQuery(model,
        "bowling_individual_career_records_by_year_complete"));
    }

    public async Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>> GetFieldingCareerRecords(
      BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsForTeamVsOpponentsQuery(model,
          "fielding_career_records_for_team_vs_opponent"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsForTeamQuery(model,
          "fielding_career_records_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsVsOpponentsQuery(model,
          "fielding_career_records_against_specified_opponent"));
      }

      return await _mediator.Send(new FieldingRecordsCompleteQuery(model,
        "fielding_career_records_complete"));
    }

    public async Task<Result<IReadOnlyList<IndividualFieldingDetailsDto>, Error>> GetFieldingIndividualInnings(
      BattingBowlingFieldingModel model)
    {
      return await _mediator.Send(
        new FieldingIndividualCareerRecordsQuery(model, "fielding_individual_career_records_by_innings"));
    }

    public async Task<Result<IReadOnlyList<IndividualFieldingDetailsDto>, Error>> GetFieldingIndividualMatches(
      BattingBowlingFieldingModel model)
    {
      return await _mediator.Send(
        new FieldingIndividualCareerRecordsQuery(model, "fielding_individual_career_records_by_match"));
    }

    public async Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>>
      GetFieldingCareerRecordsBySeries(
        BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsForTeamVsOpponentsQuery(model,
          "fielding_career_records_by_series_for_team_vs_opposition"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsForTeamQuery(model,
          "fielding_career_records_by_series_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsVsOpponentsQuery(model,
          "fielding_career_records_by_series_vs_opponent"));
      }

      return await _mediator.Send(new FieldingRecordsCompleteQuery(model,
        "fielding_career_records_by_series_complete"));
    }


    public async Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>>
      GetFieldingCareerRecordsByGround(BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsForTeamVsOpponentsQuery(model,
          "fielding_career_records_by_ground_for_team_vs_opposition"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsForTeamQuery(model,
          "fielding_career_records_by_ground_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsVsOpponentsQuery(model,
          "fielding_career_records_by_ground_vs_opponent"));
      }

      return await _mediator.Send(new FieldingRecordsCompleteQuery(model,
        "fielding_career_records_by_ground_complete"));
    }

    public async Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>>
      GetFieldingCareerRecordsByHost(
        BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsForTeamVsOpponentsQuery(model,
          "fielding_career_records_by_host_for_team_vs_opposition"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsForTeamQuery(model,
          "fielding_career_records_by_host_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsVsOpponentsQuery(model,
          "fielding_career_records_by_host_vs_opponent"));
      }

      return await _mediator.Send(new FieldingRecordsCompleteQuery(model,
        "fielding_career_records_by_host_complete"));
    }

    public async Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>>
      GetFieldingCareerRecordsByOpposition(
        BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsForTeamVsOpponentsQuery(model,
          "fielding_career_records_by_opp_for_team_vs_opposition"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsForTeamQuery(model,
          "fielding_career_records_by_opp_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsVsOpponentsQuery(model,
          "fielding_career_records_by_opp_vs_opponent"));
      }

      return await _mediator.Send(new FieldingRecordsCompleteQuery(model,
        "fielding_career_records_by_opp_complete"));
    }

    public async Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>>
      GetFieldingCareerRecordsByYear(
        BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsForTeamVsOpponentsQuery(model,
          "fielding_career_records_by_year_for_team_vs_opposition"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsForTeamQuery(model,
          "fielding_career_records_by_year_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsVsOpponentsQuery(model,
          "fielding_career_records_by_year_vs_opponent"));
      }

      return await _mediator.Send(new FieldingRecordsCompleteQuery(model,
        "fielding_career_records_by_year_complete"));
    }

    public async Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>>
      GetFieldingCareerRecordsBySeason(
        BattingBowlingFieldingModel model)
    {
      if (model.TeamId.Value != 0 && model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsForTeamVsOpponentsQuery(model,
          "fielding_career_records_by_season_for_team_vs_opposition"));
      }

      if (model.TeamId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsForTeamQuery(model,
          "fielding_career_records_by_season_for_team"));
      }

      if (model.OpponentsId.Value != 0)
      {
        return await _mediator.Send(new FieldingRecordsVsOpponentsQuery(model,
          "fielding_career_records_by_season_vs_opponent"));
      }

      return await _mediator.Send(new FieldingRecordsCompleteQuery(model,
        "fielding_career_records_by_season_complete"));
    }

    public async Task<Result<IReadOnlyList<PlayerListDto>, Error>> GetPlayersFromSearch(
      PlayerSearchModel playerSearchModel)
    {
      string format = "yyyy-MM-dd";

      string otherNamePart = "";
      string sortNamePart = "";

      if (playerSearchModel.Name.Contains(" "))
      {
        otherNamePart = playerSearchModel.Name.Split(" ").First();
        sortNamePart = playerSearchModel.Name.Substring(playerSearchModel.Name.IndexOf(" ") + 1);
      }
      else
      {
        sortNamePart = playerSearchModel.Name;
      }
      
      var debutDate = EpochDateType.Create(playerSearchModel.DebutDate, format);
      var activeUntilDate = EpochDateType.Create(playerSearchModel.ActiveUntil, format);
      if (debutDate.IsFailure || activeUntilDate.IsFailure)
        return Result.Failure<IReadOnlyList<PlayerListDto>, Error>(
          Errors.InvalidDateError("Either the start or end date is invalid"));

      return await _mediator.Send(new PlayerSearchQuery(otherNamePart, sortNamePart, playerSearchModel.Team,
        debutDate.Value,
        activeUntilDate.Value, playerSearchModel.ExactNameMatch));
    }
  }
}