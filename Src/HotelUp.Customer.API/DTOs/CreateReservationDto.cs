using System.ComponentModel;
using HotelUp.Customer.Domain.Consts;
using Swashbuckle.AspNetCore.Annotations;

namespace HotelUp.Customer.API.DTOs;

public record CreateReservationDto
{
    [DefaultValue(new int[]{1})]
    public required int[] RoomNumbers { get; init; }
    public required TenantDataDto[] TenantsData { get; init; }
    
    [DefaultValue("2025-01-01")]
    public required DateOnly StartDate { get; init; }
    
    [DefaultValue("2025-01-05")]
    public required DateOnly EndDate { get; init; }
}