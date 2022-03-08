using System;
using System.Collections.Generic;
using System.Linq;
using AcsTypes.Types;
using Domain;

namespace AcsStatsWeb.Models
{
    public class PartnershipCareerRecordDetailsDisplay
    {
        public PartnershipCareerRecordDetailsDisplay(PartnershipCareerRecordDetails details)
        {
            PlayerIds = details.PlayerIds;
            Avg = details.Avg;
            Fifties = details.Fifties;
            Ground = details.Ground;
            Hundreds = details.Hundreds;
            Innings = details.Innings;
            Opponents = details.Opponents;
            Team = details.Team;
            Unbroken = details.Unbroken;
            Runs = details.Runs;
            CountryName = details.CountryName;
            HighestScore = details.HighestScore;
            NotOuts = details.NotOuts;
            SeriesDate = details.SeriesDate;

            Player1 = new Name(details.Player1);
            Player2 = new Name(details.Player2);
        }

        public string PlayerIds { get; set; }

        private Name Player1 { get; set; }
        private Name Player2 { get; set; }

        public string PlayerNames
        {
            get
            {
                if(String.Compare(Player1.SortNamePart, Player2.SortNamePart, StringComparison.Ordinal) < 0)
                    return $"{Player1.FullName}, {Player2.FullName}";
                else
                    return $"{Player2.FullName}, {Player1.FullName}";
            }
        }

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

    public class PartnershipIndividualRecordDetailsDisplay
    {
        public PartnershipIndividualRecordDetailsDisplay(PartnershipIndividualRecordDetails details)
        {
            PlayerIds = details.PlayerIds;
            Player1 = new Name(details.Player1);
            Player2 = new Name(details.Player2);
            
            Opponents = details.Opponents;
            Team = details.Team;
            Unbroken = details.Unbroken1 && details.Unbroken2;
            Ground = details.KnownAs;
            MatchDate = details.MatchStartDate;
            Runs = details.Runs;
        }
     
        private Name Player1 { get; set; }
        private Name Player2 { get; set; }

        public string PlayerNames
        {
            get
            {
                if(String.Compare(Player1.SortNamePart, Player2.SortNamePart, StringComparison.Ordinal) < 0)
                    return $"{Player1.FullName}, {Player2.FullName}";
                else
                    return $"{Player2.FullName}, {Player1.FullName}";
            }
        }

        public string PlayerIds { get; set; }
        public string Team { get; set; }
        public string Opponents { get; set; }
        public int Runs { get; set; }
        public bool Unbroken { get; set; }
        public string Ground { get; set; }
        public string MatchDate { get; set; }


    }
}