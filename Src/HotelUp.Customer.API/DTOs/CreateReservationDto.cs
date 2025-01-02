using HotelUp.Customer.Domain.Consts;

namespace HotelUp.Customer.API.DTOs;

public record CreateReservationDto
{
    public int[] RoomNumbers { get; init; } = Array.Empty<int>();
    public TenantData[] TenantsData { get; init; } = Array.Empty<TenantData>();
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}