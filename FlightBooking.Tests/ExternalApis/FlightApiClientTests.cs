using FlightBooking.Application.Exceptions;
using FlightBooking.Application.Models.ExternalApis;
using FlightBooking.Infrastructure.ExternalApis.Clients;
using FlightBooking.Infrastructure.ExternalApis.Configurations;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq.Protected;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Tests.ExternalApis;

/// <summary>
/// Tests for the FlightApiClient.
/// </summary>
public class FlightApiClientTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly FlightApiClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlightApiClientTests"/> class.
    /// </summary>
    public FlightApiClientTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://api.example.com/")
        };

        var options = Options.Create(new FlightApiOptions
        {
            BaseUrl = "https://api.example.com/flights"
        });

        _client = new FlightApiClient(_httpClient, options);
    }

    /// <summary>
    /// Tests that GetAvailableFlightsAsync returns a list of flights when the API returns a successful response.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task GetAvailableFlightsAsyncShouldReturnFlightsWhenApiReturnsSuccess()
    {
        // Arrange
        var expectedFlights = new List<ExternalFlightDto>
        {
            new ExternalFlightDto
            {
                FlightKey = "FK123",
                FlightNumber = "VY123",
                Origin = "BCN",
                Destination = "PAR",
                FlightDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                PaxPrices = new List<ExternalPaxPriceDto>
                {
                    new ExternalPaxPriceDto { Type = "ADT", Price = 100 }
                }
            }
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(expectedFlights)
            });

        // Act
        var result = await _client.GetAvailableFlightsAsync();

        // Assert
        result.Should().BeEquivalentTo(expectedFlights);

        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Get &&
                req.RequestUri == new Uri("https://api.example.com/flights")),
            ItExpr.IsAny<CancellationToken>());
    }

    /// <summary>
    /// Tests that GetAvailableFlightsAsync throws an ExternalApiException when the API returns an error.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task GetAvailableFlightsAsyncShouldThrowExternalApiExceptionWhenApiReturnsError()
    {
        // Arrange
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });

        // Act
        Func<Task> act = async () => await _client.GetAvailableFlightsAsync();

        // Assert
        await act.Should().ThrowAsync<ExternalApiException>()
            .WithMessage("Failed to fetch flights. Status: InternalServerError");

        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Get &&
                req.RequestUri == new Uri("https://api.example.com/flights")),
            ItExpr.IsAny<CancellationToken>());
    }

    /// <summary>
    /// Tests that GetAvailableFlightsAsync returns an empty list when the API returns no content.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task GetAvailableFlightsAsyncShouldReturnEmptyListWhenApiReturnsNoContent()
    {
        // Arrange
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create((List<ExternalFlightDto>?)null)
            });

        // Act
        var result = await _client.GetAvailableFlightsAsync();

        // Assert
        result.Should().BeEmpty();

        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Get &&
                req.RequestUri == new Uri("https://api.example.com/flights")),
            ItExpr.IsAny<CancellationToken>());
    }
}