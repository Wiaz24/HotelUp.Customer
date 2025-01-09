using HotelUp.Customer.Application.Commands.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Application.Commands;

public class InMemoryCommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryCommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand
    {
        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command);
    }

    public async Task<TResult> DispatchAsync<TResult>(ICommand<TResult> command)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        var method = handlerType.GetMethod(nameof(ICommandHandler<ICommand<TResult>, TResult>.HandleAsync)) 
                     ?? throw new NullReferenceException("Handler does not have HandleAsync method");
        
        return await (Task<TResult>) method.Invoke(handler, [command])!;
    }
}