using System.Reflection;
using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Application.Commands.Decorators;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Application.Commands;

internal static class Extensions
{
    internal static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
        services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
       
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    } 
}