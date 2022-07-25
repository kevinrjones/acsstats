using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services
{
    public interface IPlayersService
    {
        Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> GetBattingCareerRecords(
            BattingBowlingFieldingModel sharedServiceFieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<BowlingCareerRecordDto>>, Error>> GetBowlingCareerRecords(
            BattingBowlingFieldingModel sharedServiceFieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<IndividualBattingDetailsDto>>, Error>> GetBattingIndividualInnings(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<IndividualBowlingDetailsDto>>, Error>> GetBowlingIndividualInnings(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<IndividualBattingDetailsDto>>, Error>> GetBattingIndividualMatches(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<IndividualBowlingDetailsDto>>, Error>> GetBowlingIndividualMatches(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> GetBattingIndividualSeries(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<BowlingCareerRecordDto>>, Error>> GetBowlingIndividualSeries(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> GetBattingIndividualGrounds(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<BowlingCareerRecordDto>>, Error>> GetBowlingIndividualGrounds(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> GetBattingIndividualHost(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<BowlingCareerRecordDto>>, Error>> GetBowlingIndividualHost(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> GetBattingIndividualOpponents(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<BowlingCareerRecordDto>>, Error>> GetBowlingIndividualOpponents(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> GetBattingIndividualSeason(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<BowlingCareerRecordDto>>, Error>> GetBowlingIndividualSeason(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error>> GetBattingIndividualYear(
            BattingBowlingFieldingModel fieldingModel);

        Task<Result<SqlResultEnvelope<IReadOnlyList<BowlingCareerRecordDto>>, Error>> GetBowlingIndividualYear(
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
        
        Task<Result<IReadOnlyList<PlayerListDto>, Error>> GetPlayersFromSearch(PlayerSearchModel playerSearchModel);

    }
}