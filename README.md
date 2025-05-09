# Cheap Flights - Technical Documentation

This project has been developed as a technical challenge for Vueling, following the principles of Clean Architecture and SOLID design.

---

## Architecture Overview

The project is structured using Clean Architecture, with strict separation of concerns across independent layers.

### Key Layers:

- **Domain**: Pure domain entities and business rules.
- **Application**: Defines CQRS (Commands and Queries) and repository interfaces. Uses MediatR.
- **Application.DTO**: Defines the DTOs used in the presentation layer.
- **Application.Adapter**: Contains Adapters that transform DTOs into Commands/Queries and invoke the MediatR pipeline. Also handles FluentValidation for request objects.
- **Persistence**: Implements EF Core with SQLite and applies migrations on app startup.
- **ExternalApis**: HTTP client that fetches available flights. Registered with `HttpClientFactory`.
- **Presentation (API)**: Thin controllers that delegate directly to Adapters.

---

## Key Technical Decisions

- FluentValidation is used in Adapters to validate the DTOs format.
- Business rules (e.g. age checks, passenger types) are validated in the Application layer.
- BookingId is generated with a 6-character alphanumeric format (e.g. `BK123A`), ensuring uniqueness.
- The Builder Pattern is used to construct domain entities and result objects (`Booking`, `CreateBookingResult`, `RetrieveBookingResult`).
- Repository and Unit of Work patterns are applied to abstract persistence.
- Automatic audit tracking (`CreatedOn`, `ModifiedOn`, etc.) is handled in the `DbContext`.
- Soft delete is implemented with `IsDeleted`, filtered inside repositories.
- Console logging is used temporarily (due to time constraints). Ideally, **Serilog with file sink** would be configured.
- `ApiGateway` centralizes external HTTP logic with retry support for selected status codes (currently 408 and 504).
- `CancellationToken` is propagated end-to-end from the controller.
- Swagger annotations for response types (`ProducesResponseType`) are not yet included, but planned.

---

## Unit Testing

Built with **xUnit**, **Moq**, and **FluentAssertions**.

### Covered:

- Builders:
  - `BookingBuilder`
  - `CreateBookingResultBuilder`
  - `RetrieveBookingResultBuilder`
- Adapters:
  - `FlightAdapterService`
  - `BookingAdapterService`
- Handlers:
  - `SearchFlightQueryHandler`
  - `CreateBookingCommandHandler`
  - `RetrieveBookingQueryHandler`
- ExternalApis:
  - `FlightApiClient`

### Not covered:

- Validation edge cases for DTOs and business rules
- Integration tests (planned but not included)
- API controller tests

---

## Clean Architecture in Practice

- Outer layers depend on inner layers (not the other way around)
- Infrastructure (e.g., DB, API clients) is abstracted behind interfaces
- Application layer is unaware of presentation details (no DTOs)
- Layers are independently testable and swappable

---

## Known Limitations / Improvements

- Add `ProducesResponseType` attributes to all controllers
- Centralize and standardize error response structure in middleware
- Log request body content in middleware (currently omitted for simplicity)
- Add integration tests for API endpoints (`WebApplicationFactory`)
- Improve validation coverage for DTOs and business rules
- Enhance `BookingId` generation with a true service abstraction

---

## Dataset Consideration

The provided flight dataset contains flights dated in **2023**, which violates the "cannot book flights in the past" rule.

To run full tests:
- Either comment out this validation temporarily
- Or update the dataset with more recent dates

---

## Persistence Details

- Database: **SQLite** (`app.db`)
- Auto-created at app startup via `DbContext.Database.Migrate()`
- Path: project root of `Cheap.Flights.Api`
- Compatible with DB Browser for SQLite
- Temporary files (`app.db-shm`, `app.db-wal`) and database are ignored via `.gitignore`

---

## Logging

- Basic `Console.WriteLine` logging is used
- In a real-world scenario, **Serilog** with file sink should be configured
- Important logs are placed in:
  - Middleware
  - ApiGateway
  - CommandHandlers
  - UnitOfWork

