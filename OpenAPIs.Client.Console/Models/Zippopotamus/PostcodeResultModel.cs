using Newtonsoft.Json;

namespace OpenAPIs.Client.Console.Models.Zippopotamus
{
    /// <summary>
    /// Represents the results of a
    /// <see cref="Business.Zippopotamus.IZippopotamusBL.QueryPostcodeAsync">query by country
    /// and postcode</see>.
    /// </summary>
    public class PostcodeResultModel
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        [JsonProperty("places")]
        public PostcodeResultPlace[] Places { get; set; }

        [JsonProperty("post code")]
        public string Postcode { get; set; }
    }

    /// <summary>
    /// Represents a place in a <see cref="PostcodeResultModel">postcode query result</see>.
    /// </summary>
    public class PostcodeResultPlace
    {
        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("place name")]
        public string Placename { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("state abbreviation")]
        public string StateAbbreviation { get; set; }
    }
}
