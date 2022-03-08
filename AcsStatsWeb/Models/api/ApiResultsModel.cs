using System.Collections.Generic;
using Domain;

namespace AcsStatsWeb.Models.api
{
    public class ApiResultsModel
    {

        public int TeamId { get; set; }
        public int OpponentsId { get; set; }
        public string MatchType { get; set; }
        public string Team { get; set; }
        public string Opponents { get; set; }
        public int HostCountryId { get; set; }
        public string HostCountryName { get; set; }
        public string TeamGrouping { get; set; }

        public int HomeVenue { get; set; }
        public int AwayVenue { get; set; }
        public int NeutralVenue { get; set; }

        public long StartDate { get; set; }
        public long EndDate { get; set; }
        public string Season { get; set; }

    }
    
    public class ApiTeamResultsModel : ApiResultsModel
    {
        public List<MatchRecordDetails> MatchRecordDetails { get; set; }
        public List<TeamRecordDetails> TeamRecordDetails { get; set; }
        public List<MatchResult> ResultDetails { get; set; }
        public List<TeamExtrasDetails> TeamExtrasDetails { get; set; }
        public List<InningsExtrasDetails> InningsExtrasDetails { get; set; }

    }

}