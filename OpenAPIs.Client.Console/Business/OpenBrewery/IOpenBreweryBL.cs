using System.Collections.Generic;
using System.Threading.Tasks;

using OpenAPIs.Client.Console.Models.OpenBrewery;

namespace OpenAPIs.Client.Console.Business.OpenBrewery
{
    /// <summary>
    /// Represents a business layer for using the Open Brewery DB API.
    /// </summary>
    public interface IOpenBreweryBL
    {
        /// <summary>
        /// Queries the Open Brewery DB API by <paramref name="latitude">latitude</paramref> and
        /// <paramref name="longitude">longitude</paramref>.
        /// </summary>
        /// <param name="latitude">
        /// The latitude of the location in decimal degrees.
        /// </param>
        /// <param name="longitude">
        /// The longitude of the location in decimal degrees.
        /// </param>
        /// <param name="type">
        /// The type of brewery. If not set, all types will be returned.
        /// </param>
        /// <returns>
        /// The <see cref="Task{List{BreweryModel}}">task object</see> representing the
        /// asynchronous operation, returning a <see cref="List{BreweryModel}">list of
        /// <see cref="BreweryModel">brewery objects</see></see> for the specified <paramref
        /// name="latitude">latitude</paramref> and <paramref
        /// name="longitude">longitude</paramref>.
        /// </returns>
        /// <exception cref="HttpRequestException">
        /// The <see cref="HttpResponseMessage">HTTP response</see> <see
        /// cref="HttpResponseMessage.StatusCode">status code</see> does not indicate success.
        /// </exception>
        Task<List<BreweryModel>> QueryBreweriesByLocationAsync(
            double latitude,
            double longitude,
            BreweryType? type = null
        );
    }
}
