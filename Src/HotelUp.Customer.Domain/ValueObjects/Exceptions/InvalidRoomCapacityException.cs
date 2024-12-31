using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects.Exceptions;

public class InvalidRoomCapacityException : AppException
{
    public InvalidRoomCapacityException(int value) : base($"Invalid room capacity: {value}. Must be between 1 and 4.")
    {
    }
}