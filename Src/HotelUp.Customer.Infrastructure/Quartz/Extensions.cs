using HotelUp.Customer.Infrastructure.EFCore.Postgres;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Quartz;
using Quartz.Impl.AdoJobStore;

namespace HotelUp.Customer.Infrastructure.Quartz;

public static class Extensions
{
    public static IServiceCollection AddQuartz(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresOptions = configuration.GetSection("Postgres").Get<PostgresOptions>()
            ?? throw new NullReferenceException("Postgres options not found in appsettings.json");
        
        services.AddQuartz(q =>
        {
            q.SchedulerName = "CustomerScheduler";
            q.UsePersistentStore(x =>
            {
                x.UseProperties = true;
                x.UsePostgres(config =>
                {
                    config.UseDriverDelegate<PostgreSQLDelegate>();
                    config.ConnectionString = postgresOptions.ConnectionString;
                    config.TablePrefix = $"{postgresOptions.SchemaName}.QRTZ_";
                });
                x.UseSystemTextJsonSerializer();
                x.PerformSchemaValidation = false;
            });
        });
        
        services.AddQuartzHostedService(q =>
        {
            q.AwaitApplicationStarted = true;
            q.WaitForJobsToComplete = true;
        });
        return services;
    }
}