namespace AcsStatsWeb.Dtos;

public record TeamDto(int Id, string MatchType, string Name);

public record GroundDto(
    int Id,
    string MatchType,
    string Code,
    string CountryName,
    int GroundId,
    string KnownAs
);