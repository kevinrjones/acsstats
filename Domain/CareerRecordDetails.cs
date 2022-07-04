
using System;

namespace Domain
{
  public class PlayerCareerRecordDetails
  {
    public string Name { get; set; }
    public string Team { get; set; }
    public string SortNamePart { get; set; }
    public string Opponents { get; set; }
    public string Year { get; set; }
    public int Matches { get; set; }
    public int? Innings { get; set; }
    public string Ground { get; set; }
    public string CountryName { get; set; }
  }
}