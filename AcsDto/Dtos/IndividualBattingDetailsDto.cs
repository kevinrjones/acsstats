namespace AcsDto.Dtos;

public record IndividualBattingDetailsDto(
    string FullName,
    string SortNamePart,
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
    int? Minutes,
    float? SR);

public static class IndividualBattingDetailsDtoFormatter
{
    public static string Format(this IndividualBattingDetailsDto dto)
    {
        var hs = dto.NotOut ? $"{dto.PlayerScore}*" : $"{dto.PlayerScore}";
        return $"{dto.FullName}, {dto.Team}, {dto.Opponents}, {dto.Ground.Replace(",", " ")}, " +
                          $"{dto.MatchDate}, {hs}, " +
                          $"{dto.Balls}, {dto.Fours}, {dto.Sixes}, {dto.Minutes}";
    }

    public static string FormatHeader(this IReadOnlyList<IndividualBattingDetailsDto> dto)
    {
        return $"Name, Team, Opponents, Ground, Date, Score, Balls, Fours, Sixes, Minutes";
    }
}