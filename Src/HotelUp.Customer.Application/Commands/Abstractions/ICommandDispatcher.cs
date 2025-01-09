namespace HotelUp.Customer.Application.Commands.Abstractions;

public interface ICommandDispatcher
{
    Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    Task<TResult> DispatchAsync<TResult>(ICommand<TResult> command);
}