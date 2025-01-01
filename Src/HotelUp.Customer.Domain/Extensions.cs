using HotelUp.Customer.Domain.Factories;
using HotelUp.Customer.Domain.Factories.Abstractions;
using HotelUp.Customer.Domain.Factories.Options;
using HotelUp.Customer.Domain.Policies.RoomPricePolicy;
using HotelUp.Customer.Domain.Policies.TenantPricePolicy;
using HotelUp.Customer.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFactories();
        services.AddDomainServices();
        services.AddRoomPricePolicy();
        services.AddTenantPricePolicy();
        return services;
    }
    
    //Repositories should be implemented in infrastructure layer
    private static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        // Register domain services here
        // Domain services can depend on repositories
        return services;
    }
}