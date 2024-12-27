using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Infrastructure.Repositories;

internal static class Extensions
{
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Register repositories here
        return services;
    }
}