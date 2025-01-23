using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using HotelUp.Customer.API.DTOs;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using HotelUp.Customer.Tests.Integration.Utils;
using HotelUp.Customer.Tests.Shared.Utils;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NSubstitute;

using Quartz;
using Quartz.Impl.Matchers;

using Shouldly;
using Xunit.Abstractions;

namespace HotelUp.Customer.Tests.Integration.Controllers;

[Collection(nameof(CommandsControllerTests))]
public class CommandsControllerTests : IntegrationTestsBase
{
    private readonly ITestOutputHelper _testOutputHelper;
    private const string Prefix = "api/customer/commands";
    private readonly ISchedulerFactory _schedulerFactory;
    private static int RoomCount { get; set; } = 0;
    public CommandsControllerTests(TestWebAppFactory factory, ITestOutputHelper testOutputHelper) : base(factory)
    {
        _testOutputHelper = testOutputHelper;
        _schedulerFactory = Factory.Services.GetRequiredService<ISchedulerFactory>();
    }
    
    #region helpers
    private HttpClient CreateHttpClientWithToken(Guid clientId)
    {
        var httpClient = Factory.CreateClient();
        var token = MockJwtTokens.GenerateJwtToken(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, clientId.ToString()),
            new Claim(ClaimTypes.Role, "Clients")
        });
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return httpClient;
    }

    private async Task<Client> CreateSampleClient()
    {
        var clientId = Guid.NewGuid();
        var client = await ClientGenerator.GenerateSampleClient(clientId);
        
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        dbContext.Clients.Add(client);
        await dbContext.SaveChangesAsync();
        return client;
    }
    
    private async Task<List<Room>> CreateSampleRooms(int count, int tenantsCount)
    {
        var rooms = (await RoomGenerator
            .GenerateSampleRooms(count, tenantsCount, RoomCount+1))
            .ToList();
        
        RoomCount += count;
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        dbContext.Rooms.AddRange(rooms);
        await dbContext.SaveChangesAsync();
        return rooms;
    }
    #endregion
    
    [Fact]
    public async Task CreateReservation_WhenValidRequest_ShouldReturnCreated()
    {
        // Arrange
        var client = await CreateSampleClient();
        var rooms  = await CreateSampleRooms(1, 2);
        var httpClient = CreateHttpClientWithToken(client.Id);
        var reservationRequest = new CreateReservationDto()
        {
            RoomNumbers = rooms.Select(r => r.Number).ToArray(),
            TenantsData = TenantDataGenerator.GenerateSampleTenantsDataDtos(2).ToArray(),
            StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
            EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1))
        };
        
        // Act
        var response = await httpClient.PostAsJsonAsync($"{Prefix}/create-reservation", reservationRequest);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateReservation_ShouldEnqueueAndExecuteAnonymizationJob()
    {
        // Arrange
        var scheduler = await _schedulerFactory.GetScheduler("CustomerScheduler");
        scheduler.ShouldNotBeNull();
        await scheduler.Clear();
        
        var client = await CreateSampleClient();
        var rooms = await CreateSampleRooms(3, 2);
        var httpClient = CreateHttpClientWithToken(client.Id);
        
        _testOutputHelper.WriteLine($"sample data created");
        var reservationRequest = new CreateReservationDto()
        {
            RoomNumbers = rooms.Select(r => r.Number).ToArray(),
            TenantsData = TenantDataGenerator.GenerateSampleTenantsDataDtos(2).ToArray(),
            StartDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-35)),
            EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-30))
        };
        
        // Act
        var response = await httpClient.PostAsJsonAsync($"{Prefix}/create-reservation", reservationRequest);
        var jobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
            var reservation = await dbContext.Reservations.FirstOrDefaultAsync(x => x.Client.Id == client.Id);
            reservation.ShouldNotBeNull();
        }
        jobKeys.Count.ShouldBe(1);
    }

    [Fact]
    public async Task CreateClient_WhenTokenIsPresent_ShouldReturnCreated()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        var client = Factory.CreateClient();
        var token = MockJwtTokens.GenerateJwtToken(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, clientId.ToString()),
            new Claim(ClaimTypes.Email, "example@email.com")
        });
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        HttpResponseMessage response = default!;
        var exception = await Record.ExceptionAsync(async () =>
        {
            response = await client.PostAsync($"{Prefix}/create-client", null);
        });
        
        // Assert
        if (exception is not null)
        {
            _testOutputHelper.WriteLine(exception.ToString());
        }
        exception.ShouldBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
}