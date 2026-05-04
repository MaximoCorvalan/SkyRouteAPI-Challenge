using SkyRouteAPI.DTOS;
using SkyRouteAPI.Interfaces;

namespace SkyRouteAPI.Services
{
    public class AirportServices : IAirportServices
    {
        /// <summary>
        /// Static list of supported airports used by the application.
        /// </summary>
        private static readonly List<AirportResponse> Airports = new()
        {
            new AirportResponse
            {
                Code = "EZE",
                Name = "Ministro Pistarini International Airport",
                City = "Buenos Aires",
                Country = "Argentina"
            },
            new AirportResponse
            {
                Code = "AEP",
                Name = "Jorge Newbery Airport",
                City = "Buenos Aires",
                Country = "Argentina"
            },
            new AirportResponse
            {
                Code = "COR",
                Name = "Ingeniero Ambrosio Taravella Airport",
                City = "Córdoba",
                Country = "Argentina"
            },
            new AirportResponse
            {
                Code = "MDZ",
                Name = "Gobernador Francisco Gabrielli International Airport",
                City = "Mendoza",
                Country = "Argentina"
            },
            new AirportResponse
            {
                Code = "GRU",
                Name = "São Paulo/Guarulhos International Airport",
                City = "São Paulo",
                Country = "Brazil"
            },
            new AirportResponse
            {
                Code = "GIG",
                Name = "Rio de Janeiro/Galeão International Airport",
                City = "Rio de Janeiro",
                Country = "Brazil"
            },
            new AirportResponse
            {
                Code = "SCL",
                Name = "Arturo Merino Benítez International Airport",
                City = "Santiago",
                Country = "Chile"
            },
            new AirportResponse
            {
                Code = "MVD",
                Name = "Carrasco International Airport",
                City = "Montevideo",
                Country = "Uruguay"
            }
        };
        /// <summary>
        /// Gets all supported airports.
        /// </summary>
        /// <returns>
        /// Returns a read-only list of airports with their code, name, city and country.
        /// </returns>
        public async Task< IReadOnlyList<AirportResponse>> GetAll()
        {
            await Task.Delay(100); // Simulate async operation
            return Airports;
        }
        /// <summary>
        /// Gets an airport by its code.
        /// </summary>
        /// <param name="code">The airport code.</param>
        /// <returns>The matching airport if found; otherwise, null.</returns>
        public async Task< AirportResponse?> GetByCode(string code)
        {
            await Task.Delay(100); // Simulate async operation
            return  Airports.FirstOrDefault(a => a.Code == code);
        }
        }
    }

