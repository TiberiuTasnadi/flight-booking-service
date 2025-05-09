

using FlightBooking.Application.Builders;
using FlightBooking.Application.Features.Booking.Commands;
using FlightBooking.Domain.Entities;
using FluentAssertions;

namespace FlightBooking.Tests.Builders;

/// <summary>
/// Tests for the BookingBuilder.
/// </summary>
public class BookingBuilderTests
{
    /// <summary>
    /// Tests if the BookingBuilder returns the expected domain entity.
    /// </summary>
    [Fact]
    public void BookingBuilderShouldReturnExpectedDomainEntity()
    {
        // Arrange
        var passanger = new Passenger
        {
            Type = "ADT",
            FirstName = "Test1",
            LastName = "Test2",
            BirthDate = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc),
        };

        var contact = new Contact
        {
            FirstName = "Test3",
            LastName = "Test4",
            Email = "test@example.com",
        };

        var bookingDate = DateTime.UtcNow;

        var booking = new BookingBuilder()
            .WithBookingId("BK5678")
            .WithFlightDetails("VY8888", bookingDate, "BIO", "AMS")
            .WithPassengers(new List<Passenger> { passanger })
            .WithContact(contact)
            .WithBookingDate(bookingDate)
            .WithTotalPrice(89.99m)
            .Build();

        // Assert
        booking.BookingId.Should().Be("BK5678");
        booking.FlightNumber.Should().Be("VY8888");
        booking.Passengers.Should().HaveCount(1);
        booking.Contact.Email.Should().Be("test@example.com");
        booking.TotalPrice.Should().Be(89.99m);
    }
}
