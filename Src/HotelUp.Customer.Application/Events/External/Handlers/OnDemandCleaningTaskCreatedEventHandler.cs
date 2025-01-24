using HotelUp.Cleaning.Services.Events;
using HotelUp.Customer.Application.Commands;
using HotelUp.Customer.Application.Commands.Abstractions;

using MassTransit;

namespace HotelUp.Customer.Application.Events.External.Handlers;

public class OnDemandCleaningTaskCreatedEventHandler : IConsumer<OnDemandCleaningTaskCreatedEvent>
{
    private readonly ICommandDispatcher _commandDispatcher;

    public OnDemandCleaningTaskCreatedEventHandler(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    public async Task Consume(ConsumeContext<OnDemandCleaningTaskCreatedEvent> context)
    {
        var command = new AddAdditionalCost(context.Message.ReservationId, 20);
        await _commandDispatcher.DispatchAsync(command);
    }
}