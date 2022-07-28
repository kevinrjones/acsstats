namespace AcsStatsWeb.Dtos;

public record TeamDto(int Id, string Name, string MatchType);

public record GroundWithCodeDto(
    int Id,
    string MatchType,
    string Code,
    string CountryName,
    int GroundId,
    string KnownAs
);

public record GroundDto(
    int Id,
    int GroundId,
    string KnownAs,
    int CountryId,
    string CountryName
);