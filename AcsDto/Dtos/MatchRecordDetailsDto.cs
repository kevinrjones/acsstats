namespace AcsDto.Dtos;

public record MatchRecordDetailsDto(
    string Team,
    string Opponents,
    string MatchTitle,
    string Location,
    string MatchDate,
    string ResultString,
    string TotalRuns,
    int BallsBowled,
    int BallsPerOver,
    string TotalWickets
);