namespace AcsDto.Dtos;

public record BattingDetailsDto(int Id, string MatchType, string Team, string Opponents, string Ground, string MatchStartDate,
    int InningsNumber, string Dismissal, int? DismissalType, int? FielderId, string FielderName, int? BowlerId,
    string BowlerName, int Score, int Position, ulong NotOut, int? Balls, int? Minutes, int? Fours, int? Sixes,
    ulong Captain, ulong WicketKeeper, float? SR);