namespace AcsDto.Dtos;

public record InningsExtrasDetailsDto
(
    string Team ,
    string Opponents ,
    string KnownAs ,
    string MatchStartDate ,
    string Overs ,
    int Total ,        
    int Extras ,        
    int Byes ,        
    int LegByes ,        
    int Wides ,        
    int NoBalls ,        
    int Penalties ,
    double? Percentage);