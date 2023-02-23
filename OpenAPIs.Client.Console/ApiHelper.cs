using System.Net.Http;
using System.Net.Http.Headers;

namespace OpenAPIs.Client.Console
{
    /// <summary>Provides utilities for use by the API BL classes.</summary>
    internal static class ApiHelper
    {
        /// <summary>
        /// Gets or sets the static <see cref="HttpClient">HTTP client</see> used by the API BL
        /// classes.
        /// </summary>
        public static HttpClient ApiClient { get; set; } = new HttpClient();

        /// <summary>
        /// Initializes the <see cref="ApiClient">HTTP client</see> to accept JSON responses.
        /// </summary>
        public static void InitializeClient()
        {
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }
    }
}
