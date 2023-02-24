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
        public string Country { get; set; }

        [JsonProperty("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        [JsonProperty("place name")]
        public string Placename { get; set; }
        public PlacenameResultPlace[] Places { get; set; }

        public string State { get; set; }

        [JsonProperty("state abbreviation")]
        public string StateAbbreviation { get; set; }
    }

    /// <summary>
    /// Represents a place in a <see cref="PlacenameResultModel">placename query result</see>.
    /// </summary>
    public class PlacenameResultPlace
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [JsonProperty("place name")]
        public string Placename { get; set; }

        [JsonProperty("post code")]
        public string Postcode { get; set; }
    }
}
