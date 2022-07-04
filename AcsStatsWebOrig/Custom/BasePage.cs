using AcsStatsWeb.Models;
using Microsoft.AspNetCore.Mvc.Razor;

namespace AcsStatsWeb.Custom
{
    public abstract class BaseRazorPage<TModel> : RazorPage<TModel> where TModel : ResultsModel
    {
        protected string BaseUrl;
        protected string NameClass = "";
        protected string TeamClass = "";
        protected string PartnershipsClass = "";
        protected string OpponentsClass = "";
        protected string PlayedClass = "";
        protected string InningsClass = "";
        protected string DismissalsClass = "";
        protected string CaughtClass = "";
        protected string GroundClass = "";
        protected string StumpedClass = "";
        protected string CwkClass = "";
        protected string CfClass = "";
        protected string BestDismissalsClass = "";
        protected string SeasonClass = "";
        protected string WinsClass = "";
        protected string LossesClass = "";
        protected string DrawsClass = "";
        protected string TiedClass = "";
        protected string ExtrasClass = "";
        protected string ByesClass = "";
        protected string LegByesClass = "";
        protected string WidesClass = "";
        protected string NoBallsClass = "";
        protected string PenaltiesClass = "";
        protected string RunsClass = "";
        protected string Bat1Class = "";
        protected string Bat2Class = "";
        protected string WicketsClass = "";
        protected string BallsClass = "";
        protected string FoursClass = "";
        protected string SixesClass = "";
        protected string PercentageClass = "";
        protected string TotalsClass = "";
        protected string HostCountryClass = "";
        protected string OversClass = "";
        protected string StartDateClass = "";
        protected string MarginClass = "";
        protected string SeriesDateClass = "";
        protected string MinutesClass = "";
        protected string HSClass = "";
        protected string AvgClass = "";
        protected string RpoClass = "";
        protected string HundredsClass = "";
        protected string FiftiesClass = "";
        protected string DucksClass = "";
        protected string BBIClass = "";
        protected string BBMClass = "";
        protected string FiveForClass = "";
        protected string TenForClass = "";
        protected string MaidensClass = "";

        protected void SetClasses()
        {
            BaseUrl = Model.GetBaseUrl();
            if (Context.Request.Query.ContainsKey("sortorder"))
            {
                if (Context.Request.Query["sortorder"] == "name")
                {
                    NameClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "played")
                {
                    PlayedClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "innings")
                {
                    InningsClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "season" || Context.Request.Query["sortorder"] == "matchstartyear")
                {
                    SeasonClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "dismissals")
                {
                    DismissalsClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "caught")
                {
                    CaughtClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "stumpings")
                {
                    StumpedClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "cwk")
                {
                    CwkClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "cf")
                {
                    CfClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "bestdismissals")
                {
                    BestDismissalsClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "team")
                {
                    TeamClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "opponents")
                {
                    OpponentsClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "wins")
                {
                    WinsClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "lost")
                {
                    LossesClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "drawn")
                {
                    DrawsClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "tied")
                {
                    TiedClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "extras")
                {
                    ExtrasClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "byes")
                {
                    ByesClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "legbyes")
                {
                    LegByesClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "wides")
                {
                    WidesClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "noballs")
                {
                    NoBallsClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "penalties")
                {
                    PenaltiesClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "runs")
                {
                    RunsClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "wickets")
                {
                    WicketsClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "balls")
                {
                    BallsClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "percentage")
                {
                    PercentageClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "ground")
                {
                    GroundClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "totals")
                {
                    TotalsClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "host")
                {
                    HostCountryClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "overs")
                {
                    OversClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "matchstartdate")
                {
                    StartDateClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "VictoryMargin")
                {
                    MarginClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "seriesdate")
                {
                    SeriesDateClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "fours")
                {
                    FoursClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "sixes")
                {
                    SixesClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "bat1")
                {
                    Bat1Class = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "bat2")
                {
                    Bat2Class = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "minutes")
                {
                    MinutesClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "hs")
                {
                    HSClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "avg")
                {
                    AvgClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "hundreds")
                {
                    HundredsClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "fifties")
                {
                    FiftiesClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "ducks")
                {
                    DucksClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "bbi")
                {
                    BBIClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "bbm")
                {
                    BBMClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "fivefor")
                {
                    FiveForClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "tenfor")
                {
                    TenForClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "maidens")
                {
                    MaidensClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
                else if (Context.Request.Query["sortorder"] == "rpo")
                {
                    RpoClass = Model.SortDirection.ToUpper() == "ASC" ? "fas fa-arrow-down" : "fas fa-arrow-up";
                }
            }
        }
    }
}