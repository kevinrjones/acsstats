using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services
{
    public interface IPlayersService
    {
        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingCareerRecords(
            BattingBowlingFieldingModel sharedServiceFieldingModel);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingCareerRecords(
            BattingBowlingFieldingModel sharedServiceFieldingModel);

        Task<Result<IReadOnlyList<IndividualBattingDetails>, Error>> GetBattingIndividualInnings(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<IndividualBowlingDetails>, Error>> GetBowlingIndividualInnings(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<IndividualBattingDetails>, Error>> GetBattingIndividualMatches(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<IndividualBowlingDetails>, Error>> GetBowlingIndividualMatches(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingIndividualSeries(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingIndividualSeries(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingIndividualGrounds(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingIndividualGrounds(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingIndividualHost(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingIndividualHost(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingIndividualOpponents(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingIndividualOpponents(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingIndividualSeason(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingIndividualSeason(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error>> GetBattingIndividualYear(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error>> GetBowlingIndividualYear(
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