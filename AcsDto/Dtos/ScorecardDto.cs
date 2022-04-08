namespace AcsDto.Dtos;

public record  ScorecardDto(List<string> Notes, PersonDto[] Debuts, ScorecardHeaderDto Header,
    List<InningDto> Innings);

public record struct ScorecardHeaderDto(
    ScorecardTeamDto Toss,
    LocationDto Where,
    ResultDto Result,
    int TestNo,
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
    List<string> CloseOfPlay,
    int BallsPerOer,
    List<PersonDto> MatchReferee,
    string MatchDesignator
);


public record struct LocationDto(int Key, string Name, string VictoryType, string ResultString, int VictoryMargin);

public record ResultDto(ScorecardTeamDto? WhoWon, ScorecardTeamDto? WhoLost, string ResultString, string VictoryType);

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
    int Balls,
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

public record DismissalDto(String dismissal);

public record struct BowlingLineDto(
    int? Dots,
    int? Runs,
    int? Balls,
    int? Fours,
    int? Sixes,
    string Overs,
    int? wides,
    PersonDto Player,
    int? Maidens,
    int? NoBalls,
    int? Wickets,
    bool IsCaptain
);

public record struct FallOfWicketDto(int Wicket, int? Score, bool Unbroken, PersonDto Player, string overs);    