using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Application.Queries.DTOs;

namespace HotelUp.Customer.Application.Queries;

public record GetUsersReservations(Guid Id) : IQuery<IEnumerable<ReservationDto>>;