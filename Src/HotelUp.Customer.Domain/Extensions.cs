using HotelUp.Customer.Domain.Factories;
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

        services.AddScoped<IRoomRepository, MockRoomRepository>(); //REMOVE THIS IN THE FUTURE
        return services;
    }
    
    //Repositories should be implemented in infrastructure layer

    private static IServiceCollection AddFactories(this IServiceCollection services)
    {
        // Register factories here
        // Factories can depend on repositories
        services.AddOptions<HotelDayOptions>()
            .BindConfiguration("HotelDay")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddScoped<IHotelDayFactory, HotelDayFactory>();
        services.AddScoped<IReservationFactory, ReservationFactory>();
        services.AddScoped<IRoomFactory, RoomFactory>();
        return services;
    }
    
    private static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        // Register domain services here
        // Domain services can depend on repositories
        return services;
    }
}