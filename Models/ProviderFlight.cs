namespace SkyRouteAPI.Models
{
    /// <summary>
    /// Represents a flight provided by an airline provider before applying pricing rules the differences, this is an internal class
    /// whit FlightResultDto is FlightResultDto returns the final pricing after applying all rules,passangers, providers and other information
    /// </summary>
    public class ProviderFlight
    {
        /// <summary>
        /// Unique flight number assigned by the airline provider. Example: GA101.
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
        /// Flight departure time.
        /// </summary>
        public TimeSpan DepartureTime { get; set; }

        /// <summary>
        /// Flight arrival time.
        /// </summary>
        public TimeSpan ArrivalTime { get; set; }

        /// <summary>
        /// Flight departure date.
        /// </summary>
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Flight duration expressed in minutes.
        /// </summary>
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Base price per passenger before applying provider pricing rules.
        /// </summary>
        public decimal BaseFare { get; set; }

        /// <summary>
        /// Cabin class assigned to the flight. Example: Economy, Business or First Class.
        /// </summary>
        public string CabinClass { get; set; } = "Economy";


    }
}