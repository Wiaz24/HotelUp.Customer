using System.Net;
using Shouldly;

namespace HotelUp.Customer.Tests.Integration.Controllers;

[Collection("QueriesControllerTests")]
public class QueriesControllerTests : ControllerTestsBase
{
    private const string Prefix = "api/Customer/queries";

    public QueriesControllerTests(TestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetOne_ShouldReturnOK_WhenDatabaseExists()
    {
        var response = await HttpClient.GetAsync(Prefix);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}