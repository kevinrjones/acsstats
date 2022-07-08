using AcsDto.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;

namespace AcsCommands.Util;

public static class DtoExtensions
{
    public static Result<IReadOnlyList<BattingCareerRecordDto>, Error> ToDto(
        this Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error> details)
    {
        return details.Map(r =>
            (IReadOnlyList<BattingCareerRecordDto>) r.Map(item =>
                new BattingCareerRecordDto(item.Name, item.SortNamePart, item.Team, item.Opponents, item.Year,
                    item.Matches,
                    item.Innings, item.Ground, item.CountryName, item.Runs, item.NotOuts,
                    item.HighestScore, item.NotOut, item.Avg, item.Hundreds ?? 0, item.Fifties ?? 0,
                    item.Ducks ?? 0, item.Fours ?? 0, item.Sixes ?? 0, item.Balls ?? 0)).ToList());
    }

    public static Result<SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>, Error> ToEnvelope(
        this Result<IReadOnlyList<PlayerBattingCareerRecordDetails>, Error> details, int count)
    {
        return details.Map(r =>
                (IReadOnlyList<BattingCareerRecordDto>) r.Map(item =>
                    new BattingCareerRecordDto(item.Name, item.SortNamePart, item.Team, item.Opponents, item.Year,
                        item.Matches,
                        item.Innings, item.Ground, item.CountryName, item.Runs, item.NotOuts,
                        item.HighestScore, item.NotOut, item.Avg, item.Hundreds ?? 0, item.Fifties ?? 0,
                        item.Ducks ?? 0, item.Fours ?? 0, item.Sixes ?? 0, item.Balls ?? 0)).ToList())
            .Map(r => new SqlResultEnvelope<IReadOnlyList<BattingCareerRecordDto>>(count, r));
    }

    public static Result<SqlResultEnvelope<IReadOnlyList<IndividualBattingDetailsDto>>, Error> ToEnvelope(
        this Result<IReadOnlyList<IndividualBattingDetails>, Error> details, int count)
    {
        return details.Map(r =>
                (IReadOnlyList<IndividualBattingDetailsDto>) r.Map(item =>
                    new IndividualBattingDetailsDto(item.FullName, item.SortNamePart, item.Team, item.Opponents,
                        item.InningsNumber,
                        item.Ground,
                        item.MatchDate, item.PlayerScore, item.Bat1, item.Bat2, item.NotOut,
                        item.Position, item.Balls, item.Fours, item.Sixes ?? 0, item.Minutes ?? 0)).ToList())
            .Map(r =>
                new SqlResultEnvelope<IReadOnlyList<IndividualBattingDetailsDto>>(count, r));
    }

    public static Result<IReadOnlyList<BowlingCareerRecordDto>, Error> ToDto(
        this Result<IReadOnlyList<PlayerBowlingCareerRecordDetails>, Error> details)
    {
        return details.Map(r =>
            (IReadOnlyList<BowlingCareerRecordDto>) r.Map(item =>
                new BowlingCareerRecordDto(item.Name, item.SortNamePart, item.Team, item.Opponents, item.Year,
                    item.Matches, item.Innings, item.Ground, item.CountryName, item.Balls, item.Maidens,
                    item.Runs, item.Wickets, item.Avg ?? 0.0f, item.Fours ?? 0, item.Sixes ?? 0,
                    FiveFor: item.FiveFor ?? 0, item.TenFor ?? 0, item.bbiw ?? 0, item.bbir ?? 0, item.bbmw ?? 0,
                    item.bbmr ?? 0)).ToList());
    }

    public static Result<IReadOnlyList<IndividualBowlingDetailsDto>, Error> ToDto(
        this Result<IReadOnlyList<IndividualBowlingDetails>, Error> details)
    {
        return details.Map(r =>
            (IReadOnlyList<IndividualBowlingDetailsDto>) r.Map(item =>
                new IndividualBowlingDetailsDto(item.FullName, item.SortNamePart, item.Team, item.Opponents,
                    item.InningsNumber,
                    item.Ground, item.MatchDate, item.PlayerBalls, item.PlayerMaidens, item.PlayerRuns,
                    item.PlayerWickets, item.BallsPerOver, item.Econ)).ToList());
    }

    public static Result<IReadOnlyList<FieldingCareerRecordDto>, Error> ToDto(
        this Result<IReadOnlyList<PlayerFieldingCareerRecordDetails>, Error> details)
    {
        return details.Map(r =>
            (IReadOnlyList<FieldingCareerRecordDto>) r.Map(item =>
                new FieldingCareerRecordDto(item.Name, item.Team, item.Opponents, item.Year, item.Matches,
                    item.Innings ?? 0, item.Ground, item.CountryName, item.Dismissals, item.Caught, item.Stumpings,
                    item.CaughtKeeper, item.CaughtFielder, item.BestDismissals, item.BestCaughtFielder,
                    item.BestCaughtKeeper,
                    item.BestStumpings)).ToList());
    }

