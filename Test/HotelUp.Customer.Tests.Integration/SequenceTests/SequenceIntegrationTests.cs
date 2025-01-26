using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

using HotelUp.Customer.API.DTOs;
using HotelUp.Customer.Application.Queries.DTOs;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Tests.Shared.Utils;

using Microsoft.AspNetCore.Http;

using Shouldly;

using Xunit.Abstractions;

namespace HotelUp.Customer.Tests.Integration.SequenceTests;

[Collection(nameof(SequenceIntegrationTests))]
public class SequenceIntegrationTests : IntegrationTestsBase
{
    private readonly ITestOutputHelper _output;
    private const string Prefix = "api/customer";
    public SequenceIntegrationTests(TestWebAppFactory factory, ITestOutputHelper output) 
        : base(factory)
    {
        _output = output;
    }
    
    private async Task<MultipartFormDataContent> CreateMultipartFormDataWithImage(Room room)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "room1.jpg");
        var fileName = Path.GetFileName(filePath);
        
        var fileContent = await File.ReadAllBytesAsync(filePath);
        
        var form = new MultipartFormDataContent();

        // Dodaj pozostałe właściwości DTO jako StringContent
        form.Add(new StringContent(room.Capacity.Value.ToString()), nameof(CreateRoomDto.Capacity));
        form.Add(new StringContent(room.Floor.Value.ToString()), nameof(CreateRoomDto.Floor));
        form.Add(new StringContent(room.Type.ToString()), nameof(CreateRoomDto.Type));
        form.Add(new StringContent(room.Id.ToString()), nameof(CreateRoomDto.Number));
        form.Add(new StringContent(room.WithSpecialNeeds.ToString()), nameof(CreateRoomDto.WithSpecialNeeds));

        // Dodaj plik jako ByteArrayContent
        var fileStreamContent = new ByteArrayContent(fileContent);
        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        form.Add(fileStreamContent, nameof(CreateRoomDto.Image), fileName);
        return form;
    }

    [Fact]
    public async Task CreateRoom_CreateReservation_GetFreeRoomsSequence()
    {
        var adminClaim = new Claim(ClaimTypes.Role, "Admins");
        var client = await CreateSampleClient();
        var httpClient = CreateHttpClientWithClaims(client.Id, [adminClaim]);
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2));
        var endDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(3));
        
        var response1 = await httpClient.GetAsync(
            $"{Prefix}/queries/get-free-rooms?StartDate={startDate.ToString("O")}&EndDate={endDate.ToString("O")}");
        _output.WriteLine($"Response1: {await response1.Content.ReadAsStringAsync()}");
        response1.StatusCode.ShouldBe(HttpStatusCode.OK);
        var firstContent = await response1.Content.ReadFromJsonAsync<IEnumerable<RoomDto>>();
        firstContent.ShouldNotBeNull();
        firstContent.ShouldBeEmpty();
        
        // Create room
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "room1.jpg");
        var fileName = Path.GetFileName(filePath);
        var fileContent = await File.ReadAllBytesAsync(filePath);
        
        var room = (await RoomGenerator.GenerateSampleRooms(1, 1, 1)).First();
        using var form = await CreateMultipartFormDataWithImage(room);
        var response2 = await httpClient.PostAsync($"{Prefix}/commands/create-room", form);
        _output.WriteLine($"Response2: {await response2.Content.ReadAsStringAsync()}");
        response2.StatusCode.ShouldBe(HttpStatusCode.Created);
        
        // Create reservation
        
        var reservationDto = new CreateReservationDto
        {
            RoomNumbers = [room.Number],
            TenantsData = TenantDataGenerator.GenerateSampleTenantsDataDtos(1).ToArray(),
            StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
            EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1))
        };
        
        var response3 = await httpClient.PostAsJsonAsync($"{Prefix}/commands/create-reservation", reservationDto);
        _output.WriteLine($"Response3: {await response3.Content.ReadAsStringAsync()}");
        response3.StatusCode.ShouldBe(HttpStatusCode.Created);
        
        // Get free rooms
        var response4 = await httpClient.GetAsync(
            $"{Prefix}/queries/get-free-rooms?StartDate={startDate.ToString("O")}&EndDate={endDate.ToString("O")}");
        _output.WriteLine($"Response4: {await response4.Content.ReadAsStringAsync()}");
        response4.StatusCode.ShouldBe(HttpStatusCode.OK);
        
    }
    
    // [Fact]
    // public async Task GetUserReservations_WithDeltaInstalled_ShouldProvideAndUseETags()
    // {
    //     // Arrange1
    //     var client = await CreateSampleClient();
    //     var adminClaim = new Claim(ClaimTypes.Role, "Admins");
    //     var httpClient = CreateHttpClientWithClaims(client.Id, [adminClaim]);
    //     var requestPath = $"{Prefix}/queries/get-users-reservations";
    //     
    //     // Act1
    //     var response = await httpClient.GetAsync(requestPath);
    //     response.StatusCode.ShouldBe(HttpStatusCode.OK);
    //     var eTag = response.Headers.ETag;
    //     
    //     // Assert1
    //     eTag.ShouldNotBeNull();
    //     
    //     // Arrange2
    //     var request = new HttpRequestMessage(HttpMethod.Get, requestPath);
    //     request.Headers.IfNoneMatch.Add(eTag);
    //     
    //     // Act2
    //     var response2 = await httpClient.SendAsync(request);
    //     
    //     // Assert2
    //     response2.StatusCode.ShouldBe(HttpStatusCode.NotModified);
    // }
}