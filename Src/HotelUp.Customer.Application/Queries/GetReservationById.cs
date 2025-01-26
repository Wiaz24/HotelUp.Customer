using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Application.Queries.DTOs;

namespace HotelUp.Customer.Application.Queries;

public record GetReservationById(Guid Id) : IQuery<ReservationDto?>;
