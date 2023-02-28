using Newtonsoft.Json;

namespace OpenAPIs.Client.Console.Models.Zippopotamus
{
    /// <summary>
    /// Represents the results of a
    /// <see cref="Business.Zippopotamus.IZippopotamusBL.QueryPlacenameAsync">query by country,
    /// state, and place name</see>.
    /// </summary>
    public class PlacenameResultModel
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        [JsonProperty("place name")]
        public string Placename { get; set; }

        [JsonProperty("places")]
        public PlacenameResultPlace[] Places { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("state abbreviation")]
        public string StateAbbreviation { get; set; }
    }

    /// <summary>
    /// Represents a place in a <see cref="PlacenameResultModel">placename query result</see>.
    /// </summary>
    public class PlacenameResultPlace
    {
        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("place name")]
        public string Placename { get; set; }

        [JsonProperty("post code")]
        public string Postcode { get; set; }
    }
}
