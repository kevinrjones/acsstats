namespace AcsDto.Models;

public class PlayerSearchModel
{
  public string? Name { get; set; }
  public string? Team { get; set; }
  public bool ExactNameMatch { get; set; }
  public string? DebutDate { get; set; }
  public string? ActiveUntil { get; set; }
}