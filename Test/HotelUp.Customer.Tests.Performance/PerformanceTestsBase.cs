namespace HotelUp.Customer.Tests.Performance;

public abstract class PerformanceTestsBase : IClassFixture<TestWebAppFactory>
{
    protected TestWebAppFactory Factory { get; }
    protected readonly IServiceProvider _serviceProvider;
    
    protected PerformanceTestsBase(TestWebAppFactory factory)
    {
        Factory = factory;
        _serviceProvider = factory.Services;
    }
}