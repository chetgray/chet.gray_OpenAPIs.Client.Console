using System.Threading.Tasks;

using OpenAPIs.Client.Console.Models.Zippopotamus;

namespace OpenAPIs.Client.Console.Business.Zippopotamus
{
    /// <summary>
    /// Represents business logic for using the Zippopotamus API.
    /// </summary>
    public interface IZippopotamusBL
    {
        /// <summary>
        /// Queries the Zippopotamus API by <paramref
        /// name="countryAbbreviation">country</paramref>, <paramref
        /// name="stateAbbreviation">state</paramref> and <paramref
        /// name="postcode">postcode</paramref>.
        /// </summary>
        /// <returns>
        /// A <see cref="PlacenameResultModel">result object</see> with the matching <see
        /// cref="PlacenameResultPlace">place</see>s.
        /// </returns>
        Task<PlacenameResultModel> QueryPlacenameAsync(
            string countryAbbreviation,
            string stateAbbreviation,
            string placename
        );

        /// <summary>
        /// Queries the Zippopotamus API by <paramref
        /// name="countryAbbreviation">country</paramref> and <paramref
        /// name="postcode">postcode</paramref>.
        /// </summary>
        /// <param name="countryAbbreviation"></param>
        /// <param name="postcode"></param>
        /// <returns>
        /// A <see cref="PostcodeResultModel">result object</see> with the matching <see
        /// cref="PostcodeResultPlace">place</see>s.
        /// </returns>
        Task<PostcodeResultModel> QueryPostcodeAsync(
            string countryAbbreviation,
            string postcode
        );
    }
}