    public static Result<IReadOnlyList<IndividualFieldingDetailsDto>, Error> ToDto(
        this Result<IReadOnlyList<IndividualFieldingDetails>, Error> details)
    {
        return details.Map(r =>
            (IReadOnlyList<IndividualFieldingDetailsDto>) r.Map(item =>
                new IndividualFieldingDetailsDto(item.FullName, item.Team, item.Opponents, item.InningsNumber,
                    item.Ground, item.MatchDate, item.Dismissals, item.Caught, item.Stumpings,
                    item.CaughtKeeper, item.CaughtFielder)).ToList());
    }

    public static Result<IReadOnlyList<PartnershipCareerRecordDetailsDto>, Error> ToDto(
        this Result<IReadOnlyList<PartnershipCareerRecordDetails>, Error> details)
    {
        return details.Map(r =>
            (IReadOnlyList<PartnershipCareerRecordDetailsDto>) r.Map(item =>
                new PartnershipCareerRecordDetailsDto(item.PlayerIds, item.Player1, item.Player2, item.Team,
                    item.Opponents,
                    item.Innings, item.NotOuts, item.Runs, item.Avg, item.Hundreds, item.Fifties, item.HighestScore,
                    item.Unbroken, item.Ground, item.CountryName, item.SeriesDate)).ToList());
    }

    public static Result<IReadOnlyList<PartnershipIndividualRecordDetailsDto>, Error> ToDto(
        this Result<IReadOnlyList<PartnershipIndividualRecordDetails>, Error> details)
    {
        return details.Map(r =>
            (IReadOnlyList<PartnershipIndividualRecordDetailsDto>) r.Map(item =>
                new PartnershipIndividualRecordDetailsDto(item.PlayerIds, item.Player1, item.Player2, item.Team,
                    item.Opponents,
                    item.Runs, item.Unbroken1, item.Unbroken2, item.KnownAs, item.MatchStartDate)).ToList());
    }

    public static Result<IReadOnlyList<TeamRecordDetailsDto>, Error> ToDto(
        this Result<IReadOnlyList<TeamRecordDetails>, Error> details)
    {
        return details.Map(r =>
            (IReadOnlyList<TeamRecordDetailsDto>) r.Map(item =>
                new TeamRecordDetailsDto(item.Team, item.Opponents, item.Played, item.Wins, item.Drawn, item.Lost,
                    item.Tied,
                    item.Innings, item.TotalRuns, item.WicketsLost, item.Avg, item.Rpo, item.SeriesDate,
                    item.MatchStartYear,
                    item.KnownAs, item.CountryName)).ToList());
    }

    public static Result<IReadOnlyList<TeamExtrasDetailsDto>, Error> ToDto(
        this Result<IReadOnlyList<TeamExtrasDetails>, Error> details)
    {
        return details.Map(r =>
            (IReadOnlyList<TeamExtrasDetailsDto>) r.Map(item =>
                new TeamExtrasDetailsDto(item.Team, item.Played, item.Runs, item.Extras, item.Byes, item.LegByes,
                    item.Wides,
                    item.NoBalls, item.Penalties, item.Balls, item.Wickets, item.Percentage)).ToList());
    }

    public static Result<IReadOnlyList<InningsExtrasDetailsDto>, Error> ToDto(
        this Result<IReadOnlyList<InningsExtrasDetails>, Error> details)
    {
        return details.Map(r =>
            (IReadOnlyList<InningsExtrasDetailsDto>) r.Map(item =>
                new InningsExtrasDetailsDto(item.Team, item.Opponents, item.KnownAs, item.MatchStartDate, item.Overs,
                    item.Total, item.Extras,
                    item.Byes, item.LegByes, item.Wides, item.NoBalls, item.Penalties, item.Percentage)).ToList());
    }

    public static Result<IReadOnlyList<MatchRecordDetailsDto>, Error> ToDto(
        this Result<IReadOnlyList<MatchRecordDetails>, Error> details)
    {
        return details.Map(r =>
            (IReadOnlyList<MatchRecordDetailsDto>) r.Map(item =>
                new MatchRecordDetailsDto(item.Team, item.Opponents, item.MatchTitle, item.Location, item.MatchDate,
                    item.ResultString, item.TotalRuns,
                    item.BallsBowled, item.BallsPerOver, item.TotalWickets)).ToList());
    }

    public static Result<IReadOnlyList<MatchResultDto>, Error> ToDto(
        this Result<IReadOnlyList<MatchResult>, Error> details)
    {
        return details.Map(r =>
            (IReadOnlyList<MatchResultDto>) r.Map(item =>
                new MatchResultDto(item.Team, item.Opponents, item.VictoryType, item.HowMuch, item.KnownAs,
                    item.MatchStartDate, item.TeamId,
                    item.OpponentsId, item.WhoWonId, item.TossTeamId)).ToList());
    }
}