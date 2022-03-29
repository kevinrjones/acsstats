namespace AcsDto.Dtos;

public record TeamRecordDetailsDto(
    string Team,
    string Opponents,
    int Played,
    int Wins,
    int Drawn,
    int Lost,
    int Tied,
    int Innings,
    int TotalRuns,
    int WicketsLost,
    double Avg,
    double Rpo,
    string SeriesDate,
    string MatchStartYear,
    string KnownAs,
    string CountryName
);