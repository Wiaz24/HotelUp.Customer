namespace HotelUp.Customer.Application.Queries.Abstractions;

public interface IQueryHandler<in TQuery, TResult> where TQuery : class, IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query);
}

public interface IQueryHandler<in TQuery> where TQuery : class, IQuery
{
    Task HandleAsync(TQuery query);
}
