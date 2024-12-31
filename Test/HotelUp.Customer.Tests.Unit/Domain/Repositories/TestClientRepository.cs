using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Repositories;

namespace HotelUp.Customer.Unit.Domain.Repositories;

public class TestClientRepository : IClientRepository
{
    public readonly Dictionary<Guid, Client> Clients = new();

    public Task<Client?> GetAsync(Guid id)
    {
        return Task.FromResult(Clients.TryGetValue(id, out var client) ? client : null);
    }
}