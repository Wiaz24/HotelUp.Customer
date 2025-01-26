using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using HotelUp.Customer.Tests.Integration.Utils;
using Shouldly;

namespace HotelUp.Customer.Tests.Integration.Controllers;

[Collection(nameof(QueriesControllerTests))]
public class QueriesControllerTests : IntegrationTestsBase
{
    private const string Prefix = "api/customer/queries";
    public QueriesControllerTests(TestWebAppFactory factory) : base(factory)
    {
    }
    
}