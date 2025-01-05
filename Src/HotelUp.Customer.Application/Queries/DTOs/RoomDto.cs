using HotelUp.Customer.Domain.Consts;

namespace HotelUp.Customer.Application.Queries.DTOs;

public record RoomDto
{
    public int Id { get; init; }
    public int Capacity { get; init; }
    public int Floor { get; init; }
    public bool WithSpecialNeeds { get; init; }
    public RoomType Type { get; init; }
    public string ImageUrl { get; init; }
}