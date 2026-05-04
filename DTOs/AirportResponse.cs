namespace SkyRouteAPI.DTOS
{
    /// <summary>
    /// Represents an airport returned by the API.
    /// </summary>
    public class AirportResponse
    {
        /// <summary>
        /// Airport code. Example: EZE.
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Airport name. Example: Ministro Pistarini International Airport.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// City where the airport is located. Example: Buenos Aires.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Country where the airport is located. Example: Argentina.
        /// </summary>
        public string Country { get; set; } = string.Empty;
    }
}