using SkyRouteAPI.DTOs;
using SkyRouteAPI.Models;

namespace SkyRouteAPI.Interfaces
{
    /// <summary>
    /// Defines the contract for searching and retrieving flights across all airline providers.
    /// </summary>
    public interface IFlightSearchServices
    {
        /// <summary>
        /// Searches available flights across all registered airline providers.
        /// </summary>
        /// <param name="request">
        /// Search criteria including origin, destination, departure date, number of passengers and cabin class.
        /// </param>
        /// <returns>
        /// Returns a read-only list of available flights that match the search criteria.
        /// </returns>
        Task<IReadOnlyList<FlightResultDto>> SearchFlights(FlightSearchRequestDto request);

        /// <summary>
        /// Retrieves a flight by its flight number across the registered airline providers.
        /// </summary>
        /// <param name="flightNumber">
        /// Flight number used to identify the selected flight.
        /// </param>
        /// <returns>
        /// Returns the matching provider flight if found; otherwise, null.
        /// </returns>
        Task<ProviderFlight?> GetFlightByIdAsync(string flightNumber);
    }
}