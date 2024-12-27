namespace HotelUp.Customer.Application.Queries.Abstractions;

public interface IQueryDispatcher
{
    Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query);
}