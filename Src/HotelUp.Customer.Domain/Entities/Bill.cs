using HotelUp.Customer.Domain.Entities.Abstractions;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Entities;

public class Bill : Entity<Guid>
{
    public Money AccomodationPrice { get; private set; }
    
    private List<AdditionalCost> _additionalCosts = new();
    public IEnumerable<AdditionalCost> AdditionalCosts => _additionalCosts;
    
    private List<Payment> _payments = new();
    public IEnumerable<Payment> Payments => _payments;
    
    public Bill(Guid id, Money accomodationPrice, 
        IEnumerable<AdditionalCost> additionalCosts, IEnumerable<Payment> payments)
    {
        Id = id;
        AccomodationPrice = accomodationPrice;
        foreach (var cost in additionalCosts)
        {
            AddAdditionalCost(cost);
        }
        foreach (var payment in payments)
        {
            AddPayment(payment);
        }
    }
    
    public void AddAdditionalCost(AdditionalCost additionalCost)
    {
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
    
}