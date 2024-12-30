using HotelUp.Customer.Domain.Entities.Abstractions;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Entities;

public class AdditionalCost : Entity<Guid>
{
    public Guid TaskId => Id;
    public Money Price { get; private set; }

    public AdditionalCost(Guid taskId, Money price)
    {
        Id = taskId;
        Price = price;
    }
}