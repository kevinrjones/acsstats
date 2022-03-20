namespace Domain;

public class PlayerBattingCareerRecordDetails : PlayerCareerRecordDetails
{
    public int? Runs { get; set; }
    public int? NotOuts { get; set; }
    public int HighestScore { get; set; }
    public bool NotOut { get; set; }
    public float? Avg { get; set; }
    public int? Hundreds { get; set; }
    public int? Fifties { get; set; }
    public int? Ducks { get; set; }
    public int? Fours { get; set; }
    public int? Sixes { get; set; }
    public int? Balls { get; set; }
}