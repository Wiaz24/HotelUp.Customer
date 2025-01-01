using HotelUp.Customer.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Infrastructure.Repositories;

internal static class Extensions
{
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, PostgresClientRepository>();
        services.AddScoped<IReservationRepository, PostgresReservationRepository>();
        services.AddScoped<IRoomRepository, PostgresRoomRepository>();
        return services;
    }
}