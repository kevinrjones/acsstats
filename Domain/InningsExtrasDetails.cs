namespace Domain
{
    public class InningsExtrasDetails
    {
        public string Team { get; set; }
        public string Opponents { get; set; }
        public string KnownAs { get; set; }
        public string MatchStartDate { get; set; }
        public string Overs { get; set; }

        public int Total { get; set; }        
        public int Extras { get; set; }        
        public int Byes { get; set; }        
        public int LegByes { get; set; }        
        public int Wides { get; set; }        
        public int NoBalls { get; set; }        
        public int Penalties { get; set; }
        public double? Percentage { get; set; }        
        
    }
}