using System;

namespace Domain
{
    public class MatchRecordDetails // : BaseEntity
    {
        public string Team { get; set; }
        public string Opponents { get; set; }
        public string MatchTitle { get; set; }
        public string Location { get; set; }
        public string MatchDate { get; set; }
        public string ResultString { get; set; }
        public string TotalRuns { get; set; }
        public int BallsBowled { get; set; }
        public int BallsPerOver { get; set; }
        public string TotalWickets { get; set; }
    }
}