using HotelUp.Customer.Tests.Integration.TestContainers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.Keycloak;
using Testcontainers.PostgreSql;

namespace HotelUp.Customer.Tests.Integration;

public class TestWebAppFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = 
        TestDatabaseFactory.Create();
    
    private readonly KeycloakContainer _keycloakContainer = 
        KeycloakContainerFactory.Create();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            var dbContextTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsSubclassOf(typeof(DbContext)) && !t.IsAbstract)
                .ToList();
            
            foreach (var dbContextType in dbContextTypes)
            {
                services.RemoveAll(dbContextType);
                var dbContextOptionsType = typeof(DbContextOptions<>).MakeGenericType(dbContextType);
                services.RemoveAll(dbContextOptionsType);

                var addDbContextMethod = typeof(EntityFrameworkServiceCollectionExtensions)
                    .GetMethods()
                    .FirstOrDefault(m => m.Name == "AddDbContext" && m.IsGenericMethod);

                if (addDbContextMethod != null)
                {
                    var genericAddDbContextMethod = addDbContextMethod.MakeGenericMethod(dbContextType);
                    
                    Action<DbContextOptionsBuilder> optionsAction = options =>
                    {
                        options.UseNpgsql(_dbContainer.GetConnectionString());
                    };
                    
                    genericAddDbContextMethod.Invoke(null, new object[] { services, optionsAction, null, null });
                }
            }
        });
    }

    public async Task InitializeAsync()
    {
        var tasks = new List<Task>
        {
            _dbContainer.StartAsync(),
            // _keycloakContainer.StartAsync()
        };
        
        await Task.WhenAll(tasks);
    }

    public new async Task DisposeAsync()
    {
        var tasks = new List<Task>
        {
            _dbContainer.StopAsync(),
            // _keycloakContainer.StopAsync()
        };
        
        await Task.WhenAll(tasks);
    }
}