namespace AcsDto.Dtos;

public record PartnershipCareerRecordDetailsDto(
    string PlayerIds,
    string Player1,
    string Player2,
    string Team,
    string Opponents,
    int Innings,
    int NotOuts,
    int Runs,
    decimal? Avg,
    int Hundreds,
    int Fifties,
    int HighestScore,
    bool Unbroken,
    string Ground,
    string CountryName,
    string SeriesDate);