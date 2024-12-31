using HotelUp.Customer.Domain.Entities.Abstractions;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Entities;

public class Payment : Entity<Guid>
{
    public Money Amount { get; private set; }
    public DateTime SettlementDate { get; private set; }
    internal Payment(Money amount, DateTime settlementDate)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        SettlementDate = settlementDate;
    }
}