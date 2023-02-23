using Newtonsoft.Json;

namespace OpenAPIs.Client.Console.Models.Zippopotamus
{
    /// <summary>
    /// Represents the results of a <see cref="Business.Zippopotamus.IZippopotamusBL.QueryPostcode">query by country
    /// and postcode</see>.
    /// </summary>
    public class PostcodeResultModel
    {
        public string Country { get; set; }

        [JsonProperty("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        [JsonProperty("post code")]
        public string Postcode { get; set; }
        public PostcodeResultPlace[] Places { get; set; }
    }

    /// <summary>
    /// Represents a place in a <see cref="PostcodeResultModel">postcode query result</see>.
    /// </summary>
    public class PostcodeResultPlace
    {
        [JsonProperty("place name")]
        public string Placename { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public string State { get; set; }

        [JsonProperty("state abbreviation")]
        public string StateAbbreviation { get; set; }
    }
}
