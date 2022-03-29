using System.Text;

namespace AcsDto.Dtos;

public record IndividualFieldingDetailsDto(
    string FullName,
    string Team,
    string Opponents,
    int InningsNumber,
    string Ground,
    string MatchDate,
    int Dismissals,
    int Caught,
    int Stumpings,
    int CaughtKeeper,
    int CaughtFielder
);

public static class IndividualFieldingDetailsDtoFormatter
{
    public static string Format(this IndividualFieldingDetailsDto dto)
    {
        return $"{dto.FullName}, {dto.Team}, {dto.Opponents}, {dto.Ground}, " +
               $"{dto.MatchDate}, {dto.Dismissals}, {dto.Caught}, {dto.Stumpings}, {dto.CaughtFielder}" +
               $"{dto.CaughtKeeper}";
    }

    public static string FormatHeader(this IReadOnlyList<IndividualFieldingDetailsDto> dto)
    {
        return $"Name, Team, Opponents, Ground, Date, Dismissals, Caught, Stumpings, Caught Fielder, Caught Keeper";
    }
}