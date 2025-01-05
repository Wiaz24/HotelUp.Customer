using System.ComponentModel;

namespace HotelUp.Customer.API.DTOs;

public record GetFreeRoomsDto
{
    [DefaultValue(typeof(DateTime), "2025-01-01")]
    public required DateTime StartDate { get; init; }
    
    [DefaultValue(typeof(DateTime), "2025-01-02")]
    public required DateTime EndDate { get; init; }
}