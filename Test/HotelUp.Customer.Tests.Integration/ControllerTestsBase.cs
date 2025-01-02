using System.Net.Http.Headers;
using System.Security.Claims;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using HotelUp.Customer.Tests.Integration.Utils;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Tests.Integration;

public abstract class ControllerTestsBase : IClassFixture<TestWebAppFactory>, IDisposable
{
    protected TestWebAppFactory Factory { get; }
    
    protected readonly IBus Bus;
    protected readonly WriteDbContext DbContext;
    protected readonly HttpClient DefaultClient;
    

    protected ControllerTestsBase(TestWebAppFactory factory)
    {
        Factory = factory;
        DefaultClient = factory.CreateClient();
        Bus = factory.Services.GetRequiredService<IBus>();
        DbContext = factory.Services.GetRequiredService<WriteDbContext>();
        DefaultClient.DefaultRequestHeaders.Add("Accept", "application/json");
        var testEmail = "test@email.com";
        var token = MockJwtTokens.GenerateJwtToken(new[]
        {
            new Claim(ClaimTypes.Email, testEmail),
        });
        DefaultClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public void Dispose()
    {
        DbContext?.Dispose();
    }
}