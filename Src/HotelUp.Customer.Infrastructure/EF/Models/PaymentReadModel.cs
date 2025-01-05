using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Infrastructure.EF.Models;

public class PaymentReadModel
{
    public required Guid Id { get; set; }
    public required Money Amount { get; set; }
    public required DateTime SettlementDate { get; set; }
}