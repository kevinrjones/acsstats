using System;

namespace Domain
{
    public class IndividualBattingDetails : IndividualDetails
    {
        public int PlayerScore { get; set; }
        public int Bat1 { get; set; }
        public int Bat2 { get; set; }
        public bool NotOut { get; set; }
        public bool Captain { get; set; }
        public int Position { get; set; }
        public string Dismissal { get; set; }
        public string Bowler { get; set; }
        public string Fielder { get; set; }
        public int? Balls { get; set; }
        public int? Fours { get; set; }
        public int? Sixes { get; set; }
        public int? Minutes { get; set; }
    }
}