namespace AcsDto.Dtos;

public record PlayerBattingRecordDto(
    string Name,
    string Team,
    string Opponents,
    string Year,
    int Matches,
    int? Innings,
    string Ground,
    string CountryName,
    int? Runs,
    int? NotOuts,
    int HighestScore,
    bool NotOut,
    float? Avg,
    int Hundreds,
    int Fifties,
    int Ducks,
    int Fours,
    int Sixes,
    int Balls);