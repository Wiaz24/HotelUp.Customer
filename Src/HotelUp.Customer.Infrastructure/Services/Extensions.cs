using HotelUp.Customer.Domain.Services;

using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Infrastructure.Services;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITenantCleanerService, QuartzTenantCleanerService>();
        return services;
    }
}