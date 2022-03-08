using System;

namespace Domain
{
    public class IndividualDetails 
    {
        public string FullName { get; set; }
        public string Team { get; set; }
        public string Opponents { get; set; }
        public int InningsNumber { get; set; }
        public string Ground { get; set; }
        public string MatchDate { get; set; }
        
    }
    public class IndividualBattingDetails : IndividualDetails
    {
        public int PlayerScore { get; set; }
        public int Bat1 { get; set; }
        public int Bat2 { get; set; }
        public bool NotOut { get; set; }
        public int Position { get; set; }
        public int? Balls { get; set; }
        public int? Fours { get; set; }
        public int? Sixes { get; set; }
        public int? Minutes { get; set; }
    }

    public class IndividualBowlingDetails : IndividualDetails
    {
        public int PlayerBalls { get; set; }
        public int? PlayerMaidens { get; set; }
        public int PlayerRuns { get; set; }
        public int PlayerWickets { get; set; }
        public int BallsPerOver { get; set; }
        
    }

    public class IndividualFieldingDetails : IndividualDetails
    {
        public int Dismissals { get; set; }
        public int Caught { get; set; }
        public int Stumpings { get; set; }
        public int CaughtKeeper { get; set; }
        public int CaughtFielder { get; set; }
    }
}