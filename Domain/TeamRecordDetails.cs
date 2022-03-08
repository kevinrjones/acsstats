namespace Domain
{
    public class TeamRecordDetails
    {
        public string Team { get; set; }
        public string Opponents { get; set; }
        public int Played { get; set; }        
        public int Wins { get; set; }        
        public int Drawn { get; set; }        
        public int Lost { get; set; }        
        public int Tied { get; set; }        
        public int Innings { get; set; }        
        public int TotalRuns { get; set; }        
        public int WicketsLost { get; set; }        
        public double Avg { get; set; }        
        public double Rpo { get; set; }
        public string SeriesDate { get; set; }        
        public string MatchStartYear { get; set; }        
        public string KnownAs { get; set; }        
        public string CountryName { get; set; }        
    }
}