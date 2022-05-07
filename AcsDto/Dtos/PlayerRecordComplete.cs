namespace AcsDto.Dtos;

public record PlayerRecordComplete(PlayerBiographyDto PlayerBiography
    , List<PlayerOverallDto> PlayerDetails
    , Dictionary<string, List<BattingDetailsDto>> BattingDetails
    , Dictionary<string, List<BowlingDetailsDto>> BowlingDetails
    );