using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace HotelUp.Customer.Tests.Integration.Utils;

public static class MockPostgres
{
    public static IServiceCollection AddMockPostgres(this IServiceCollection services, PostgreSqlContainer dbContainer)
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
                    options.UseNpgsql(dbContainer.GetConnectionString());
                };
                    
                genericAddDbContextMethod.Invoke(null, new object[] { services, optionsAction, null, null });
            }
        }

        return services;
    }
}