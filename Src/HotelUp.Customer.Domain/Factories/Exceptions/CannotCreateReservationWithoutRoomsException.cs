using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.Factories.Exceptions;

public class CannotCreateReservationWithoutRoomsException : AppException
{
    public CannotCreateReservationWithoutRoomsException() 
        : base("Cannot create reservation without rooms")
    {
    }
}