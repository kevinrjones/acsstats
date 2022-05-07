namespace AcsDto.Dtos;

public record PlayerOverallDto(string Team, string MatchType, int TeamId, long Matches, decimal Runs, decimal Innings, decimal Notouts, decimal Balls,
    decimal Fours, decimal Sixes, decimal Hundreds, decimal Fifties, decimal? BowlingBalls, decimal? BowlingRuns,
    decimal? Maidens, decimal? Wickets, decimal? BowlingFours, decimal? BowlingSixes, decimal? Wides, decimal? NoBalls);

public record PlayerBattingOverallDto(string MatchType, int TeamId, string Team, long Matches, decimal Runs, long innings, decimal Notouts, decimal Balls,
  decimal Fours, decimal Sixes, decimal Hundreds, decimal Fifties);
  
public record PlayerBowlingOverallDto(string MatchType, int TeamId, long Matches, decimal? BowlingBalls, decimal? BowlingRuns,
  decimal? Maidens, decimal? Wickets, decimal? BowlingFours, decimal? BowlingSixes, decimal? Wides, decimal? NoBalls);
