using AcsTypes.Types;

namespace Services.Models
{
    public class SharedModel
    {
        public MatchType MatchType { get;  init; }
        public TeamId TeamId { get;  init; }
        public TeamId OpponentsId { get;  init; }
        public GroundId GroundId { get;  init; }
        public CountryId HostCountryId { get;  init; }
        public VenueId HomeVenue { get;  init; }
        public VenueId AwayVenue { get;  init; }
        public VenueId NeutralVenue { get;  init; }

        public SortOrder SortOrder { get; init; }
        public SortDirection SortDirection { get; init; }
        public long StartDateEpoch { get; init; }
        public long EndDateEpoch { get; init; }
        public string Season { get; init; }
        public MatchResult MatchResult { get; init; }

        public ScoreLimit Limit { get; init; }
    }
    
    public class BattingBowlingFieldingModel : SharedModel 
    {
        public bool TeamGrouping { get; set; }
    }

    public class PartnershipModel : SharedModel 
    {
        public bool TeamGrouping { get; set; }
    }

    public static class SharedModelExtensions {
        // ReSharper disable once InconsistentNaming
        public static string BuildQueryString(this SharedModel sharedModel)
        {
            var venue = sharedModel.HomeVenue.Value 
                        | (sharedModel.AwayVenue.Value)
                        | (sharedModel.NeutralVenue.Value);
            return $"?groundid={sharedModel.GroundId.Value}" +
                   $"&hostcountryid={sharedModel.HostCountryId.Value}" +
                   $"&venue={venue}" +
                   $"&Limit={sharedModel.Limit.Value}"+
                   $"&matchresult={sharedModel.MatchResult.Value}" +
                   $"&sortorder={sharedModel.SortOrder}" +
                   $"&sortdirection={sharedModel.SortDirection}" +
                   $"&startDate={sharedModel.StartDateEpoch}" +
                   $"&endDate={sharedModel.EndDateEpoch}" +
                   $"&season={sharedModel.Season}";
        }
    }

}