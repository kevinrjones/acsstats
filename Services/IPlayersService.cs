using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services
{
    public interface IPlayersService
    {
        Task<Result<IReadOnlyList<BattingCareerRecordDto>, Error>> GetBattingCareerRecords(
            BattingBowlingFieldingModel sharedServiceFieldingModel);

        Task<Result<IReadOnlyList<BowlingCareerRecordDetailsDto>, Error>> GetBowlingCareerRecords(
            BattingBowlingFieldingModel sharedServiceFieldingModel);

        Task<Result<IReadOnlyList<IndividualBattingDetailsDto>, Error>> GetBattingIndividualInnings(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<IndividualBowlingDetailsDto>, Error>> GetBowlingIndividualInnings(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<IndividualBattingDetailsDto>, Error>> GetBattingIndividualMatches(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<IndividualBowlingDetailsDto>, Error>> GetBowlingIndividualMatches(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<BattingCareerRecordDto>, Error>> GetBattingIndividualSeries(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<BowlingCareerRecordDetailsDto>, Error>> GetBowlingIndividualSeries(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<BattingCareerRecordDto>, Error>> GetBattingIndividualGrounds(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<BowlingCareerRecordDetailsDto>, Error>> GetBowlingIndividualGrounds(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<BattingCareerRecordDto>, Error>> GetBattingIndividualHost(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<BowlingCareerRecordDetailsDto>, Error>> GetBowlingIndividualHost(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<BattingCareerRecordDto>, Error>> GetBattingIndividualOpponents(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<BowlingCareerRecordDetailsDto>, Error>> GetBowlingIndividualOpponents(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<BattingCareerRecordDto>, Error>> GetBattingIndividualSeason(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<BowlingCareerRecordDetailsDto>, Error>> GetBowlingIndividualSeason(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<BattingCareerRecordDto>, Error>> GetBattingIndividualYear(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<BowlingCareerRecordDetailsDto>, Error>> GetBowlingIndividualYear(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>> GetFieldingCareerRecords(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>> GetFieldingCareerRecordsBySeries(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>> GetFieldingCareerRecordsByGround(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>> GetFieldingCareerRecordsByHost(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>> GetFieldingCareerRecordsByOpposition(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>> GetFieldingCareerRecordsByYear(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<FieldingCareerRecordDto>, Error>> GetFieldingCareerRecordsBySeason(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<IndividualFieldingDetailsDto>, Error>> GetFieldingIndividualInnings(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<IndividualFieldingDetailsDto>, Error>> GetFieldingIndividualMatches(
            BattingBowlingFieldingModel model);
    }
}