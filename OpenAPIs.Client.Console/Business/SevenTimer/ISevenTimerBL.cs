using System.Threading.Tasks;

using OpenAPIs.Client.Console.Models.SevenTimer;

namespace OpenAPIs.Client.Console.Business.SevenTimer
{
    /// <summary>
    /// Represents  business logic for using the 7Timer! API.
    /// </summary>
    public interface ISevenTimerBL
    {
        /// <summary>
        /// Queries the 7Timer! API by <paramref name="latitude">latitude</paramref> and
        /// <paramref name="longitude">longitude</paramref>.
        /// </summary>
        /// <param name="latitude">
        /// The latitude of the location in decimal degrees.
        /// </param>
        /// <param name="longitude">
        /// The longitude of the location in decimal degrees.
        /// </param>
        /// <returns>
        /// The <see cref="Task{AstroForecastResultModel}">task object</see> representing the
        /// asynchronous operation, returning a <see cref="ForecastResultModel">result
        /// object</see> for the specified <paramref name="latitude">latitude</paramref> and
        /// <paramref name="longitude">longitude</paramref>.
        /// </returns>
        /// <exception cref="HttpRequestException">
        /// The <see cref="HttpResponseMessage">HTTP response</see> <see
        /// cref="HttpResponseMessage.StatusCode">status code</see> does not indicate success.
        /// </exception>
        Task<ForecastResultModel> QueryAstroForecastAsync(double latitude, double longitude);
    }
}
