# FlightBooking ✈️

A clean and modular .NET 8 API for searching flights and managing bookings, built using Clean Architecture and CQRS principles. Originally designed for a technical assessment, this project has been refined and published as a portfolio piece.

## ✨ Features

- 🔍 **Search flights** by date, origin and destination, returning the cheapest option.
- 🧾 **Book flights** by selecting a flight and entering passenger details.
- 📂 **Retrieve booking** using a unique Booking ID.
- ✅ Input validation using FluentValidation.
- 🔄 Business rules validation within CQRS Handlers.
- 💽 Persistence layer using EF Core + SQLite.
- 🔗 Decoupled architecture using interfaces and dependency injection.
- 🔧 Uses an internal static JSON file to simulate flight data.

## 🏗️ Tech Stack

- .NET 8
- CQRS + MediatR
- Clean Architecture
- FluentValidation
- AutoMapper
- SQLite + EF Core
- Swagger (OpenAPI)
- xUnit + Moq + FluentAssertions for unit testing

## 🧪 Testing Strategy

- ✅ Unit tests for CQRS handlers (Search & Booking)
- ✅ Tests for result builders
- ✅ Adapter validation and mapping tests
- ⚠️ **Pending**: More exhaustive DTO validation coverage and integration tests *(documented in TODOs)*

## ⚠️ Known Limitations

- The dataset used for available flights contains dates in the past (2023). 
  - ✏️ For testing, business validation to block past flights has been commented.
  - 📝 This is explained in the documentation and can be toggled if needed.
- Request logs are written using `Console.WriteLine` due to time constraints.
  - 🔧 Ideally, a logging framework like **Serilog** would be integrated with file sinks.

## 🛠️ How to Run

1. Clone the repository.
2. Run the solution. EF Core migrations are applied at startup.
3. Open Swagger at `/swagger` to explore and test the endpoints.

> 🧰 The SQLite database is created as `app.db` in the root of the API project.
> You can inspect it using [DB Browser for SQLite](https://sqlitebrowser.org/).

## 📌 Future Improvements

- [ ] Add Serilog logging with file sink.
- [ ] Improve exception handling unification in middleware.
- [ ] Add integration tests and improve DTO validation test coverage.
- [ ] Externalize configuration and enhance retry strategy.

## 📂 Static JSON Source

The flight listing uses a static JSON file embedded in the project under:
```
FlightBooking.Infrastructure.ExternalApis/Data/flights.json
```
