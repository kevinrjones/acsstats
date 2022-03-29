namespace Domain;

public class PlayerBowlingCareerRecordDetails : PlayerCareerRecordDetails
{
    public int? Balls { get; set; }
    public int? Maidens { get; set; }
    public int? Runs { get; set; }
    public int Wickets { get; set; }
    public float? Avg { get; set; }
    public int? Fours { get; set; }
    public int? Sixes { get; set; }
    public int? FiveFor { get; set; }
    public int? TenFor { get; set; }
    public int? bbiw { get; set; }
    public int? bbir { get; set; }
    public int? bbmw { get; set; }
    public int? bbmr { get; set; }
}