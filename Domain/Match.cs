namespace Domain
{
  public class Match : BaseEntity
  {
    public string MatchType { get; set; }
    public string SeriesDate { get; set; }
    public string MatchStartDate { get; set; }
    public long MatchStartDateAsOffset { get; set; }
  }
}