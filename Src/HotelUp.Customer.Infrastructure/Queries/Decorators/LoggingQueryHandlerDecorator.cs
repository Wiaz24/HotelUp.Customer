using HotelUp.Customer.Application.Queries.Abstractions;
using Microsoft.Extensions.Logging;

namespace HotelUp.Customer.Infrastructure.Queries.Decorators;

internal sealed class LoggingQueryHandlerDecorator<TQuery> : IQueryHandler<TQuery> where TQuery : class, IQuery
{
    private readonly IQueryHandler<TQuery> _queryHandler;
    private readonly ILogger<LoggingQueryHandlerDecorator<TQuery>> _logger;

    public LoggingQueryHandlerDecorator(IQueryHandler<TQuery> queryHandler,
        ILogger<LoggingQueryHandlerDecorator<TQuery>> logger)
    {
        _queryHandler = queryHandler;
        _logger = logger;
    }

    public async Task HandleAsync(TQuery query)
    {
        var queryName = typeof(TQuery).Name;
        _logger.LogInformation("Started handling a query: {queryName}...", queryName);
        await _queryHandler.HandleAsync(query);
        _logger.LogInformation("Completed handling a command: {queryName}.", queryName);
    }
}

internal sealed class LoggingQueryHandlerDecorator<TQuery, TResult>
    : IQueryHandler<TQuery, TResult> where TQuery : class, IQuery<TResult>
{
    private readonly IQueryHandler<TQuery, TResult> _queryHandler;
    private readonly ILogger<LoggingQueryHandlerDecorator<TQuery, TResult>> _logger;

    public LoggingQueryHandlerDecorator(IQueryHandler<TQuery, TResult> queryHandler,
        ILogger<LoggingQueryHandlerDecorator<TQuery, TResult>> logger)
    {
        _queryHandler = queryHandler;
        _logger = logger;
    }

    public async Task<TResult> HandleAsync(TQuery query)
    {
        var queryName = typeof(TQuery).Name;
        _logger.LogInformation("Started handling a query: {queryName}...", queryName);
        var result = await _queryHandler.HandleAsync(query);
        _logger.LogInformation("Completed handling a command: {queryName}.", queryName);
        return result;
    }
}