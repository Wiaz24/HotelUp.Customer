using HotelUp.Customer.Domain.Factories.Abstractions;
using HotelUp.Customer.Domain.Factories.Options;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Domain.Factories;

internal static class Extensions
{
    internal static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddOptions<HotelDayOptions>()
            .BindConfiguration("HotelDay")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddScoped<IHotelDayFactory, HotelDayFactory>();
        services.AddScoped<IReservationFactory, ReservationFactory>();
        services.AddScoped<IRoomFactory, RoomFactory>();
        services.AddScoped<IClientFactory, ClientFactory>();
        return services;
    }
}