using FlightBooking.Application.Builders;
using FlightBooking.Application.Features.Booking.Commands;
using FluentAssertions;

namespace FlightBooking.Tests.Builders;

/// <summary>  
/// Tests for the CreateBookingResultBuilder.  
/// </summary>  
public class CreateBookingResultBuilderTests
{
    /// <summary>
    /// Test to verify that the CreateBookingResultBuilder returns the expected result.
    /// </summary>
    [Fact]
    public void CreateBookingResultBuilderShouldReturnExpectedResult()
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
            Email = "test@example.com",
        };

        var builder = new CreateBookingResultBuilder()
            .WithBookingId("BK0001")
            .WithFlight("VY1234", new DateTime(2025, 4, 20, 0, 0, 0, DateTimeKind.Utc), "BCN", "LON")
            .WithBookingDate(DateTime.UtcNow)
            .WithPassengers(new List<PassengerCommand> { passanger })
            .WithContact(contact)
            .WithTotalPrice(150.50m);

        // Act  
        var result = builder.Build();

        // Assert  
        result.BookingId.Should().Be("BK0001");
        result.FlightNumber.Should().Be("VY1234");
        result.Origin.Should().Be("BCN");
        result.Destination.Should().Be("LON");
        result.Passengers.Should().HaveCount(1);
        result.Contact!.Email.Should().Be("test@example.com");
        result.TotalPrice.Should().Be(150.50m);
    }
}
