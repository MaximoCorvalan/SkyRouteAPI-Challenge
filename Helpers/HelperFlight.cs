using SkyRouteAPI.DTOs;

namespace SkyRouteAPI.Helpers
{
    public static class HelperFlight
    {
        /// <summary>
        /// Validate the flight search request fields to ensure they meet the required criteria for a valid search.
        /// </summary>
    
        /// Returns a validation result containing the validation state and a message.
        /// </returns>
        public static (bool state, string message) FlightValidateFields(FlightSearchRequestDto request) 
        {
           
          
            if (request.DepartureDate.Date < DateTime.Today)
                return (false, "Departure date cannot be in the past.");

  

            var allowedCabinClasses = new[] { "Economy", "Business", "First Class" };

            if (!allowedCabinClasses.Contains(request.CabinClass, StringComparer.OrdinalIgnoreCase))
                return (false, "Cabin class must be Economy, Business, or First Class.");


            return (true, string.Empty);

        }

        
    }
}
