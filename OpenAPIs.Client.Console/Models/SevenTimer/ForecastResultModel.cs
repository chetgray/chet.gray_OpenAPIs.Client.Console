using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace OpenAPIs.Client.Console.Models.SevenTimer
{
    public class ForecastResultModel
    {
        public static Dictionary<int, string> AstroSeeingDescriptions { get; } =
            new Dictionary<int, string>
            {
                { 1, "<0.5\"" },
                { 2, "0.5\"-0.75\"" },
                { 3, "0.75\"-1\"" },
                { 4, "1\"-1.25\"" },
                { 5, "1.25\"-1.5\"" },
                { 6, "1.5\"-2\"" },
                { 7, "2\"-2.5\"" },
                { 8, ">2.5\"" },
            };
        public static Dictionary<int, string> CloudCoverDescriptions { get; } =
            new Dictionary<int, string>
            {
                { 1, "0%-6%" },
                { 2, "6%-19%" },
                { 3, "19%-31%" },
                { 4, "31%-44%" },
                { 5, "44%-56%" },
                { 6, "56%-69%" },
                { 7, "69%-81%" },
                { 8, "81%-94%" },
                { 9, "94%-100%" },
            };

        public static Dictionary<int, string> HumidityDescriptions { get; } =
            new Dictionary<int, string>
            {
                { -4, "0%-5%" },
                { -3, "5%-10%" },
                { -2, "10%-15%" },
                { -1, "15%-20%" },
                { 0, "20%-25%" },
                { 1, "25%-30%" },
                { 2, "30%-35%" },
                { 3, "35%-40%" },
                { 4, "40%-45%" },
                { 5, "45%-50%" },
                { 6, "50%-55%" },
                { 7, "55%-60%" },
                { 8, "60%-65%" },
                { 9, "65%-70%" },
                { 10, "70%-75%" },
                { 11, "75%-80%" },
                { 12, "80%-85%" },
                { 13, "85%-90%" },
                { 14, "90%-95%" },
                { 15, "95%-99%" },
                { 16, "100%" },
            };

        public static Dictionary<int, string> LiftedIndexDescriptions { get; } =
            new Dictionary<int, string>
            {
                { -10, "Below -7" },
                { -6, "-7 to -5" },
                { -4, "-5 to -3" },
                { -1, "-3 to 0" },
                { 2, "0 to 4" },
                { 6, "4 to 8" },
                { 10, "8 to 11" },
                { 15, "Over 11" },
            };

        public static Dictionary<int, string> TransparencyDescriptions { get; } =
            new Dictionary<int, string>
            {
                { 1, "<0.3" },
                { 2, "0.3-0.4" },
                { 3, "0.4-0.5" },
                { 4, "0.5-0.6" },
                { 5, "0.6-0.7" },
                { 6, "0.7-0.85" },
                { 7, "0.85-1" },
                { 8, ">1" },
            };

        public static Dictionary<int, string> WindSpeedCategories { get; } =
            new Dictionary<int, string>
            {
                { 1, "Calm" },
                { 2, "Light" },
                { 3, "Moderate" },
                { 4, "Fresh" },
                { 5, "Strong" },
                { 6, "Gale" },
                { 7, "Storm" },
                { 8, "Hurricane" }
            };

        public static Dictionary<int, string> WindSpeedDescriptions { get; } =
            new Dictionary<int, string>
            {
                { 1, "Below 0.3m/s" },
                { 2, "0.3-3.4m/s" },
                { 3, "3.4-8.0m/s" },
                { 4, "8.0-10.8m/s" },
                { 5, "10.8-17.2m/s" },
                { 6, "17.2-24.5m/s" },
                { 7, "24.5-32.6m/s" },
                { 8, "Over 32.6m/s" }
            };

        [JsonProperty("dataseries")]
        public ForecastDataPoint[] DataPoints { get; set; }

        [JsonProperty("init")]
        public DateTime InitialDateTime { get; set; }

        [JsonProperty("product")]
        public string Product { get; set; }
    }

    public class ForecastDataPoint
    {
        [JsonProperty("seeing")]
        public int AstroSeeing { get; set; }

        [JsonProperty("cloudcover")]
        public int CloudCover { get; set; }

        [JsonProperty("rh2m")]
        public int Humidity2M { get; set; }

        [JsonProperty("lifted_index")]
        public int LiftedIndex { get; set; }

        [JsonProperty("prec_type")]
        public string PrecipitationType { get; set; }

        [JsonProperty("temp2m")]
        public int Temperature2M { get; set; }

        [JsonProperty("timepoint")]
        public int TimeOffsetHours { get; set; }

        [JsonProperty("transparency")]
        public int Transparency { get; set; }

        [JsonProperty("wind10m")]
        public ForecastWind10M Wind10M { get; set; }
    }

    public class ForecastWind10M
    {
        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("speed")]
        public int Speed { get; set; }
    }
}
