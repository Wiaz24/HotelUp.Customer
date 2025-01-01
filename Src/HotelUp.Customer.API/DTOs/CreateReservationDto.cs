using HotelUp.Customer.Domain.Consts;

namespace HotelUp.Customer.API.DTOs;

public record CreateReservationDto(int[] RoomNumbers, TenantData[] TenantsData, DateOnly StartDate, DateOnly EndDate);