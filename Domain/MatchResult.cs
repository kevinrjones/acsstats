namespace Domain
{
    public class MatchResult
    {
        public string Team { get; set; }
        public string Opponents { get; set; }
        public int VictoryType { get; set; }        
        public int HowMuch { get; set; }        
        public string KnownAs { get; set; }
        public string MatchStartDate { get; set; }
        public int TeamId { get; set; }
        public int OpponentsId { get; set; }
        public int WhoWonId { get; set; }
        public int TossTeamId { get; set; }
    }
}