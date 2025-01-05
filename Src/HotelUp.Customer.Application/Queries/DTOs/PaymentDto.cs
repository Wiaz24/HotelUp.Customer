using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Application.Queries.DTOs;

public record PaymentDto
{
    public string Amount { get; init; }
    public DateTime SettlementDate { get; init; }

    public PaymentDto(Payment payment)
    {
        Amount = payment.Amount.ToString();
        SettlementDate = payment.SettlementDate;
    }
}