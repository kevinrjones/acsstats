using System;
using System.Collections.Generic;
using AcsDto.Dtos;
using AcsStatsWeb.Dtos;
using Domain;
using Services;
using Services.Models;
// ReSharper disable InconsistentNaming

namespace AcsStatsWeb.Models
{
    public class ResultsModel
    {
        public ResultsModel()
        {
            MatchResult = new int[4];
        }

        public int TeamId { get; set; }
        public int OpponentsId { get; set; }
        public string MatchType { get; set; }
        public string Team { get; set; }
        public string Opponents { get; set; }
        public int GroundId { get; set; }
        public string Ground { get; set; }
        public int HostCountryId { get; set; }
        public string HostCountryName { get; set; }
        public bool ShowTeamInList { get; set; }
        public bool ShowOpponentsInList { get; set; }
        public string Title { get; set; }
        public string SortDirection { get; set; }
        public string TeamGrouping { get; set; }

        public int HomeVenue { get; set; }
        public int AwayVenue { get; set; }
        public int NeutralVenue { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Season { get; set; }

        public int[] MatchResult { get; set; }

        public int Format { get; set; }

        public void SetOppositeSortDirection(SortDirection sortDirection)
        {
            SortDirection = sortDirection == Services.Models.SortDirection.Asc ? "Desc" : "Asc";
        }

        public void UpdateMatchResult(int[] fromMatchResult)
        {
            Array.Copy(fromMatchResult, MatchResult, fromMatchResult.Length);
        }
        
        public int Limit { get; set; }
        public virtual string GetBaseUrl()
        {
            var baseUrl = $"teamid={TeamId}&opponentsid={OpponentsId}&matchtype={MatchType}" +
                          $"&sortdirection={SortDirection}&groundid={GroundId}&teamGrouping={TeamGrouping}" +
                          $"&hostcountryid={HostCountryId}&venue={HomeVenue}&startDate={StartDate}&endDate={EndDate}" +
                          $"&season={Season}&won={MatchResult[0]}&lost={MatchResult[1]}&drawn={MatchResult[2]}&tied={MatchResult[3]}" +
                          $"&format={Format}&Limit={Limit}";
            

            return baseUrl;
        }

        public string GetMatchType()
        {
            switch (MatchType)
            {
                case "wo":
                case "wa":
                case "o":
                case "a":
                    return "One Day Internationals";
                    
                case "witt":
                case "itt":
                case "wtt":
                case "tt":
                    return "T20s";
                    
                case "wf":
                case "f":
                    return "First Class";
                    
                case "wt":
                case "t":
                    return "Test Matches";
                default:
                    return "Unknown";
            }
        }

        public string AtGroundName()
        {
            return String.IsNullOrEmpty(Ground) ? "" : $"(at {Ground})";
        }

        public string GetHostCountry()
        {
            return string.IsNullOrEmpty(HostCountryName) ? "" : $"(in {HostCountryName})";
        }

        public int GetVenueIds()
        {
            return HomeVenue | AwayVenue | NeutralVenue;
        }

        public string GetVenue()
        {
            var venueIds = GetVenueIds();
            return venueIds == 1 ? "at Home Only" : venueIds == 2 ? "at Away Only" : venueIds == 3 ? "at Home and Away only"
                : venueIds == 4 ? "neutral grounds" : venueIds == 5 ? "at Home and Neutral" : venueIds == 6 ? "at Away and Neutral" : "at all venues";
        }
    }

    public class ResultsBattingModel : ResultsModel
    {
        public List<BattingCareerRecordDto> PlayerRecordDetails { get; set; }
        public List<IndividualBattingDetailsDto> IndividualBattingDetails { get; set; }
        
    }

    public class ResultsBowlingModel : ResultsModel
    {
        public List<BowlingCareerRecordDetailsDto> PlayerRecordDetails { get; set; }
        public List<IndividualBowlingDetailsDto> IndividualBowlingDetails { get; set; }
        
    }

    public class ResultsPartnershipModel : ResultsModel
    {
        public List<PartnershipCareerRecordDetailsDisplay> PartnershipDetails { get; set; }
        public List<PartnershipIndividualRecordDetailsDisplay> IndividualPartnershipDetails { get; set; }
        
    }


    public class ResultsFieldingModel : ResultsModel
    {
        public List<PlayerFieldingCareerRecordDetails> PlayerRecordDetails { get; set; }
        public List<IndividualFieldingDetails> IndividualFieldingDetails { get; set; }
        
    }
    
    public class TeamResultsModel : ResultsModel
    {
        public List<MatchRecordDetails> MatchRecordDetails { get; set; }
        public List<TeamRecordDetails> TeamRecordDetails { get; set; }
        public List<MatchResult> ResultDetails { get; set; }
        public List<TeamExtrasDetails> TeamExtrasDetails { get; set; }
        public List<InningsExtrasDetails> InningsExtrasDetails { get; set; }

        public override string GetBaseUrl()
        {
            var ret = base.GetBaseUrl();
            return ret + $"&limit={Limit}";
        }

    }

}