using Microsoft.AspNetCore.Mvc;
using SkyRouteAPI.DTOs;
using SkyRouteAPI.Interfaces;
using SkyRouteAPI.Models;

namespace SkyRouteAPI.Services
{
    public class BudgetWingsServices : IAirlineServices
    {
        public string ProviderName => "BudgetWings";
        /// <summary>
        /// Static list of supported flights used by the application.
        /// </summary>
        private static readonly List<ProviderFlight> Flights = new()
         {
                    new ProviderFlight
                    {
                        FlightNumber = "BW701",
                        Origin = "EZE",
                        Destination = "GRU",
                        DepartureDate = new DateTime(2026, 5, 10),
                        DepartureTime = new TimeSpan(5, 40, 0),
                        ArrivalTime = new TimeSpan(8, 30, 0),
                        DurationMinutes = 170,
                        BaseFare = 160m,
                        CabinClass = "Economy"
                    },
                    new ProviderFlight
                    {
                        FlightNumber = "BW702",
                        Origin = "EZE",
                        Destination = "GRU",
                        DepartureDate = new DateTime(2026, 5, 10),
                        DepartureTime = new TimeSpan(22, 15, 0),
                        ArrivalTime = new TimeSpan(1, 5, 0),
                        DurationMinutes = 170,
                        BaseFare = 145m,
                        CabinClass = "Economy"
                    },
                    new ProviderFlight
                    {
                        FlightNumber = "BW801",
                        Origin = "EZE",
                        Destination = "SCL",
                        DepartureDate = new DateTime(2026, 5, 11),
                        DepartureTime = new TimeSpan(6, 10, 0),
                        ArrivalTime = new TimeSpan(8, 30, 0),
                        DurationMinutes = 140,
                        BaseFare = 120m,
                        CabinClass = "Economy"
                    },
                    new ProviderFlight
                    {
                        FlightNumber = "BW901",
                        Origin = "AEP",
                        Destination = "COR",
                        DepartureDate = new DateTime(2026, 5, 12),
                        DepartureTime = new TimeSpan(7, 0, 0),
                        ArrivalTime = new TimeSpan(8, 30, 0),
                        DurationMinutes = 90,
                        BaseFare = 45m,
                        CabinClass = "Economy"
                    },
                    new ProviderFlight
                    {
                        FlightNumber = "BW301",
                        Origin = "GRU",
                        Destination = "GIG",
                        DepartureDate = new DateTime(2026, 5, 13),
                        DepartureTime = new TimeSpan(6, 0, 0),
                        ArrivalTime = new TimeSpan(7, 10, 0),
                        DurationMinutes = 70,
                        BaseFare = 25m,
                        CabinClass = "Economy"
                    },
                   
                    // Ejemplos con Business para probar filtro por cabina
                    new ProviderFlight
                    {
                        FlightNumber = "BW710",
                        Origin = "EZE",
                        Destination = "GRU",
                        DepartureDate = new DateTime(2026, 5, 10),
                        DepartureTime = new TimeSpan(12, 30, 0),
                        ArrivalTime = new TimeSpan(15, 20, 0),
                        DurationMinutes = 170,
                        BaseFare = 260m,
                        CabinClass = "Business"
                    },
                    new ProviderFlight
                    {
                        FlightNumber = "BW811",
                        Origin = "EZE",
                        Destination = "SCL",
                        DepartureDate = new DateTime(2026, 5, 11),
                        DepartureTime = new TimeSpan(14, 0, 0),
                        ArrivalTime = new TimeSpan(16, 20, 0),
                        DurationMinutes = 140,
                        BaseFare = 230m,
                        CabinClass = "Business"
                    }
         };

        /// <summary>
        /// Searches available BudgetWings flights that match the provided search criteria.
        /// </summary>
        /// <param name="request">
        /// Search criteria including origin, destination, departure date, passengers and cabin class.
        /// </param>
        /// <returns>
        /// Returns a list of matching flights with the BudgetWings pricing rule applied.
        /// </returns>
        /// <remarks>
        /// BudgetWings applies a 10% promotional discount to the base fare.
        /// The final price per passenger cannot be lower than USD 29.99.
        /// </remarks>
        public async Task< IReadOnlyList<FlightResultDto>> SearchFlightsAirline(FlightSearchRequestDto request)
        {
        
            return  Flights
                .Where(f => ///filtro por origen, destino, clase y fecha de salida
                    f.Origin.Equals(request.Origin, StringComparison.OrdinalIgnoreCase) &&
                    f.Destination.Equals(request.Destination, StringComparison.OrdinalIgnoreCase) &&
                    f.CabinClass.Equals(request.CabinClass, StringComparison.OrdinalIgnoreCase) &&
                    f.DepartureDate.Date==request.DepartureDate.Date)
               .Select(f =>
               {  ///transformo el resultado de la consulta a mi DTO de salida, aplicando la regla de negocio del descuento y precio mínimo
                   //aplico el descuento que me piden y  valido el precio mínimo
                   var discountedPrice = f.BaseFare * 0.90m;

                   var pricePerPassenger = discountedPrice < 29.99m
                       ? 29.99m
                       : Math.Round(discountedPrice, 2); // redondeo a 2 decimales

                   var totalPrice = Math.Round(pricePerPassenger * request.Passengers, 2);

                   return new FlightResultDto
                   {
                      
                       Provider = ProviderName,
                       FlightNumber = f.FlightNumber,
                       Origin = f.Origin,
                       Destination = f.Destination,
                       DepartureDate = f.DepartureDate,
                       DepartureTime = f.DepartureTime.ToString(@"hh\:mm"),
                       ArrivalTime = f.ArrivalTime.ToString(@"hh\:mm"),
                       DurationMinutes = f.DurationMinutes,
                       CabinClass = f.CabinClass,
                       PricePerPassenger = pricePerPassenger,
                       Passengers = request.Passengers,
                       TotalPrice = totalPrice
                   };
               })
        .ToList();
        }

        public async Task<ProviderFlight?> GetFlightByIdAsyncAirline(string FlightNumber)
        {
            await Task.Delay(100); // Simula una operación asincrónica, como una consulta a una base de datos
            var Flight =   Flights.FirstOrDefault(f => f.FlightNumber == FlightNumber);
       
            return Flight;
        }
    }
}
