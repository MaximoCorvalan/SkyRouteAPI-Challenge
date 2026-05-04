using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyRouteAPI.DTOs;
using SkyRouteAPI.Helpers;
using SkyRouteAPI.Interfaces;

namespace SkyRouteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ILogger<BookingsController> _logger;
        private readonly IFlightSearchServices _flightService;
        private readonly IAirportServices _airportService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingsController"/> class.
        /// </summary>
        /// <param name="logger">Logger used to record booking-related information and errors.</param>
        /// <param name="flightService">Service used to search and validate flight information.</param>
        /// <param name="airportServices">Service used to retrieve airport information and validate routes.</param>
        public BookingsController(ILogger<BookingsController> logger, IFlightSearchServices flightService, IAirportServices airportServices)
        {
            _logger = logger;
            _flightService = flightService;
            _airportService = airportServices;
        }




        /// <summary>
        /// Creates a booking for a selected flight.
        /// </summary>
        /// <param name="request">
        /// Booking request containing the selected flight information, passenger information and document number.
        /// </param>
        /// <returns>
        /// Returns a booking confirmation with a generated booking reference code.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> PostBooking([FromBody] BookingRequestDto request)
        {
      
           

            try
            {
              var flight = await _flightService.GetFlightByIdAsync(request.FlightNumber);

                if (flight == null) 
                {
                    return BadRequest("The flight was not found.");
                }
                var AirportOrigin = await _airportService.GetByCode(flight.Origin);
                var AirportDestiny = await _airportService.GetByCode(flight.Destination);
                if(AirportOrigin == null || AirportDestiny == null)
                {
                    return BadRequest("Invalid origin or destination airport code.");
                }

                bool Isinternational = AirportOrigin.Country != AirportDestiny.Country ? true : false;
                   

             
                var respVal = HelperBooking.ValidatePassengerDocument(request.DocumentNumber, Isinternational);

               if (!respVal.state )
                {
                    return BadRequest(respVal.message);
                }

                await Task.Delay(5000);
                _logger.LogInformation("Booking created successfully for {FullName} with email {EmailAddress}.", request.FullName, request.EmailAddress);

                var confirmCode = new BookingResponseDto()
                {
                    BookingReference = flight.FlightNumber+"-"+request.DocumentNumber+"-"+request.Destination+"-"+ Guid.NewGuid().ToString("N")[..4].ToUpper(),
                };
                return Ok(confirmCode);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error occurred while creating booking.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
