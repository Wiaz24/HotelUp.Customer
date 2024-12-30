using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects.Exceptions;

public class InvalidRoomFloorException : AppException
{
    public InvalidRoomFloorException(int floor)
        : base($"Invalid room floor: {floor}. Room floor must be between 0 and 100.")
    {
    }
}