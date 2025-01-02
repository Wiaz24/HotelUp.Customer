using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Domain.Repositories;

public interface IClientRepository
{
    Task<Client?> GetAsync(Guid id);
    Task AddAsync(Client client);
}