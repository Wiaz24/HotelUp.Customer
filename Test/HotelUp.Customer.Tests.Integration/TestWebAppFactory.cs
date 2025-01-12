using DotNet.Testcontainers.Containers;
using HotelUp.Customer.Tests.Integration.TestContainers;
using HotelUp.Customer.Tests.Integration.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;

namespace HotelUp.Customer.Tests.Integration;

public class TestWebAppFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly List<DockerContainer> _containers = new();

    private readonly PostgreSqlContainer _dbContainer =
        TestDatabaseFactory.Create();
    private readonly RabbitMqContainer _rabbitMqContainer =
        RabbitMqContainerFactory.Create();

    public TestWebAppFactory()
    {
        _containers.Add(_dbContainer);
        _containers.Add(_rabbitMqContainer);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("Postgres:ConnectionString", _dbContainer.GetConnectionString());
        
        var port = _rabbitMqContainer.GetMappedPublicPort(5672);
        builder.UseSetting("MessageBroker:RabbitMQ:Host", $"amqp://localhost:{port}");
        
        builder.UseEnvironment("Testing");
        builder.ConfigureTestServices(services =>
        {
            services.AddMockJwtTokens();
        });
        base.ConfigureWebHost(builder);
    }

    public async Task InitializeAsync()
    {
        var tasks = _containers
            .Select(c => c.StartAsync());
        await Task.WhenAll(tasks);
    }

    public new Task DisposeAsync()
    {
        var tasks = _containers
            .Select(c => c.StopAsync());
        return Task.WhenAll(tasks);
    }
}