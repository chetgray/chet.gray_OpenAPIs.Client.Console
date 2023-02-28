using System;

using Newtonsoft.Json;

namespace OpenAPIs.Client.Console.Models.SunriseSunset
{
    /// <summary>
    /// Represents the result of <see cref="SunriseSunsetClient.GetSunriseSunsetAsync">query by
    /// latitude and longitude</see>.
    /// </summary>
    public class SunriseSunsetResultModel
    {
        [JsonProperty("results")]
        public SunriseSunsetResults Results { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Represents a set of data in a <see cref="SunriseSunsetResultModel">sunrise/sunset query
    /// result</see>.
    /// </summary>
    public class SunriseSunsetResults
    {
        [JsonProperty("dawn")]
        public DateTime Dawn { get; set; }

        [JsonProperty("day_length")]
        public TimeSpan DayLength { get; set; }

        [JsonProperty("dusk")]
        public DateTime Dusk { get; set; }

        [JsonProperty("first_light")]
        public DateTime FirstLight { get; set; }

        [JsonProperty("golden_hour")]
        public DateTime GoldenHour { get; set; }

        [JsonProperty("last_light")]
        public DateTime LastLight { get; set; }

        [JsonProperty("solar_noon")]
        public DateTime SolarNoon { get; set; }

        [JsonProperty("sunrise")]
        public DateTime Sunrise { get; set; }

        [JsonProperty("sunset")]
        public DateTime Sunset { get; set; }

        [JsonProperty("timezone")]
        public string TimeZone { get; set; }
    }
}
