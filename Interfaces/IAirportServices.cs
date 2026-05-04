using SkyRouteAPI.DTOS;

namespace SkyRouteAPI.Interfaces
{
    /// <summary>
    /// Defines the contract for retrieving airport information.
    /// </summary>
    public interface IAirportServices
    {
        /// <summary>
        /// Retrieves all available airports.
        /// </summary>
        /// <returns>
        /// Returns a read-only list of airports with their code, name, city and country.
        /// </returns>
        Task<IReadOnlyList<AirportResponse>> GetAll();

        /// <summary>
        /// Retrieves an airport by its airport code.
        /// </summary>
        /// <param name="code">
        /// Airport code used to identify the airport. Example: EZE.
        /// </param>
        /// <returns>
        /// Returns the matching airport if found; otherwise, null.
        /// </returns>
        Task<AirportResponse?> GetByCode(string code);
    }
}