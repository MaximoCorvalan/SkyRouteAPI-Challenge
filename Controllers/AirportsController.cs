using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkyRouteAPI.Interfaces;

namespace SkyRouteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly IAirportServices _airportServices;
        public AirportsController(IAirportServices airportServices) 
        {
            _airportServices = airportServices;
        }


        /// <summary>
        /// Retrieves all available airports.
        /// </summary>
        /// <returns>
        /// Returns a list of airports with their code, name, city and country.
        /// </returns>
 
        [HttpGet]
        public async Task<IActionResult> GetAirports()
        {
            try
            {
                var airports = await _airportServices.GetAll();

                return Ok(airports);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "An unexpected error occurred while retrieving airports.");
            }
        }
            

    }
}
