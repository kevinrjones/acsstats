using System;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Services.Models;

namespace AcsStatsWeb.Models.api
{
    public class ApiRecordInputModel
    {

        [FromRoute] public int TeamId { get; set; }

        [FromRoute] public int OpponentsId { get; set; }
        // {
        //     get => throw new Exception("Use public opponentsId property instead");
        //     set => opponentsId = AcsTypes.Types.TeamId.Create(value);
        // }

        [FromRoute]
        public string MatchType{ get; set; }

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