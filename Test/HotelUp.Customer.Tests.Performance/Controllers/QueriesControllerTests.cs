﻿using System.Text.Json;
using HotelUp.Customer.API.DTOs;
using HotelUp.Customer.Domain.Consts;

using Microsoft.AspNetCore.Http;

using NBomber.Contracts.Stats;
using NBomber.CSharp;
using NBomber.Http.CSharp;

using Shouldly;

using Xunit.Abstractions;

namespace HotelUp.Customer.Tests.Performance.Controllers;

public class QueriesControllerTests : PerformanceTestsBase
{
    private readonly ITestOutputHelper _outputHelper;
    
    public QueriesControllerTests(TestWebAppFactory factory, ITestOutputHelper output) 
        : base(factory)
    {
        _outputHelper = output;
    }

    [Theory]
    [InlineData(500)]
    public async Task GetFreeRooms_ShouldHandleAtLeastXRequestsPerSecond(int expectedRPS)
    {
        const string url = "api/customer/queries/get-free-rooms";
        const int durationSeconds = 5;
        
        var dto = new GetFreeRoomsDto()
        {
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
            RoomType = RoomType.Basic,
            RoomCapacity = 2
        };
        var payload = new StringContent(JsonSerializer.Serialize(dto));

        var client = Factory.CreateClient();
        var initialResponse = await client.GetAsync(url);
        var etag = initialResponse.Headers.ETag?.Tag;

        var scenario = Scenario.Create("getFreeRoomsScenario", async context =>
            {
                var request =
                    Http.CreateRequest("GET", url)
                        .WithHeader("Accept", "application/json")
                        .WithHeader("If-None-Match", etag)
                        .WithBody(payload);
                
                var response = await Http.Send(client, request);
                if (response.StatusCode is "NotModified" or "OK")
                {
                    return Response.Ok();
                }
                return Response.Fail();
            })
            .WithWarmUpDuration(TimeSpan.FromSeconds(5))
            .WithLoadSimulations(Simulation.KeepConstant(100, TimeSpan.FromSeconds(durationSeconds)));
        
        
        // Assert
        var stats = NBomberRunner
            .RegisterScenarios(scenario)
            .WithReportFileName("getFreeRoomsReport")
            .WithReportFolder("reports")
            .WithReportFormats(ReportFormat.Txt, ReportFormat.Html)
            .Run();
        var rps = stats.AllOkCount / durationSeconds;

        _outputHelper.WriteLine($"OK count: {stats.AllOkCount}, FAIL count: {stats.AllFailCount}, RPS: {rps}");
        rps.ShouldBeGreaterThanOrEqualTo(expectedRPS);
    }
}