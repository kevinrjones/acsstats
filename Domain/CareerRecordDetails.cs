
using System;

namespace Domain
{
  public class PlayerCareerRecordDetails
  {
    public string Name { get; set; }
    public string Team { get; set; }
    public string Opponents { get; set; }
    public string Year { get; set; }
    public int Matches { get; set; }
    public int? Innings { get; set; }
    public string Ground { get; set; }
    public string CountryName { get; set; }
  }
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
  
  public class PlayerBowlingCareerRecordDetails : PlayerCareerRecordDetails
  {
    public int? Balls { get; set; }
    public int? Maidens { get; set; }
    public int? Runs { get; set; }
    public int Wickets { get; set; }
    public float? Avg { get; set; }
    public int? NoBalls { get; set; }
    public int? Wides { get; set; }
    public int? Dots { get; set; }
    public int? Fours { get; set; }
    public int? Sixes { get; set; }
    public int? FiveFor { get; set; }
    public int? TenFor { get; set; }
    public int? bbiw { get; set; }
    public int? bbir { get; set; }
    public int? bbmw { get; set; }
    public int? bbmr { get; set; }
  }

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
}