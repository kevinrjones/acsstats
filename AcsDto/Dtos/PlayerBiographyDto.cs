namespace AcsDto.Dtos;

public record NameDetail(string FullName, long UsedFrom);
public record PlayerBiographyDto(List<NameDetail> NameDetails);