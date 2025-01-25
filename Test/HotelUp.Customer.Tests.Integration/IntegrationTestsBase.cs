using System.Net.Http.Headers;
using System.Security.Claims;

using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Infrastructure.EFCore.Contexts;
using HotelUp.Customer.Tests.Integration.Utils;
using HotelUp.Customer.Tests.Shared.Utils;

using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Tests.Integration;

public abstract class IntegrationTestsBase : IClassFixture<TestWebAppFactory>
{
    protected TestWebAppFactory Factory { get; }
    protected readonly IServiceProvider _serviceProvider;
    
    protected IntegrationTestsBase(TestWebAppFactory factory)
    {
        Factory = factory;
        _serviceProvider = factory.Services;
    }
    
    protected HttpClient CreateHttpClientWithClaims(Guid clientId, IEnumerable<Claim> claims)
    {
        var httpClient = Factory.CreateClient();
        var token = MockJwtTokens.GenerateJwtToken(new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, clientId.ToString()),
        }.Concat(claims));
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return httpClient;
    }
    
    protected async Task<Client> CreateSampleClient()
    {
        var clientId = Guid.NewGuid();
        var client = await ClientGenerator.GenerateSampleClient(clientId);
        
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        dbContext.Clients.Add(client);
        await dbContext.SaveChangesAsync();
        return client;
    }
}