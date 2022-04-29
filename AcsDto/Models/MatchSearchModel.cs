namespace AcsDto.Models;

public class MatchSearchModel
{
    public string? HomeTeam { get; set; }
    public string? AwayTeam { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public int[]? Venue { get; set; }
    public string? MatchType { get; set; }
    public int? MatchResult { get; set; }

    public bool ExactHomeTeamMatch { get; set; }
    public bool ExactAwayTeamMatch { get; set; }
}