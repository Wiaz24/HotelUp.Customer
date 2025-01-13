using HotelUp.Customer.Application.Queries.DTOs;
using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Application.Events;

public record ReservationCreatedEvent(ReservationDto ReservationDto);