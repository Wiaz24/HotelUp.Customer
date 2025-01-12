using HotelUp.Customer.Application.Commands.Abstractions;

namespace HotelUp.Customer.Application.Commands;

public record AnonymizeReservationTenantsData(Guid ReservationId) : ICommand;