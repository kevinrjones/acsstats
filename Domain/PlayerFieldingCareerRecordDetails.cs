namespace Domain;

public class PlayerFieldingCareerRecordDetails : PlayerCareerRecordDetails
{
    public int Dismissals { get; set; }
    public int Caught { get; set; }
    public int Stumpings { get; set; }
    public int CaughtKeeper { get; set; }
    public int CaughtFielder { get; set; }
    public int BestDismissals { get; set; }
    public int BestCaughtFielder { get; set; }
    public int BestCaughtKeeper { get; set; }
    public int BestStumpings { get; set; }
}