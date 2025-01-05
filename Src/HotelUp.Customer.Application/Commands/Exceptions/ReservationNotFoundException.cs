using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Application.Commands.Exceptions;

public class ReservationNotFoundException : NotFoundException
{
    public ReservationNotFoundException(Guid id) : base($"Reservation with id: '{id}' was not found.")
    {
    }
}