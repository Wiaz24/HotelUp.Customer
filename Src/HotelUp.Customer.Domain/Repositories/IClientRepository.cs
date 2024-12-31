using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Domain.Repositories;

public interface IClientRepository
{
    Task<Client?> GetAsync(Guid id);
}

public class MockClientRepository : IClientRepository
{
    public Task<Client?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}