using HotelUp.Customer.Application.Queries.DTOs;
using HotelUp.Customer.Domain.Consts;

namespace HotelUp.Customer.Infrastructure.EF.Models;

public class RoomReadModel
{
    public required int Id { get; set; }
    public required int Capacity { get; set; }
    public required int Floor { get; set; }
    public required bool WithSpecialNeeds { get; set; }
    public required RoomType Type { get; set; }
    public required string ImageUrl { get; set; }
    
    public RoomDto ToDto()
    {
        return new RoomDto
        {
            Id = Id,
            Capacity = Capacity,
            Floor = Floor,
            WithSpecialNeeds = WithSpecialNeeds,
            Type = Type,
            ImageUrl = ImageUrl
        };
    }
}