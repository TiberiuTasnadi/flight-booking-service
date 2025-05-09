using FlightBooking.Application.Builders;
using FlightBooking.Application.Features.Booking.Commands;
using FluentAssertions;

namespace FlightBooking.Tests.Builders;

/// <summary>
/// Tests for the RetrieveBookingResultBuilder class.
/// </summary>
public class RetrieveBookingResultBuilderTests
{
    /// <summary>
    /// Tests the RetrieveBookingResultBuilder to ensure it returns the expected result.
    /// </summary>
    [Fact]
    public void RetrieveBookingResultBuilderShouldReturnExpectedResult()
    {
        // Arrange
        var passanger = new PassengerCommand
        {
            Type = "ADT",
            FirstName = "Test1",
            LastName = "Test2",
            BirthDate = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc),
        };

        var contact = new ContactCommand
        {
            FirstName = "Test3",
            LastName = "Test4",
            Email = "alice@example.com",
        };

        var builder = new RetrieveBookingResultBuilder()
            .WithBookingId("BK1000")
            .WithFlight("VY5678", new DateTime(2025, 5, 15, 0 , 0 , 0, DateTimeKind.Utc), "PMI", "PAR")
            .WithBookingDate(DateTime.UtcNow)
            .WithPassengers(new List<PassengerCommand> { passanger })
            .WithContact(contact)
            .WithTotalPrice(200.99m);

        // Act
        var result = builder.Build();

        // Assert
        result.BookingId.Should().Be("BK1000");
        result.FlightNumber.Should().Be("VY5678");
        result.Passengers.Should().HaveCount(1);
        result.Contact!.FirstName.Should().Be("Test3");
        result.TotalPrice.Should().Be(200.99m);
    }
}