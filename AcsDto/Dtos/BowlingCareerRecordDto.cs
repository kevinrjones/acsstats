using System.Text;

namespace AcsDto.Dtos;

public record BowlingCareerRecordDto(
    string Name,
    string SortNamePart,
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
    float? Sr,
    float? Bi,
    int? Fours,
    int? Sixes,
    int? FiveFor,
    int? TenFor,
    int? bbiw,
    int? bbir,
    int? bbmw,
    int? bbmr);
    
public static class PlayerBowlingCareerRecordDetailsDtoFormatter
{
    public static string Format(this BowlingCareerRecordDto dto)
    {

        return $"{dto.Name}, {dto.Team.Replace(",", ";")}, {dto.Matches}, " +
               $"{dto.Innings}, {dto.Balls}, {dto.Maidens}, {dto.Runs}, {dto.Wickets}, " +
               $"{dto.Avg}, {dto.Sr}, {dto.Bi}, {dto.Fours}, {dto.Sixes}, {dto.FiveFor}, {dto.TenFor}, {dto.bbiw}/{dto.bbir}, " +
               $"{dto.bbmw}/{dto.bbmr}";
    }

    public static string FormatHeader(this IReadOnlyList<BowlingCareerRecordDto> _)
    {
        return $"Name, Team, Matches, Innings, Balls, Maidens, Runs, Wickets, Avg, Fours, " +
               $"Sixes, FiveFor, TenFor, BBI, BBM";
    }
}