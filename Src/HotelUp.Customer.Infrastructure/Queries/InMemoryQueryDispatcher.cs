using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Shared.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Infrastructure.Queries;

public class InMemoryQueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryQueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        try
        {
            var result = await (Task<TResult>)handlerType
                .GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync))?
                .Invoke(handler, new[] { query });
            return result;
        }
        catch (Exception e)
        {
            throw new DatabaseException(e.Message);
        }
    }
}