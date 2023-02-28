using System;
using System.Threading.Tasks;

using OpenAPIs.Client.Console.Models.SunriseSunset;

namespace OpenAPIs.Client.Console.Business.SunriseSunset
{
    /// <summary>
    /// Represents business logic for using the SunriseSunset API.
    /// </summary>
    public interface ISunriseSunsetBL
    {
        /// <summary>
        /// Queries the SunriseSunset API by <paramref name="latitude">latitude</paramref> and
        /// <paramref name="longitude">longitude</paramref>.
        /// </summary>
        /// <param name="latitude">
        /// The latitude of the location in decimal degrees.
        /// </param>
        /// <param name="longitude">
        /// The longitude of the location in decimal degrees.
        /// </param>
        /// <param name="date">
        /// Date in YYYY-MM-DD format. You can also specify relative formats such as "today" and
        /// "tomorrow". If not set it’ll default to today.
        /// </param>
        /// <param name="timeZone">
        /// Set a timezone of the returned times. By default the API will return the times in
        /// the location’s timezone.
        /// </param>
        /// <returns>
        /// The <see cref="Task{SunriseSunsetResultModel}">task object</see> representing the
        /// asynchronous operation, returning a <see cref="SunriseSunsetResultModel">result
        /// object</see> for the specified <paramref name="latitude">latitude</paramref> and
        /// <paramref name="longitude">longitude</paramref>.
        /// </returns>
        /// <exception cref="HttpRequestException">
        /// The <see cref="HttpResponseMessage">HTTP response</see> <see
        /// cref="HttpResponseMessage.StatusCode">status code</see> does not indicate success.
        /// </exception>
        Task<SunriseSunsetResultModel> QuerySunriseSunsetAsync(
            double latitude,
            double longitude,
            string date = null,
            TimeZoneInfo timeZone = null
        );
    }
}
