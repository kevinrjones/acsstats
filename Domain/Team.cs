namespace Domain
{
    public class Team : BaseEntity<int>
    {
        public string MatchType { get; set; }
        public string Name { get; set; }
        public string TeamId { get; set; }
    }
}