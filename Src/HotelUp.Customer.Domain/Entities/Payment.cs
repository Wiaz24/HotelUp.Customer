using HotelUp.Customer.Domain.Entities.Abstractions;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Entities;

public class Payment : Entity<Guid>
{
    public Money Amount { get; private set; } = null!;
    public SettlementDate SettlementDate { get; private set; } = null!;

    internal Payment(Money amount, SettlementDate settlementDate)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        SettlementDate = settlementDate;
    }
    
    private Payment()
    {
    }
}