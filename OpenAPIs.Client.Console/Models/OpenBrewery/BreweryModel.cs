using System;
using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OpenAPIs.Client.Console.Models.OpenBrewery
{
    /// <summary>
    /// Represents a brewery type in the Open Brewery DB API.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BreweryType
    {
        /// <summary>
        /// Most craft breweries. For example, Samuel Adams is still considered a micro brewery.
        /// </summary>
        [EnumMember(Value = "micro")]
        Micro = 1,

        /// <summary>
        /// An extremely small brewery which typically only distributes locally.
        /// </summary>
        [EnumMember(Value = "nano")]
        Nano,

        /// <summary>
        /// A regional location of an expanded brewery. Ex. Sierra Nevada’s Asheville, NC
        /// location.
        /// </summary>
        [EnumMember(Value = "regional")]
        Regional,

        /// <summary>
        /// A beer-focused restaurant or restaurant/bar with a brewery on-premise.
        /// </summary>
        [EnumMember(Value = "brewpub")]
        Brewpub,

        /// <summary>
        /// A very large brewery. Likely not for visitors. Ex. Miller-Coors. (deprecated)
        /// </summary>
        [EnumMember(Value = "large")]
        Large,

        /// <summary>
        /// A brewery in planning or not yet opened to the public.
        /// </summary>
        [EnumMember(Value = "planning")]
        Planning,

        /// <summary>
        /// A bar. No brewery equipment on premise. (deprecated)
        /// </summary>
        [EnumMember(Value = "bar")]
        Bar,

        /// <summary>
        /// A brewery that uses another brewery’s equipment.
        /// </summary>
        [EnumMember(Value = "contract")]
        Contract,

        /// <summary>
        /// Similar to contract brewing but refers more to a brewery incubator.
        /// </summary>
        [EnumMember(Value = "proprietor")]
        Proprietor,

        /// <summary>
        /// A location which has been closed.
        /// </summary>
        [EnumMember(Value = "closed")]
        Closed
    }

    /// <summary>
    /// Represents a brewery in the Open Brewery DB API.
    /// </summary>
    public class BreweryModel
    {
        [JsonProperty("address_2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("address_3")]
        public string AddressLine3 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("county_province")]
        public string CountyProvince { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreationTime { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("postal_code")]
        public string Postcode { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("brewery_type")]
        public BreweryType? Type { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdateTime { get; set; }

        [JsonProperty("website_url")]
        public Uri WebsiteUri { get; set; }
    }
}
