namespace Domain;

public class IndividualFieldingDetails : IndividualDetails
{
    public int Dismissals { get; set; }
    public int Caught { get; set; }
    public int Stumpings { get; set; }
    public int CaughtKeeper { get; set; }
    public int CaughtFielder { get; set; }
}