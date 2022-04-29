namespace AcsDto.Dtos;

public record  ScorecardDto(List<string> Notes, List<DebutDto> Debuts, ScorecardHeaderDto Header,
    List<InningDto> Innings);

public record struct ScorecardHeaderDto(
    ScorecardTeamDto? Toss,
    LocationDto Where,
    ResultDto Result,
    List<PersonDto> Scorers,
    List<PersonDto> Umpires,
    ScorecardTeamDto AwayTeam,
    string[] AwayTeamScores,
    bool DayNight,
    ScorecardTeamDto HomeTeam,
    string[] HomeTeamScores,
    List<PersonDto> TvUmpires,
    string MatchDate,
    string MatchType,
    string MatchTitle,
    string SeriesDate,
    List<CloseOfPlayDto> CloseOfPlay,
    int BallsPerOver,
    List<PersonDto> MatchReferee,
    string MatchDesignator
);

public enum  VictoryType {
    Awarded = 0,
    Drawn,
    Runs,
    Wickets,
    Innings,
    Tied,
    Abandoned,
    NoResult,
    RunRate,
    LosingFewerWickets,
    FasterScoringRate,
    Unknown
}

public record struct LocationDto(int Key, string Name);
public record struct CloseOfPlayDto(int Day, string Note); 
public record struct DebutDto(int PlayerId, string FullName, int TeamId, string TeamName);

public record ResultDto(ScorecardTeamDto? WhoWon, ScorecardTeamDto? WhoLost, string ResultString, VictoryType VictoryType);

public record struct InningDto(ScorecardTeamDto Team,
    ScorecardTeamDto Opponents,
    int InningsNumber,
    int InningsOrder,
    TotalDto Total,
    ExtrasDto Extras,
    List<BattlingLineDto> BattingLines,
    List<BowlingLineDto> BowlingLines,
    List<FallOfWicketDto> FallOfWickets);

public record struct ScorecardTeamDto(int Key, string name);

public record struct PersonDto(int Key, String Name);

public record struct TotalDto(
    int Wickets,
    bool Declared,
    string Overs,
    int Minutes,
    int Total
);

public record struct ExtrasDto(
    int Byes,
    int LegByes,
    int Wides,
    int NoBalls,
    int Pens,
    int Total
);

public record struct BattlingLineDto(
    PersonDto Player,
    int? Runs,
    int? Balls,
    int? Fours,
    int? Sixes,
    bool NotOut,
    int? Minutes,
    int Position,
    DismissalDto Dismissal,
    bool IsCaptain,
    bool IsWicketKeeper
);

public record DismissalDto(String Dismissal, PersonDto Bowler, PersonDto Fielder);

public record struct BowlingLineDto(
    int? Dots,
    int? Runs,
    int? Balls,
    int? Fours,
    int? Sixes,
    string Overs,
    int? Wides,
    PersonDto Player,
    int? Maidens,
    int? NoBalls,
    int? Wickets,
    bool IsCaptain
);

public record struct FallOfWicketDto(int Wicket, int? Score, PersonDto Player, string overs);    