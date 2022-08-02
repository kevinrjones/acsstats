using Microsoft.AspNetCore.Mvc;
using Services.Models;

namespace AcsStatsAngular.Models.api
{
    public class ApiRecordInputModel
    {

        [FromRoute] public int TeamId { get; set; }

        [FromRoute] public int OpponentsId { get; set; }

        [FromRoute]
        public string MatchType{ get; set; }
        [FromQuery]
        public string MatchSubType{ get; set; }

        [FromQuery]
        public int GroundId{ get; set; }

        [FromQuery]
        public int HostCountryId{ get; set; }

        [FromQuery]
        public int Venue{ get; set; }

        [FromQuery]
        public int Limit{ get; set; }

        [FromQuery]
        public int MatchResult{ get; set; }

        [FromQuery] public SortOrder SortOrder { get; set; }
        [FromQuery] public SortDirection SortDirection { get; set; }
        [FromQuery] public long StartDate { get; set; }
        [FromQuery] public long EndDate { get; set; }
        [FromQuery] public string Season { get; set; }
        [FromQuery] public int StartRow { get; set; }
        [FromQuery] public int PageSize { get; set; }

        public override string ToString()
        {
            return
                $"matchResult: {MatchResult}, matchType: {MatchType}, teamId: {TeamId}, opponentsId: {OpponentsId}, " +
                $"groundId: {GroundId}, hostCountryId: {HostCountryId}, venue: {Venue}, limit: {Limit}." +
                $"SortOrder {SortOrder}, SortDirection: {SortDirection}, " +
                $"StartDate: {StartDate}, EndDate: {EndDate}, Season: {Season}";
        }
    }
}