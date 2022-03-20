using System.Text;

namespace AcsDto.Dtos;

public record FieldingCareerRecordDto(
    string Name,
    string Team,
    string Opponents,
    string Year,
    int Matches,
    int? Innings,
    string Ground,
    string CountryName,
    int Dismissals,
    int Caught,
    int Stumpings,
    int CaughtKeeper,
    int CaughtFielder,
    int BestDismissals,
    int BestCaughtFielder,
    int BestCaughtKeeper,
    int BestStumpings);

public static class PlayerFieldingRecordDtoFormatter
{
    public static string Format(this FieldingCareerRecordDto dto)
    {
        return
            $"{dto.Name}, {dto.Team.Replace(",", ";")}, {dto.Matches}, {dto.Innings}, {dto.Ground}, {dto.CountryName}, " +
            $"{dto.Dismissals}, {dto.Caught}, {dto.Stumpings}, {dto.CaughtKeeper}, {dto.CaughtFielder}, " +
            $"{dto.BestDismissals}, {dto.BestCaughtFielder}, {dto.BestCaughtKeeper}, {dto.BestStumpings}";
    }

    public static string FormatHeader(this IReadOnlyList<FieldingCareerRecordDto> _)
    {
        return $"Name, Team, Matches, Innings, Ground, Country, Dismissals, Caught, Stumped, Caught by Keeper, " +
               $"Caught by Fielder, Best Dismissals, Best Caught Fielder, Best Caught Keeper, Best Stumpings";
    }
}