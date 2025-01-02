using HotelUp.Customer.Tests.Integration.TestContainers;
using HotelUp.Customer.Tests.Integration.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Testcontainers.Keycloak;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;

namespace HotelUp.Customer.Tests.Integration;

public class TestWebAppFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = 
        TestDatabaseFactory.Create();
    
    // private readonly KeycloakContainer _keycloakContainer = 
    //     KeycloakContainerFactory.Create();
    
    private readonly RabbitMqContainer _rabbitMqContainer = 
        RabbitMqContainerFactory.Create();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddMockJwtTokens();
            services.AddMockPostgres(_dbContainer);
        });
    }
    
    public Task InitializeAsync()
    {
        var tasks = new List<Task>
        {
            _dbContainer.StartAsync(),
            _rabbitMqContainer.StartAsync()
            // _keycloakContainer.StartAsync()
        };
        return Task.WhenAll(tasks);
    }

    public new Task DisposeAsync()
    {
        var tasks = new List<Task>
        {
            _dbContainer.StopAsync(),
            _rabbitMqContainer.StopAsync()
            // _keycloakContainer.StopAsync()
        };
        return Task.WhenAll(tasks);
    }
}