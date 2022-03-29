namespace AcsDto.Dtos;

public record TeamExtrasDetailsDto
(
    string Team,
    int Played,
    int Runs,
    int Extras,
    int Byes,
    int LegByes,
    int Wides,
    int NoBalls,
    int Penalties,
    int Balls,
    int Wickets,
    double Percentage);