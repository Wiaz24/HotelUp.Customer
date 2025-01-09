using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.Factories.Exceptions;

public class CannotCreateReservationWithoutRoomsException : BusinessRuleException
{
    public CannotCreateReservationWithoutRoomsException() 
        : base("Cannot create reservation without rooms")
    {
    }
}