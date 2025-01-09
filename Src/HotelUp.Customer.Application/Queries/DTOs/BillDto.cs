using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Application.Queries.DTOs;

public record BillDto
{
    public string AccomodationPrice { get; init; }
    public List<AdditionalCostDto> AdditionalCosts { get; init; }
    public List<PaymentDto> Payments { get; init; }

    public BillDto(Bill bill)
    {
        AccomodationPrice = bill.AccomodationPrice.ToString();
        AdditionalCosts = bill.AdditionalCosts
            .Select(x => new AdditionalCostDto(x))
            .ToList();
        Payments = bill.Payments
            .Select(x => new PaymentDto(x))
            .ToList();
    }
}