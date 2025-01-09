using HotelUp.Customer.Domain.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Testcontainers.PostgreSql;

namespace HotelUp.Customer.Tests.Integration.Utils;

public static class MockPostgres
{
    private const string SchemaName = "customer";
    public static IServiceCollection AddMockPostgres(this IServiceCollection services, PostgreSqlContainer dbContainer)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
        var dbContextTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(DbContext)) && !t.IsAbstract)
            .ToList();
        
        var connectionString = dbContainer.GetConnectionString();
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.MapEnum<DocumentType>($"{SchemaName}.document_type");
        dataSourceBuilder.MapEnum<PresenceStatus>($"{SchemaName}.presence_status");
        dataSourceBuilder.MapEnum<ReservationStatus>($"{SchemaName}.reservation_status");
        dataSourceBuilder.MapEnum<RoomType>($"{SchemaName}.room_type");
        var dataSource = dataSourceBuilder.Build();
            
        foreach (var dbContextType in dbContextTypes)
        {
            services.RemoveAll(dbContextType);
            var dbContextOptionsType = typeof(DbContextOptions<>).MakeGenericType(dbContextType);
            services.RemoveAll(dbContextOptionsType);

            var addDbContextMethod = typeof(EntityFrameworkServiceCollectionExtensions)
                .GetMethods()
                .FirstOrDefault(m => m is { Name: "AddDbContext", IsGenericMethod: true });

            if (addDbContextMethod is null) continue;
                    
            Action<DbContextOptionsBuilder> optionsAction = options =>
            {
                options.UseNpgsql(dataSource);
            };
            // addDbContextMethod.Invoke(null, [services, optionsAction]);  
            
            var genericMethod = addDbContextMethod.MakeGenericMethod(dbContextType);
            genericMethod.Invoke(null, [services, optionsAction, null, null]);
            // genericAddDbContextMethod.Invoke(null, new object[] { services, optionsAction, null, null });
        }

        return services;
    }
}