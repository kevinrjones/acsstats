using System.Text;

namespace AcsDto.Dtos;

public record BattingCareerRecordDto(
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

public static class PlayerBattingRecordDtoFormatter
{
    public static string Format(this BattingCareerRecordDto dto)
    {
        StringBuilder buffer = new StringBuilder();
        var hs = dto.NotOut ? $"{dto.HighestScore}*" : $"{dto.HighestScore}";
        
        return    $"{dto.Name}, {dto.Team.Replace(",", ";")}, {dto.Matches}, {dto.Innings}, {dto.Runs}, {dto.NotOuts}, " +
            $"{hs}, {dto.Avg}, {dto.Hundreds}, {dto.Fifties}, {dto.Ducks}, " +
            $"{dto.Sixes}, {dto.Fours}, {dto.Balls}";
        
    }

    public static string FormatHeader(this IReadOnlyList<BattingCareerRecordDto> _)
    {
        return $"Name, Team, Matches, Innings, Runs, NotOuts, Highest Score, Average, Hundreds, " +
               $"Fifties, Ducks, Sixes, Fours, Balls";
    }
}