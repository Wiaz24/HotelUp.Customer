using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Application.Events;

public record ReservationCreatedEvent(Reservation Reservation);