using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Factories.Abstractions;
using HotelUp.Customer.Domain.Factories.Exceptions;
using HotelUp.Customer.Domain.Repositories;

namespace HotelUp.Customer.Domain.Factories;

public class ClientFactory : IClientFactory
{
    private readonly IClientRepository _clientRepository;

    public ClientFactory(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Client> Create(Guid id)
    {
        var existingClient = await _clientRepository.GetAsync(id);
        if (existingClient is not null)
        {
            throw new ClientAlreadyExistsException(id);
        }
        return new Client(id);
    }
}