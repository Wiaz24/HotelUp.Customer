using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HotelUp.Customer.Application.Commands;
using HotelUp.Customer.Domain.Consts;

namespace HotelUp.Customer.API.DTOs;

public record CreateRoomDto
{
    [DefaultValue(1)]
    public required int Number { get; init; }
    
    [DefaultValue(2)]
    public required int Capacity { get; init; }
    
    [DefaultValue(0)]
    public required int Floor { get; init; }
    
    [DefaultValue(false)]
    public required bool WithSpecialNeeds { get; init; }
    
    [EnumDataType(typeof(RoomType))]
    [DefaultValue(RoomType.Basic)]
    public required RoomType Type { get; init; }
    
    public required IFormFile Image { get; init; }
}