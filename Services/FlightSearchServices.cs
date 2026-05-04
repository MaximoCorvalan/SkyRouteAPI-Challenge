using SkyRouteAPI.DTOs;
using SkyRouteAPI.Interfaces;
using SkyRouteAPI.Models;

namespace SkyRouteAPI.Services
{
    /// <summary>
    /// Provides flight search operations by aggregating results from all registered airline providers.
    /// </summary>
    public class FlightSearchServices : IFlightSearchServices
    {
        private readonly IEnumerable<IAirlineServices> _airlineServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlightSearchServices"/> class.
        /// </summary>
        /// <param name="airlineServices">
        /// Collection of registered airline provider services used to search and retrieve flights.
        /// </param>
        public FlightSearchServices(IEnumerable<IAirlineServices> airlineServices)
        {
            _airlineServices = airlineServices;
        }

        /// <summary>
        /// Retrieves a flight by its flight number from all registered airline providers.
        /// </summary>
        /// <param name="flightId">
        /// Flight number used to identify the selected flight.
        /// </param>
        /// <returns>
        /// Returns the matching provider flight if found; otherwise, null.
        /// </returns>
        public async Task<ProviderFlight?> GetFlightByIdAsync(string flightId)
        {
            var tasks = _airlineServices
                .Select(service => service.GetFlightByIdAsyncAirline(flightId)); //“Por cada provider registrado, ejecutá GetFlightByIdAsyncAirline(flightId) y devolveme una lista de tareas.”

            var results = await Task.WhenAll(tasks);

            return results.FirstOrDefault(flight => flight != null);
        }

        /// <summary>
        /// Searches available flights across all registered airline providers.
        /// </summary>
        /// <param name="request">
        /// Search criteria including origin, destination, departure date, number of passengers and cabin class.
        /// </param>
        /// <returns>
        /// Returns a read-only list containing all matching flights from the registered providers.
        /// </returns>
        public async Task<IReadOnlyList<FlightResultDto>> SearchFlights(
            FlightSearchRequestDto request)
        {
            var tasks = _airlineServices
                .Select(service => service.SearchFlightsAirline(request)); // for each registered provider, execute SearchFlightsAirline(request) and return a list of tasks.

            var results = await Task.WhenAll(tasks);

            return results
                .SelectMany(flights => flights)
                .ToList(); //convert in one list of FlightResultDto
        }
    }
}