namespace AcsDto.Dtos;

public record MatchResultDto(
    string Team,
    string Opponents,
    int VictoryType,
    int HowMuch,
    string KnownAs,
    string MatchStartDate,
    int TeamId,
    int OpponentsId,
    int WhoWonId,
    int TossTeamId);