---

The original assignment follows below:

# **Cheap flights**

Trabajas para una empresa que se dedica a la venta de paquetes turísticos, entre los que destacan las reserva de hoteles, coches...
Tu jefe acaba de firmar un acuerdo con varias aerolineas para poder realizar reservas a través de vuestra empresa.

Para esta tarea, necesitarás desarrollar una API que tenga la siguiente funcionalidad:

- Devolver listado de vuelos y precios para el día seleccionado, el número de pasajeros y el origen-destino.
- Poder realizar una reserva de un vuelo seleccionado del listado anterior.
- Poder recuperar información sobre una reserva realizada.

El formato puede estar en XML o JSON.

## **Recursos**

* Obtener la información de los vuelos de la API: https://otd-exams-data.vueling.app/cheap-flights/flight-rs-2.json

* La definición de los schemas para poder generar los modelos se obtienen de:
- FlightRq: https://otd-exams-data.vueling.app/cheap-flights/SchemaFlightRq-2.json
- FlighRs: https://otd-exams-data.vueling.app/cheap-flights/SchemaFlightRs-2.json
- BookingRq: https://otd-exams-data.vueling.app/cheap-flights/SchemaBookingRq-2.json
- BookingRs: https://otd-exams-data.vueling.app/cheap-flights/SchemaBookingRs-2.json
- RetrieveBookingRq: https://otd-exams-data.vueling.app/cheap-flights/SchemaRetrieveBookingRq.json
- Contact: https://otd-exams-data.vueling.app/cheap-flights/SchemaContact.json
- PaxType: https://otd-exams-data.vueling.app/cheap-flights/SchemaPaxType-2.json
- PaxPrice: https://otd-exams-data.vueling.app/cheap-flights/SchemaPaxPrice-2.json

* Recordar que los modelos son un ejemplo, y se pueden adaptar a las necesidades

## **TIPs** 
Hay que escoger el vuelo más barato del listado.
El precio de los vuelos está expresado en decimales (redondeado a 2)
El precio de la reserva varía en función del tipo de pasajero.
El número de la reserva es un identificador único (de 6 caracteres alfanuméricos) y no podrá repetirse.
Se debe comprobar que la información enviada es válida.
Los únicos modelos predefinidos son los de FlightRq, FlightRs, el resto los puedes adaptar según tus necesidades
No permitir reservas para vuelos ya volados.
	
## **cómo pistas te decimos lo que nos gustaría llegar a encontrar**
* Ver cómo cargas el listado de vuelos para una fecha.
* Ver cómo persistes la información de las reservas para poder recueparlas más adelante.
	* En el ejemplo se usa MemoryCache, pero se puede usar cualquier sistema
* Ver cómo separas por N capas el proyecto (Servicios distribuidos, capa de aplicación, capa de dominio, ...). 
* Ver cómo usas SOLID (separación de responsabilidades, Inversión de Dependencias, ...).
* Ver si usas un correcto naming-convention y consistente.
* Ver cómo cubres el código con pruebas unitarias
* Ver cómo defines y gestionas los mensajes (tanto de error como informativos) 
* Ver cómo usas el patrón async/await
* Ver cómo validas los modelos según las reglas de negocio definidas

## **Validaciones**
Tipos de pasajero aceptados:
- ADT (Adulto) (A partir de 16 años)
- CHD (Niño) 
BookingId: Longitud 6
Fecha de vuelo: Obligatoria
Origen: Obligatorio
Destino: Opcional
En la reserva debe haber mínimo 1 adulto.

## **Se valorará**
* Crear APIs por funcionalidad

  **Por favor, una vez finalizada la prueba simplemente debemos crear una petición de incorporación (Pull Request) hacía la rama de master del repositorio, y el último commit contenga en al descripción "Finished". Con esto podemos saber que la prueba ha sido finalizada.**
 