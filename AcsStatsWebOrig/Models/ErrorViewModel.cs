using AcsTypes.Error;

namespace AcsStatsWeb.Models
{
  public class ErrorViewModel
  {

    public Error Error { get; }

    public ErrorViewModel(Error error)
    {
      Error = error;
    }
  }
}
