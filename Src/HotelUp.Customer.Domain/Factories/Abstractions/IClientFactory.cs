using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Domain.Factories.Abstractions;

public interface IClientFactory
{
    Task<Client> Create(Guid id);
}