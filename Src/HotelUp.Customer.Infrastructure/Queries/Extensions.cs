using System.Reflection;
using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Application.Commands.Decorators;
using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Infrastructure.Queries.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Infrastructure.Queries;

internal static class Extensions
{
    internal static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        
        services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.TryDecorate(typeof(IQueryHandler<,>), typeof(LoggingQueryHandlerDecorator<,>));
        return services;
    } 
}