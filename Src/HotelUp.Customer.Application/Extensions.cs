using HotelUp.Customer.Application.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCommandHandlers(); // Auto-registered command handlers
        services.AddApplicationServices(configuration);
        // here could be scanning for policies
        return services;
    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register application services here

        return services;
    }
}