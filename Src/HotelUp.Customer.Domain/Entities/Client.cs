using HotelUp.Customer.Domain.Entities.Abstractions;

namespace HotelUp.Customer.Domain.Entities;

public class Client : Entity<Guid>
{
    internal Client(Guid id)
    {
        Id = id;
    }
}