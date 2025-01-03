using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Repositories;

namespace HotelUp.Customer.Tests.Shared.Utils.Domain.Repositories;

public class InMemoryClientRepository : IClientRepository
{
    public readonly Dictionary<Guid, Client> Clients = new();

    public Task<Client?> GetAsync(Guid id)
    {
        return Task.FromResult(Clients.TryGetValue(id, out var client) ? client : null);
    }

    public Task AddAsync(Client client)
    {
        Clients[client.Id] = client;
        return Task.CompletedTask;
    }
}