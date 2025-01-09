using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using HotelUp.Customer.API.DTOs;
using HotelUp.Customer.Tests.Integration.Utils;
using HotelUp.Customer.Tests.Shared.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shouldly;
using Xunit.Abstractions;

namespace HotelUp.Customer.Tests.Integration.Controllers;

[Collection(nameof(CommandsControllerTests))]
public class CommandsControllerTests : ControllerTestsBase
{
    private readonly ITestOutputHelper _testOutputHelper;
    private const string Prefix = "api/customer/commands";
    
    public CommandsControllerTests(TestWebAppFactory factory, ITestOutputHelper testOutputHelper) : base(factory)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    
    [Fact]
    public async Task CreateReservation_WhenValidRequest_ShouldReturnCreated()
    {
        // Arrange
        _testOutputHelper.WriteLine("Creating a reservation");
        var clientId = Guid.NewGuid();
        var client = await ClientGenerator.GenerateSampleClient(clientId);
        DbContext.Clients.Add(client);
        var rooms = await RoomGenerator.GenerateSampleRooms(3, 2);
        DbContext.Rooms.AddRange(rooms);
        await DbContext.SaveChangesAsync();
        _testOutputHelper.WriteLine("Client and rooms created");
        var httpClient = Factory.CreateClient();
        var token = MockJwtTokens.GenerateJwtToken(new []
        {
            new Claim(ClaimTypes.NameIdentifier, clientId.ToString())
        });
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var reservationRequest = new CreateReservationDto()
        {
            RoomNumbers = new [] { 1 },
            TenantsData = TenantDataGenerator.GenerateSampleTenantsDataDtos(2).ToArray(),
            StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
            EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1))
        };
        _testOutputHelper.WriteLine("Reservation request created");
        // Act
        var response = await httpClient.PostAsJsonAsync($"{Prefix}/create-reservation", reservationRequest);
        _testOutputHelper.WriteLine("Reservation request sent");
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        
    }

    [Fact]
    public async Task CreateClient_WhenTokenIsPresent_ShouldReturnCreated()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        var client = Factory.CreateClient();
        var token = MockJwtTokens.GenerateJwtToken(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, clientId.ToString())
        });
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await client.PostAsync($"{Prefix}/create-client", null);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
}