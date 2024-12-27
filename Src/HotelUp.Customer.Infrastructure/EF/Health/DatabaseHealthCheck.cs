using HotelUp.Customer.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HotelUp.Customer.Infrastructure.EF.Health;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly ReadDbContext _readDbContext;

    public DatabaseHealthCheck(ReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            await _readDbContext.Database.GetAppliedMigrationsAsync(cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception e)
        {
            return HealthCheckResult.Unhealthy(exception: e);
        }
    }
}