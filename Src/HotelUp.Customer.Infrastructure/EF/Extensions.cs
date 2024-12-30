using HotelUp.Customer.Infrastructure.EF.Contexts;
using HotelUp.Customer.Infrastructure.EF.Health;
using HotelUp.Customer.Infrastructure.EF.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Infrastructure.EF;

internal static class Extensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigurePostgres(configuration);
        services.AddPostgres<WriteDbContext>();
        services.AddPostgres<ReadDbContext>();
        services.AddHostedService<DatabaseInitializer>();
        services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>("Database");
        return services;
    }
    
    

}