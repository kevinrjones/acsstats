using System;

namespace Domain
{
  public class Ground : BaseEntity<int>
  {
    public int GroundId { get; set; }
    public string KnownAs { get; set; }
    public int CountryId { get; set; }
    public string CountryName { get; set; }
    public string MatchType { get; set; }
  }
  
  public class GroundsName : BaseEntity<int>
  {
    public string GroundId { get; set; }
    public string Name { get; set; }
  }
  
  public class CountryCodes : BaseEntity<int>
  {
    public string Country { get; set; }
    public string Code { get; set; }
  }
  
  public class GroundsWithCodes
  {
    public int Id { get; set; }
    public string MatchType { get; set; }
    public int GroundId { get; set; }
    public string KnownAs { get; set; }
    public string CountryName { get; set; }
    public string Code { get; set; }
  }

  public class Country : BaseEntity<int>
  {
    public string Name { get; set; }
    public string MatchType { get; set; }
  }

  public class MatchDate
  {
    public string Id { get; set; }
    public string Date { get; set; }
    public long DateOffset { get; set; }
    public string MatchType { get; set; }
  }
}