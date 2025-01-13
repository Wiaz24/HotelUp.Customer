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
}