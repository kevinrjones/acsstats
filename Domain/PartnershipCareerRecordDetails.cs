namespace Domain
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PartnershipCareerRecordDetails 
    {
        public string PlayerIds { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string Team { get; set; }
        public string Opponents { get; set; }
        public int Innings { get; set; }
        public int NotOuts { get; set; }
        public int Runs { get; set; }
        public decimal? Avg { get; set; }
        public int Hundreds { get; set; }
        public int Fifties { get; set; }
        public int HighestScore { get; set; }
        public bool Unbroken { get; set; }
        public string Ground { get; set; }
        public string CountryName { get; set; }
        public string SeriesDate { get; set; }
        
    }

    public class PartnershipIndividualRecordDetails
    {
        public string PlayerIds { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public string Team { get; set; }
        public string Opponents { get; set; }
        public int Runs { get; set; }
        public bool Unbroken1 { get; set; }
        public bool Unbroken2 { get; set; }
        public string KnownAs { get; set; }
        public string MatchStartDate { get; set; }
    }
}