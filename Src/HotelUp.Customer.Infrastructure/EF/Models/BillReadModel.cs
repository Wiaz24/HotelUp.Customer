using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Infrastructure.EF.Models;

public class BillReadModel
{
    public required Guid Id { get; set; }
    public required Money AccomodationPrice { get; set; }
    public required ICollection<AdditionalCostReadModel> AdditionalCosts { get; set; }
    public required ICollection<PaymentReadModel> Payments { get; set; }
}