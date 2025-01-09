using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Application.Queries.DTOs;

public record AdditionalCostDto
{
    public Guid TaskId { get; init; }
    public string Price { get; init; }

    public AdditionalCostDto(AdditionalCost additionalCost)
    {
        TaskId = additionalCost.TaskId;
        Price = additionalCost.Price.ToString();
    }
}