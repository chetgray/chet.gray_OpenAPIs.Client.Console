using System;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using OpenAPIs.Client.Console.Models.Zippopotamus;

namespace OpenAPIs.Client.Console.Business.Zippopotamus
{
    /// <inheritdoc cref="IZippopotamusBL"/>
    public class ZippopotamusBL : IZippopotamusBL
    {
        private readonly HttpClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZippopotamusBL">ZippopotamusBL</see>
        /// using the specified <paramref name="apiClient">API client</paramref>.
        /// </summary>
        /// <param name="apiClient">The <see cref="HttpClient"> to use for requests.</param>
        public ZippopotamusBL(HttpClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Gets or sets the base <see cref="Uri">URI</see> used to call the API.
        /// </summary>
        public Uri BaseUri { get; set; } = new Uri("https://www.zippopotam.us/");

        /// <summary>
        /// Gets or sets the <see cref="JsonSerializerSettings">settings</see> used to
        /// deserialize JSON responses.
        /// </summary>
        public JsonSerializerSettings SerializerSettings { get; set; } =
            new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };

        public async Task<PlacenameResultModel> QueryPlacenameAsync(
            string countryAbbreviation,
            string stateAbbreviation,
            string placename
        )
        {
            Uri queryUri = new Uri(
                BaseUri,
                $"{countryAbbreviation}/{stateAbbreviation}/{placename}"
            );
            using (HttpResponseMessage response = await _apiClient.GetAsync(queryUri))
            {
                response.EnsureSuccessStatusCode();
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Error {response.StatusCode}: {response.ReasonPhrase}"
                    );
                }
                string responseJson = await response.Content.ReadAsStringAsync();
                PlacenameResultModel result =
                    JsonConvert.DeserializeObject<PlacenameResultModel>(
                        responseJson,
                        SerializerSettings
                    );
                return result;
            }
        }

        public async Task<PostcodeResultModel> QueryPostcodeAsync(
            string countryAbbreviation,
            string postcode
        )
        {
            Uri queryUri = new Uri(BaseUri, $"{countryAbbreviation}/{postcode}");
            using (HttpResponseMessage response = await _apiClient.GetAsync(queryUri))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Error {response.StatusCode}: {response.ReasonPhrase}"
                    );
                }
                string responseJson = await response.Content.ReadAsStringAsync();
                PostcodeResultModel result = JsonConvert.DeserializeObject<PostcodeResultModel>(
                    responseJson,
                    SerializerSettings
                );
                return result;
            }
        }
    }
}
