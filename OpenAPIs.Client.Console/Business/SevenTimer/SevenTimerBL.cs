using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using OpenAPIs.Client.Console.Models.SevenTimer;

namespace OpenAPIs.Client.Console.Business.SevenTimer
{
    /// <inheritdoc cref="ISevenTimerBL"/>
    public class SevenTimerBL : ISevenTimerBL
    {
        private readonly HttpClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SevenTimerBL">SevenTimerBL</see> using
        /// the specified <paramref name="apiClient">API client</paramref>.
        /// </summary>
        /// <param name="apiClient">The <see cref="HttpClient"> to use for requests.</param>
        public SevenTimerBL(HttpClient apiClient)
        {
            _apiClient = apiClient;
            SerializerSettings.Converters.Add(
                new IsoDateTimeConverter
                {
                    DateTimeFormat = "yyyyMMddHH",
                    // Initialization datetime is in UTC
                    DateTimeStyles = DateTimeStyles.AssumeUniversal,
                }
            );
        }

        /// <summary>
        /// Gets or sets the base <see cref="Uri">URI</see> used to call the API.
        /// </summary>
        public Uri BaseUri { get; set; } = new Uri("https://www.7timer.info/bin/api.pl");

        /// <summary>
        /// Gets or sets the <see cref="JsonSerializerSettings">settings</see> used to
        /// deserialize JSON responses.
        /// </summary>
        public JsonSerializerSettings SerializerSettings { get; set; } =
            new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };

        public async Task<ForecastResultModel> QueryAstroForecastAsync(
            double latitude,
            double longitude
        )
        {
            Uri uri = new Uri(
                BaseUri,
                $"?product=astro&output=json&lat={latitude}&lon={longitude}"
            );
            using (HttpResponseMessage response = await _apiClient.GetAsync(uri))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        "Response status code does not indicate success: "
                            + $"{(int)response.StatusCode} ({response.ReasonPhrase})."
                    );
                }
                string content = await response.Content.ReadAsStringAsync();
                ForecastResultModel result = JsonConvert.DeserializeObject<ForecastResultModel>(
                    content,
                    SerializerSettings
                );

                return result;
            }
        }
    }
}
