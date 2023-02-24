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
        public string Country { get; set; }

        [JsonProperty("country abbreviation")]
        public string CountryAbbreviation { get; set; }
        public PostcodeResultPlace[] Places { get; set; }

        [JsonProperty("post code")]
        public string Postcode { get; set; }
    }

    /// <summary>
    /// Represents a place in a <see cref="PostcodeResultModel">postcode query result</see>.
    /// </summary>
    public class PostcodeResultPlace
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [JsonProperty("place name")]
        public string Placename { get; set; }

        public string State { get; set; }

        [JsonProperty("state abbreviation")]
        public string StateAbbreviation { get; set; }
    }
}
