using HotelUp.Customer.Domain.Repositories;
using MassTransit;
using IClientFactory = HotelUp.Customer.Domain.Factories.Abstractions.IClientFactory;


namespace HotelUp.Customer.Application.Events.External.Handlers;

public class UserCreatedEventHandler : IConsumer<UserCreatedEvent>
{
    private readonly IClientFactory _clientFactory;
    private readonly IClientRepository _clientRepository;

    public UserCreatedEventHandler(IClientRepository clientRepository, 
        IClientFactory clientFactory)
    {
        _clientRepository = clientRepository;
        _clientFactory = clientFactory;
    }

    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var id = context.Message.Id;
        var client = await _clientFactory.Create(id);
        await _clientRepository.AddAsync(client);
    }
}