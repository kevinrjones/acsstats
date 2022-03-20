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
    
    
public static class IndividualBowlingDetailsDtoFormatter
{
    public static string Format(this IndividualBowlingDetailsDto dto)
    {

        return $"{dto.FullName}, {dto.Team.Replace(",", ";")}, {dto.Opponents}, {dto.MatchDate}, {dto.Ground.Replace(",", " ")}, " +
               $"{dto.InningsNumber}, {dto.PlayerBalls}, {dto.PlayerMaidens}, {dto.PlayerRuns}, {dto.PlayerWickets}, {dto.BallsPerOver}";
    }

    public static string FormatHeader(this IReadOnlyList<IndividualBowlingDetailsDto> _)
    {
        return $"Name, Team, Opponents, Date, Ground, Innings, Balls, Maidens, Runs, Wickets, Balls Per Over";
    }
}