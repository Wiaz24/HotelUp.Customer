using HotelUp.Customer.Infrastructure.EFCore.Contexts;
using HotelUp.Customer.Infrastructure.EFCore.Health;
using HotelUp.Customer.Infrastructure.EFCore.Postgres;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Npgsql;

namespace HotelUp.Customer.Infrastructure.EFCore;

internal static class Extensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigurePostgres();
        services.AddPostgres<WriteDbContext>();
        services.AddPostgres<ReadDbContext>();
        services.AddHostedService<DatabaseInitializer>();
        services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>("Database");
        
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<PostgresOptions>>();
        var connectionString = options.Value.ConnectionString;
        services.AddScoped(_ => new NpgsqlConnection(connectionString));
        return services;
    }
    
    

}