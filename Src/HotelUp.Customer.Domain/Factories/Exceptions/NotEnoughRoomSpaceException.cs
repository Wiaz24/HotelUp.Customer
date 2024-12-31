using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.Factories.Exceptions;

public class NotEnoughRoomSpaceException : AppException
{
    public NotEnoughRoomSpaceException(int totalRoomsCapacity, int numTenants) 
        : base($"Not enough room space. Total rooms capacity: {totalRoomsCapacity}, number of tenants: {numTenants}")
    {
    }
}