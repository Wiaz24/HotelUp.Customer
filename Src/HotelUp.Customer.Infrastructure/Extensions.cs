using HotelUp.Customer.Infrastructure.EF;
using HotelUp.Customer.Infrastructure.EF.Health;
using HotelUp.Customer.Infrastructure.Queries;
using HotelUp.Customer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddQueryHandlers();
        services.AddRepositories();
        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration configuration)
    {
        return app;
    }
}