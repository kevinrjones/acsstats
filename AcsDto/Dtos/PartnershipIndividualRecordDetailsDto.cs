namespace AcsDto.Dtos;

public record PartnershipIndividualRecordDetailsDto
(
    string PlayerIds,
    string Player1,
    string Player2,
    string Team,
    string Opponents,
    int Runs,
    bool Unbroken1,
    bool Unbroken2,
    string KnownAs,
    string MatchStartDate
);