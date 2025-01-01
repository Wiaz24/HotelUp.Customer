using HotelUp.Customer.Domain.Entities.Abstractions;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Entities;

public class AdditionalCost : Entity<Guid>
{
    public Guid TaskId => Id;
    public Money Price { get; private set; }

    internal AdditionalCost(Money price)
    {
        Id = Guid.NewGuid();
        Price = price;
    }
    
    private AdditionalCost()
    {
    }
}