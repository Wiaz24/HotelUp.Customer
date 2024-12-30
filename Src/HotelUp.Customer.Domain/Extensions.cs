using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFactories(configuration);
        services.AddDomainServices(configuration);
        return services;
    }
    
    //Repositories should be implemented in infrastructure layer

    private static IServiceCollection AddFactories(this IServiceCollection services, IConfiguration configuration)
    {
        // Register factories here
        // Factories can depend on repositories
        
        return services;
    }
    
    private static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register domain services here
        // Domain services can depend on repositories
        
        return services;
    }
}