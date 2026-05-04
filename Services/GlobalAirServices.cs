using SkyRouteAPI.DTOs;
using SkyRouteAPI.Interfaces;
using SkyRouteAPI.Models;
using System.Reflection.Metadata.Ecma335;

namespace SkyRouteAPI.Services
{
    public class GlobalAirServices : IAirlineServices
    {
        public string ProviderName => "GlobalAir";
        /// <summary>
        /// Static list of supported flights used by the application.
        /// </summary>
        private static readonly List<ProviderFlight> Flights = new()
           {
             new ProviderFlight
             {
                 FlightNumber = "GA101",
                 Origin = "EZE",
                 Destination = "GRU",
                 DepartureDate = new DateTime(2026, 5, 10),
                 DepartureTime = new TimeSpan(8, 30, 0),
                 ArrivalTime = new TimeSpan(11, 10, 0),
                 DurationMinutes = 160,
                 BaseFare = 220m,
                 CabinClass = "Economy"
             },
             new ProviderFlight
             {
                 FlightNumber = "GA102",
                 Origin = "EZE",
                 Destination = "GRU",
                 DepartureDate = new DateTime(2026, 5, 10),
                 DepartureTime = new TimeSpan(15, 45, 0),
                 ArrivalTime = new TimeSpan(18, 25, 0),
                 DurationMinutes = 160,
                 BaseFare = 245m,
                 CabinClass = "Economy"
             },
             new ProviderFlight
             {
                 FlightNumber = "GA110",
                 Origin = "EZE",
                 Destination = "GRU",
                 DepartureDate = new DateTime(2026, 5, 10),
                 DepartureTime = new TimeSpan(12, 0, 0),
                 ArrivalTime = new TimeSpan(14, 40, 0),
                 DurationMinutes = 160,
                 BaseFare = 390m,
                 CabinClass = "Business"
             },
             new ProviderFlight
             {
                 FlightNumber = "GA301",
                 Origin = "EZE",
                 Destination = "SCL",
                 DepartureDate = new DateTime(2026, 5, 11),
                 DepartureTime = new TimeSpan(7, 50, 0),
                 ArrivalTime = new TimeSpan(10, 10, 0),
                 DurationMinutes = 140,
                 BaseFare = 180m,
                 CabinClass = "Economy"
             },
             new ProviderFlight
             {
                 FlightNumber = "GA311",
                 Origin = "EZE",
                 Destination = "SCL",
                 DepartureDate = new DateTime(2026, 5, 11),
                 DepartureTime = new TimeSpan(13, 20, 0),
                 ArrivalTime = new TimeSpan(15, 40, 0),
                 DurationMinutes = 140,
                 BaseFare = 340m,
                 CabinClass = "Business"
             },
             new ProviderFlight
             {
                 FlightNumber = "GA401",
                 Origin = "AEP",
                 Destination = "COR",
                 DepartureDate = new DateTime(2026, 5, 12),
                 DepartureTime = new TimeSpan(10, 0, 0),
                 ArrivalTime = new TimeSpan(11, 25, 0),
                 DurationMinutes = 85,
                 BaseFare = 95m,
                 CabinClass = "Economy"
             },
             new ProviderFlight
             {
                 FlightNumber = "GA411",
                 Origin = "AEP",
                 Destination = "COR",
                 DepartureDate = new DateTime(2026, 5, 12),
                 DepartureTime = new TimeSpan(17, 30, 0),
                 ArrivalTime = new TimeSpan(18, 55, 0),
                 DurationMinutes = 85,
                 BaseFare = 190m,
                 CabinClass = "Business"
             },
             new ProviderFlight
             {
                 FlightNumber = "GA501",
                 Origin = "GRU",
                 Destination = "GIG",
                 DepartureDate = new DateTime(2026, 5, 13),
                 DepartureTime = new TimeSpan(13, 0, 0),
                 ArrivalTime = new TimeSpan(14, 5, 0),
                 DurationMinutes = 65,
                 BaseFare = 80m,
                 CabinClass = "Economy"
             },
             new ProviderFlight
             {
                 FlightNumber = "GA510",
                 Origin = "GRU",
                 Destination = "GIG",
                 DepartureDate = new DateTime(2026, 5, 13),
                 DepartureTime = new TimeSpan(18, 15, 0),
                 ArrivalTime = new TimeSpan(19, 20, 0),
                 DurationMinutes = 65,
                 BaseFare = 160m,
                 CabinClass = "Business"
             }
         };


        public async Task<ProviderFlight?> GetFlightByIdAsyncAirline(string FlightNumber)
        {
            await Task.Delay(100); // Simula una operación asincrónica, como una consulta a una base de datos
            var Flight = Flights.FirstOrDefault(f => f.FlightNumber == FlightNumber);
            if (Flight == null)
            {
                return null;
            }
            return Flight;
        }
        /// <summary>
        /// Searches available GlobalAir flights that match the provided search criteria.
        /// </summary>
        /// <param name="request">
        /// Search criteria including origin, destination, departure date, passengers and cabin class.
        /// </param>
        /// <returns>
        /// Returns a list of matching flights with the GlobalAir pricing rule applied.
        /// </returns>
        /// <remarks>
        /// GlobalAir applies a 15% fuel surcharge to the base fare.
        /// The final price per passenger is rounded to 2 decimal places.
        /// </remarks>
        public async Task< IReadOnlyList<FlightResultDto>> SearchFlightsAirline(FlightSearchRequestDto request)
        {
            await Task.Delay(100); // simula llamada externa
            return Flights
                .Where(f =>
                    f.Origin.Equals(request.Origin, StringComparison.OrdinalIgnoreCase) &&
                    f.Destination.Equals(request.Destination, StringComparison.OrdinalIgnoreCase) &&
                    f.CabinClass.Equals(request.CabinClass, StringComparison.OrdinalIgnoreCase) &&
                      f.DepartureDate.Date == request.DepartureDate.Date)
                .Select(f => 
                {
                    var pricePerPassenger = Math.Round(f.BaseFare * 1.15m, 2);
                    var totalPrice = Math.Round(pricePerPassenger * request.Passengers, 2);
                    return new FlightResultDto
                    {
                        
                        Provider = ProviderName,
                        FlightNumber = f.FlightNumber,
                        Origin = f.Origin,
                        Destination = f.Destination,
                        DepartureDate = request.DepartureDate,
                        DepartureTime = f.DepartureTime.ToString(@"hh\:mm"),
                        ArrivalTime = f.ArrivalTime.ToString(@"hh\:mm"),
                        DurationMinutes = f.DurationMinutes,
                        CabinClass = request.CabinClass,
                        Passengers = request.Passengers,

                     
                        PricePerPassenger = pricePerPassenger,
                        TotalPrice = totalPrice
                    };
                   

                })
                .ToList();
        }
    }
}