using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using OpenAPIs.Client.Console.Models.OpenBrewery;

namespace OpenAPIs.Client.Console.Business.OpenBrewery
{
    /// <inheritdoc cref="IOpenBreweryBL"/>
    public class OpenBreweryBL : IOpenBreweryBL
    {
        private readonly HttpClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenBreweryBL">OpenBreweryBL</see>
        /// using the specified <paramref name="apiClient">API client</paramref>.
        /// </summary>
        /// <param name="apiClient">The <see cref="HttpClient"> to use for requests.</param>
        public OpenBreweryBL(HttpClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Gets or sets the base <see cref="Uri">URI</see> used to call the API.
        /// </summary>
        public Uri BaseUri { get; set; } = new Uri("https://api.openbrewerydb.org/breweries/");

        /// <summary>
        /// Gets or sets the <see cref="JsonSerializerSettings">settings</see> used to
        /// deserialize JSON responses.
        /// </summary>
        public JsonSerializerSettings SerializerSettings { get; set; } =
            new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };

        /// <inheritdoc/>
        public async Task<List<BreweryModel>> QueryBreweriesByLocationAsync(
            double latitude,
            double longitude,
            BreweryType? type = null
        )
        {
            UriBuilder uriBuilder = new UriBuilder(BaseUri)
            {
                Query = $"by_dist={latitude},{longitude}&per_page=3"
            };
            if (!(type is null))
            {
                uriBuilder.Query = uriBuilder.Query.Substring(1) + $"&type={type.Value}";
            }
            using (HttpResponseMessage response = await _apiClient.GetAsync(uriBuilder.Uri))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        "Response status code does not indicate success: "
                            + $"{(int)response.StatusCode} ({response.ReasonPhrase})."
                    );
                }
                string responseString = await response.Content.ReadAsStringAsync();
                List<BreweryModel> breweries = JsonConvert.DeserializeObject<
                    List<BreweryModel>
                >(responseString, SerializerSettings);
                return breweries;
            }
        }
    }
}
