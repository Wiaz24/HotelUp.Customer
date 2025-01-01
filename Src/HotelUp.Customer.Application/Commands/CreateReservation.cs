using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Application.Commands;

public record CreateReservation(
    Guid ClientId, 
    IEnumerable<int> RoomNumbers, 
    IEnumerable<TenantData> TenantsData,
    DateOnly StartDate,
    DateOnly EndDate) 
    : ICommand;