using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Application.Queries.DTOs;

public record RoomDto
{
    public int Id { get; init; }
    public int Capacity { get; init; }
    public int Floor { get; init; }
    public bool WithSpecialNeeds { get; init; }
    public RoomType Type { get; init; }
    public string ImageUrl { get; init; }

    public RoomDto(Room room)
    {
        Id = room.Id;
        Capacity = room.Capacity;
        Floor = room.Floor;
        WithSpecialNeeds = room.WithSpecialNeeds;
        Type = room.Type;
        ImageUrl = room.ImageUrl;
    }
}