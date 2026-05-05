# SkyRoute API

SkyRoute API is the backend service for the SkyRoute Travel Platform challenge. It exposes endpoints to retrieve airports, search available flights across mocked airline providers, and confirm a booking for a selected flight.

The application was designed as a small but extensible flight aggregation backend. It currently integrates two mocked airline providers: **GlobalAir** and **BudgetWings**.

## Tech Stack

- .NET 10
- ASP.NET Core Web API
- Swagger / Swashbuckle
- Dependency Injection
- In-memory mocked data

## Features

- Retrieve available airports.
- Search flights by origin, destination, departure date, passengers, and cabin class.
- Aggregate results from multiple airline providers.
- Apply provider-specific pricing rules.
- Return both per-passenger price and total price.
- Validate booking passenger data.
- Validate document type depending on route type:
  - Domestic route: National ID
  - International route: Passport Number
- Generate a booking reference code.
- Swagger UI enabled in development.

## Project Structure

```text
SkyRouteAPI/
├── Controllers/
│   ├── AirportsController.cs
│   ├── FlightsController.cs
│   └── BookingsController.cs
├── DTOs/
│   ├── AirportResponse.cs
│   ├── FlightSearchRequestDto.cs
│   ├── FlightResultDto.cs
│   ├── BookingRequestDto.cs
│   └── BookingResponseDto.cs
├── Helpers/
│   ├── HelperFlight.cs
│   └── HelperBooking.cs
├── Interfaces/
│   ├── IAirportServices.cs
│   ├── IAirlineServices.cs
│   └── IFlightSearchServices.cs
├── Models/
│   └── ProviderFlight.cs
├── Services/
│   ├── AirportServices.cs
│   ├── GlobalAirServices.cs
│   ├── BudgetWingsServices.cs
│   └── FlightSearchServices.cs
├── Program.cs
└── SkyRouteAPI.csproj
```

## Setup and Run Instructions

### Prerequisites

Install the following tools before running the project:

- .NET 10 SDK
- Visual Studio 2022, Visual Studio Code, or another compatible IDE

### 1. Clone the repository

```bash
git clone <repository-url>
cd SkyRouteAPI
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Run the API

Using HTTPS profile:

```bash
dotnet run
```


### 4. Open Swagger

After running the API, open one of the following URLs:

```text
https://localhost:7074/swagger
http://localhost:5206/swagger
```

Swagger can be used to test all available endpoints.

## API Endpoints

### Airports

```http
GET /api/Airports
```

Returns the hardcoded list of supported airports.

### Flight Search

```http
POST /api/Flights/search
```

Example request:

```json
{
  "origin": "EZE",
  "destination": "GRU",
  "departureDate": "2026-05-10",
  "passengers": 2,
  "cabinClass": "Economy"
}
```

Example response:

```json
[
  {
    "provider": "GlobalAir",
    "flightNumber": "GA101",
    "origin": "EZE",
    "destination": "GRU",
    "departureDate": "2026-05-10T00:00:00",
    "departureTime": "08:30",
    "arrivalTime": "11:10",
    "passengers": 2,
    "durationMinutes": 160,
    "cabinClass": "Economy",
    "pricePerPassenger": 253.00,
    "totalPrice": 506.00
  }
]
```

### Booking

```http
POST /api/Bookings
```

Example request:

```json
{
  "flightNumber": "GA101",
  "origin": "EZE",
  "destination": "GRU",
  "passengers": 2,
  "fullName": "John Doe",
  "emailAddress": "john.doe@email.com",
  "documentNumber": "AB123456"
}
```

Example response:

```json
{
  "bookingReference": "GA101-AB123456-GRU-A1B2"
}
```

## Mocked Data

The API uses in-memory mocked data instead of a database or real airline APIs.

Available providers:

- GlobalAir
- BudgetWings

Available airports include:

- EZE - Buenos Aires, Argentina
- AEP - Buenos Aires, Argentina
- COR - Córdoba, Argentina
- MDZ - Mendoza, Argentina
- GRU - São Paulo, Brazil
- GIG - Rio de Janeiro, Brazil
- SCL - Santiago, Chile
- MVD - Montevideo, Uruguay

Current mocked flights are available mainly for these dates and routes:

- 2026-05-10: EZE to GRU
- 2026-05-11: EZE to SCL
- 2026-05-12: AEP to COR
- 2026-05-13: GRU to GIG

## Pricing Rules

### GlobalAir

GlobalAir applies a 15% fuel surcharge to the base fare.

```text
final price per passenger = base fare + 15%
```

The final price is rounded to 2 decimal places.

### BudgetWings

BudgetWings applies a 10% promotional discount to the base fare.

```text
final price per passenger = base fare - 10%
```

The minimum final price is USD 29.99.

## Architecture Decisions

### Provider-based architecture

Each airline provider implements the `IAirlineServices` interface. This keeps provider-specific logic isolated and makes the system easier to extend.


### Aggregation service

`FlightSearchServices` receives all registered airline providers through dependency injection using `IEnumerable<IAirlineServices>`.

This allows the service to execute searches across all providers and combine the results into a single response.

### DTO separation

DTOs are used to separate API contracts from internal provider models.

- `ProviderFlight` represents the internal provider flight data before applying pricing rules.
- `FlightResultDto` represents the response returned to the frontend after applying pricing rules and passenger totals.
- `BookingRequestDto` and `BookingResponseDto` define the booking API contract.

### In-memory data

The challenge does not require persistence, so airports and flights are stored in static in-memory collections. This keeps the implementation simple and focused on business logic, validation, and API structure.

### Dependency Injection

Services are registered with dependency injection in `Program.cs`.

Singleton lifetime is used because the current services are stateless and do not depend on request-specific data or database contexts.

### Validation

Validation is handled in two layers:

- Data annotations in DTOs validate required fields, passenger range, and email format.
- Helper classes validate business rules such as departure date, cabin class, and passenger document type.

Document validation depends on whether the route is domestic or international:

- Domestic route: National ID must contain only digits and be 7 to 8 characters long.
- International route: Passport Number must be alphanumeric and be 6 to 9 characters long.

## CORS Configuration

The API includes a CORS policy named `AllowReact` configured for local frontend development:

```text
http://localhost:5173
https://localhost:5173
http://192.168.1.50:5173
```

This allows a local frontend running on Vite or a similar development server to call the backend API.

## Trade-offs and Known Limitations

- The application uses hardcoded in-memory data instead of a database.
- Booking references are generated but not stored, so they cannot be retrieved later.
- Authentication and authorization are not implemented.
- Simulated delays are used to mimic network or provider latency.
- Error responses are functional but could be improved with a standardized error response format.

