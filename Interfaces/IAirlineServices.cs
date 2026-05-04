using SkyRouteAPI.DTOs;
using SkyRouteAPI.Models;

namespace SkyRouteAPI.Interfaces
{
    /// <summary>
    /// Defines the contract that every airline provider service must implement.
    /// </summary>
    public interface IAirlineServices
    {
        /// <summary>
        /// Name of the airline provider. Example: GlobalAir or BudgetWings.
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// Searches available flights for this airline provider using the provided search criteria.
        /// </summary>
        /// <param name="request">
        /// Search criteria including origin, destination, departure date, passengers and cabin class.
        /// </param>
        /// <returns>
        /// Returns a read-only list of matching flight results for this provider.
        /// </returns>
        Task<IReadOnlyList<FlightResultDto>> SearchFlightsAirline(  FlightSearchRequestDto request );

        /// <summary>
        /// Retrieves a provider flight by its flight number.
        /// </summary>
        /// <param name="flightNumber">
        /// Flight number used to identify the flight within the provider.
        /// </param>
        /// <returns>
        /// Returns the matching provider flight if found; otherwise, null.
        /// </returns>
        Task<ProviderFlight?> GetFlightByIdAsyncAirline(string flightNumber);
    }
}