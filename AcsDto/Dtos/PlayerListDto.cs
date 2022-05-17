using AcsTypes.Types;

namespace AcsDto.Dtos;


public record PlayerSearchDto(string? Name, string? Team, bool ExactMatch, string? DebutDate, string? ActiveUntilDate, IReadOnlyList<PlayerListDto> Players);

public record PlayerListDto(int Id, string FullName, string Teams, string Debut, string ActiveUntil);