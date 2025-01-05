using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Infrastructure.EF.Models;

public class AdditionalCostReadModel
{
    public required Guid Id { get; set; }
    public required Money Price { get; set; }
}