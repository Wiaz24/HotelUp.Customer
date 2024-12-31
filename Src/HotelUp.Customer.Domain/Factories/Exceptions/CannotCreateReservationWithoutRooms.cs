using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.Factories.Exceptions;

public class CannotCreateReservationWithoutRooms : AppException
{
    public CannotCreateReservationWithoutRooms() : base("Cannot create reservation without rooms")
    {
    }
}