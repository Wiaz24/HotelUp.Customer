using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Factories;
using HotelUp.Customer.Tests.Shared.Utils.Domain.Repositories;

namespace HotelUp.Customer.Tests.Shared.Utils;

public static class ClientGenerator
{
    public static Task<Client> GenerateSampleClient(Guid id)
    {
        var clientRepository = new InMemoryClientRepository();
        var clientFactory = new ClientFactory(clientRepository);
        return clientFactory.Create(id);
    }
}