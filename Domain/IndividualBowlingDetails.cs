namespace Domain;

public class IndividualBowlingDetails : IndividualDetails
{
    public int PlayerBalls { get; set; }
    public int? PlayerMaidens { get; set; }
    public int PlayerRuns { get; set; }
    public int PlayerWickets { get; set; }
    public int BallsPerOver { get; set; }
        
}