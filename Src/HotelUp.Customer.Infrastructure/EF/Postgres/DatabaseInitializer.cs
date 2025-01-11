using HotelUp.Customer.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace HotelUp.Customer.Infrastructure.EF.Postgres;

public class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly IConfiguration _configuration;

    public DatabaseInitializer(IServiceProvider serviceProvider, 
        ILogger<DatabaseInitializer> logger, 
        IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var shouldMigrate = _configuration.GetValue<bool>("ApplyMigrations");
        if (shouldMigrate is false)
            return;
        
        var dbContextTypes = new List<Type>
        {
            typeof(ReadDbContext),
            typeof(WriteDbContext)
        };

        using var scope = _serviceProvider.CreateScope();
        foreach (var dbContextType in dbContextTypes)
        {
            var dbContext = scope.ServiceProvider.GetService(dbContextType) as DbContext;
            if (dbContext is null)
            {
                continue;
            }
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
            if (pendingMigrations.Any())
            {
                _logger.LogInformation($"Applying migrations for DB context: {dbContext.GetType().Name}...");
                await dbContext.Database.MigrateAsync(cancellationToken);
            }
        }
        
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}