using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyRouteAPI.Helpers;
using SkyRouteAPI.Interfaces;

namespace SkyRouteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightSearchServices _flightSearchServices;
        private readonly ILogger<FlightsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlightsController"/> class.
        /// </summary>
        /// <param name="flightSearchServices">
        /// Service that aggregates flight results from all registered <see cref="IAirlineServices"/> providers.
        /// </param>
        /// <param name="logger">Logger used to record flight-related information and errors.</param>
        public FlightsController(IFlightSearchServices flightSearchServices, ILogger<FlightsController> logger)
        {
            _flightSearchServices = flightSearchServices;
            _logger = logger;
        }


        /// <summary>
        /// Search aviables flight bases on the FlightSearchRequestDto parameters. This endpoint processes the flight search request and returns a list of available flights matching the search criteria.
        /// </summary>
        /// <param name="request">
        /// The flight search request containing the parameters for the search.
        /// </param>
   
        [HttpPost("search")]
        public async Task<IActionResult> Post([FromBody] DTOs.FlightSearchRequestDto request)
        {
            try
            {
                await Task.Delay(3000); // Simulate async work, replace with actual async calls if needed
                var state = HelperFlight.FlightValidateFields(request);
                if(!state.state)
                {
                    _logger.LogWarning("Flight search validation failed: {Message}", state.message);

                    return BadRequest(state.message);
                }   

                var flights =  await _flightSearchServices.SearchFlights(request);


                _logger.LogInformation(
                       "Flight search processed successfully. Origin: {Origin}, Destination: {Destination}, Date: {DepartureDate}, Passengers: {Passengers}",
                       request.Origin,
                       request.Destination,
                       request.DepartureDate.Date,
                       request.Passengers
                   );



                return Ok(flights);
            }
            catch (Exception  ex)
            {
                _logger.LogError("An error occurred while processing the flight search request."+ex.Message);  
                // Log the exception (not implemented here)
                return StatusCode(500, "An error occurred while processing your request search flight.");
            }
        }
    }
}

