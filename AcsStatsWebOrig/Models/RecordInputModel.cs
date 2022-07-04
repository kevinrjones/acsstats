using System;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Services;
using Services.Models;
// ReSharper disable InconsistentNaming

namespace AcsStatsWeb.Models
{
  public class RecordInputModel
  {
    // ReSharper disable once MemberCanBeProtected.Global
    public RecordInputModel()
    {
      MatchResult = new int[4];
    }
    public int Limit { get; set; }


    public int TeamId { get; set; }

    public int OpponentsId { get; set; }

    public string MatchType { get; set; }

    public int GroundId { get; set; }

    public int HostCountryId { get; set; }


    public int HomeVenue { get; set; }
    public int AwayVenue { get; set; }
    public int NeutralVenue { get; set; }

    public SortOrder SortOrder { get; private set; }
    public SortDirection SortDirection { get; private set; }

    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string Season { get; set; }
    public int[] MatchResult { get; set; }

    public int Format { get; set; }

    public void UpdateSortOrder(SortOrder sortOrder)
    {
      if (SortOrder == SortOrder.Undefined)
      {
        SortOrder = sortOrder;
        SortDirection = SortDirection.Desc;
      }
    }

  }

  public class TeamRecordInputModel : RecordInputModel
  {
  }
}