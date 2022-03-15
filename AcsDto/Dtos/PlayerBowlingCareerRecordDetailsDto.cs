namespace AcsDto.Dtos;

public record PlayerBowlingCareerRecordDetailsDto(
    string Name,
    string Team,
    string Opponents,
    string Year,
    int Matches,
    int? Innings,
    string Ground,
    string CountryName,
    int? Balls,
    int? Maidens,
    int? Runs,
    int Wickets,
    float? Avg,
    int? Fours,
    int? Sixes,
    int? FiveFor,
    int? TenFor,
    int? bbiw,
    int? bbir,
    int? bbmw,
    int? bbmr);