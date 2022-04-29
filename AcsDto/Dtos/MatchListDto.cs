namespace AcsDto.Dtos;

public record MatchListDto(string HomeTeamName, string AwayTeamName, string Location, int LocationId, string MatchDate, string MatchTitle, string Tournament, string ResultString, string Key);