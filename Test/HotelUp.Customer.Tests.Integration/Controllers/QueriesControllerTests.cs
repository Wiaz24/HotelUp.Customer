using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using HotelUp.Customer.Tests.Integration.Utils;
using Shouldly;

namespace HotelUp.Customer.Tests.Integration.Controllers;

[Collection("QueriesControllerTests")]
public class QueriesControllerTests : ControllerTestsBase
{
    private const string Prefix = "api/customer/queries";
    public QueriesControllerTests(TestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetOne_ShouldReturnOK_WhenDatabaseExists()
    {
        var response = await DefaultClient.GetAsync(Prefix);
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GetLoggedInUser_WhenTokenIsPresent_ShouldReturnOK()
    {
        var httpClient = Factory.CreateClient();
        var testEmail = "test@email.com";
        var token = MockJwtTokens.GenerateJwtToken(new[]
        {
            new Claim(ClaimTypes.Email, testEmail),
        });
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await httpClient.GetAsync($"{Prefix}/logged-in-user");
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var contentJson = await response.Content.ReadAsStringAsync();
        contentJson.ShouldBe($"Hello {testEmail}!");
    }
}