using HotelUp.Customer.Domain.Entities.Abstractions;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Entities;

public class Bill : Entity<Guid>
{
    public Money AccomodationPrice { get; private set; } = null!;

    private List<AdditionalCost> _additionalCosts = new();
    public IEnumerable<AdditionalCost> AdditionalCosts => _additionalCosts;
    
    private List<Payment> _payments = new();
    public IEnumerable<Payment> Payments => _payments;
    
    internal Bill(Money accomodationPrice)
    {
        AccomodationPrice = accomodationPrice;
        Id = Guid.NewGuid();
    }
    
    public void AddAdditionalCost(Money amount)
    {
        var additionalCost = new AdditionalCost(amount);
        _additionalCosts.Add(additionalCost);
    }
    
    public void AddPayment(Payment payment)
    {
        _payments.Add(payment);
    }
    
    public decimal GetBalance()
    {
        var additionalCostsSum = _additionalCosts.Sum(x => x.Price);
        var paymentsSum = _payments.Sum(x => x.Amount);
        return AccomodationPrice.Amount + additionalCostsSum - paymentsSum;
    }
    
    private Bill()
    {
    }
}