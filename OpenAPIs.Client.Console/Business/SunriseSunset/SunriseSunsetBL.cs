using System;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using OpenAPIs.Client.Console.Models.SunriseSunset;

namespace OpenAPIs.Client.Console.Business.SunriseSunset
{
    /// <inheritdoc cref="ISunriseSunsetBL"/>
    public class SunriseSunsetBL : ISunriseSunsetBL
    {
        private readonly HttpClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SunriseSunsetBL">SunriseSunsetBL</see>
        /// using the specified <paramref name="apiClient">API client</paramref>.
        /// </summary>
        /// <param name="apiClient">The <see cref="HttpClient"> to use for requests.</param>
        public SunriseSunsetBL(HttpClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Gets or sets the base <see cref="Uri">URI</see> used to call the API.
        /// </summary>
        public Uri BaseUri { get; set; } = new Uri("https://api.sunrisesunset.io/json");

        /// <summary>
        /// Gets or sets the <see cref="JsonSerializerSettings">settings</see> used to
        /// deserialize JSON responses.
        /// </summary>
        public JsonSerializerSettings SerializerSettings { get; set; } =
            new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            };

        /// <inheritdoc/>
        public async Task<SunriseSunsetResultModel> QuerySunriseSunsetAsync(
            double latitude,
            double longitude,
            string date = null,
            TimeZoneInfo timeZone = null
        )
        {
            // In .NET Framework, some care must be taken when adding query parameters to a
            // `UriBuilder`. See: https://learn.microsoft.com/en-us/dotnet/api/system.uribuilder.query
            // > To append a value to existing query information in .NET Framework, you must
            // > remove the leading question mark before setting the property with the new
            // > value. This is because .NET Framework always prepends the question mark when
            // > setting the property.
            UriBuilder uriBuilder = new UriBuilder(BaseUri)
            {
                Query = $"lat={latitude}&lng={longitude}"
            };
            if (!(date is null))
            {
                uriBuilder.Query = uriBuilder.Query.Substring(1) + $"&date={date}";
            }
            if (!(timeZone is null))
            {
                uriBuilder.Query = uriBuilder.Query.Substring(1) + $"&timezone={timeZone.Id}";
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
                string responseContent = await response.Content.ReadAsStringAsync();
                SunriseSunsetResultModel result =
                    JsonConvert.DeserializeObject<SunriseSunsetResultModel>(
                        responseContent,
                        SerializerSettings
                    );
                return result;
            }
        }
    }
}
