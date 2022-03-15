namespace AcsDto.Dtos;

public record IndividualBowlingDetailsDto(
    string FullName,
    string Team,
    string Opponents,
    int InningsNumber,
    string Ground,
    string MatchDate,
    int PlayerBalls,
    int? PlayerMaidens,
    int PlayerRuns,
    int PlayerWickets,
    int BallsPerOver);