namespace HotelUp.Customer.Application.Commands.Abstractions;

public interface ICommandDispatcher
{
    Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand;

    Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command) where TCommand : class, ICommand<TResult>;
}