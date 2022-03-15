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
        Task<Result<IReadOnlyList<PlayerBattingRecordDto>, Error>> GetBattingCareerRecords(
            BattingBowlingFieldingModel sharedServiceFieldingModel);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetailsDto>, Error>> GetBowlingCareerRecords(
            BattingBowlingFieldingModel sharedServiceFieldingModel);

        Task<Result<IReadOnlyList<IndividualBattingDetailsDto>, Error>> GetBattingIndividualInnings(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<IndividualBowlingDetailsDto>, Error>> GetBowlingIndividualInnings(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<IndividualBattingDetailsDto>, Error>> GetBattingIndividualMatches(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<IndividualBowlingDetailsDto>, Error>> GetBowlingIndividualMatches(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBattingRecordDto>, Error>> GetBattingIndividualSeries(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetailsDto>, Error>> GetBowlingIndividualSeries(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBattingRecordDto>, Error>> GetBattingIndividualGrounds(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetailsDto>, Error>> GetBowlingIndividualGrounds(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBattingRecordDto>, Error>> GetBattingIndividualHost(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetailsDto>, Error>> GetBowlingIndividualHost(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBattingRecordDto>, Error>> GetBattingIndividualOpponents(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetailsDto>, Error>> GetBowlingIndividualOpponents(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBattingRecordDto>, Error>> GetBattingIndividualSeason(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetailsDto>, Error>> GetBowlingIndividualSeason(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBattingRecordDto>, Error>> GetBattingIndividualYear(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetailsDto>, Error>> GetBowlingIndividualYear(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecords(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsBySeries(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByGround(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByHost(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByOpposition(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsByYear(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error>> GetFieldingCareerRecordsBySeason(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<IndividualFieldingDetails>, Error>> GetFieldingIndividualInnings(
            BattingBowlingFieldingModel model);

        Task<Result<IReadOnlyList<IndividualFieldingDetails>, Error>> GetFieldingIndividualMatches(
            BattingBowlingFieldingModel model);
    }
}