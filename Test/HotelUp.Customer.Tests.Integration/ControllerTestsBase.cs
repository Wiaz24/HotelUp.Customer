namespace HotelUp.Customer.Tests.Integration;

public abstract class ControllerTestsBase : IClassFixture<TestWebAppFactory>
{
    protected HttpClient HttpClient { get;}

    protected ControllerTestsBase(TestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
    }
}