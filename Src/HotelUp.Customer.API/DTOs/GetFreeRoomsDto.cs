using System.ComponentModel;
using HotelUp.Customer.Domain.Consts;

namespace HotelUp.Customer.API.DTOs;

public record GetFreeRoomsDto
{
    [DefaultValue(typeof(DateTime), "2025-01-01")]
    public required DateTime StartDate { get; init; }
    
    [DefaultValue(typeof(DateTime), "2025-01-02")]
    public required DateTime EndDate { get; init; }
    
    public RoomType? RoomType {get; init;}
    
    public int? RoomCapacity {get; init;}
}