using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.Factories.Exceptions;

public class NoAvailableRoomsException : AppException
{
    public NoAvailableRoomsException() : base("No available rooms for the given period")
    {
    }
}