namespace SkyRouteAPI.DTOs
{
    /// <summary>
    /// Represents a flight search result returned by an airline provider.
    /// </summary>
    public class FlightResultDto
    {
        /// <summary>
        /// Name of the airline provider. Example: GlobalAir.
        /// </summary>
        public string Provider { get; set; } = string.Empty;

        /// <summary>
        /// Unique flight number assigned by the provider. Example: GA101.
        /// </summary>
        public string FlightNumber { get; set; } = string.Empty;

        /// <summary>
        /// Origin airport code. Example: EZE.
        /// </summary>
        public string Origin { get; set; } = string.Empty;

        /// <summary>
        /// Destination airport code. Example: JFK.
        /// </summary>
        public string Destination { get; set; } = string.Empty;

        /// <summary>
        /// Date of departure.
        /// </summary>
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Departure time in HH:mm format.
        /// </summary>
        public string DepartureTime { get; set; } = string.Empty;

        /// <summary>
        /// Arrival time in HH:mm format.
        /// </summary>
        public string ArrivalTime { get; set; } = string.Empty;

        /// <summary>
        /// Number of passengers included in the search.
        /// </summary>
        public int Passengers { get; set; }

        /// <summary>
        /// Flight duration expressed in minutes.
        /// </summary>
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Selected cabin class. Example: Economy, Business or First Class.
        /// </summary>
        public string CabinClass { get; set; } = string.Empty;

        /// <summary>
        /// Final price per passenger after applying the provider pricing rules.
        /// </summary>
        public decimal PricePerPassenger { get; set; }

        /// <summary>
        /// Total price for all passengers combined.
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}