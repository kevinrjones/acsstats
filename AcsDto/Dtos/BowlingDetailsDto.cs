namespace AcsDto.Dtos;

public record BowlingDetailsDto(int Id, string MatchType, string Team, string Opponents, string Ground, string MatchStartDate,
  int InningsNumber, int Runs, int Balls, int Wickets, int Maidens, int? Dots, int NoBalls, int Wides, int Fours, int Sixes,
  ulong Captain, int BallsPerOver);