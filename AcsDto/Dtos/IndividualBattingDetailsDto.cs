namespace AcsDto.Dtos;

public record IndividualBattingDetailsDto(
    string FullName,
    string Team,
    string Opponents,
    int InningsNumber,
    string Ground,
    string MatchDate,
    int PlayerScore,
    int Bat1,
    int Bat2,
    bool NotOut,
    int Position,
    int? Balls,
    int? Fours,
    int? Sixes,
    int? Minutes);