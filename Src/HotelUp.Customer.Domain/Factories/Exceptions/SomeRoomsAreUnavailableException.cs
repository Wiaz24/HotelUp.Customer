using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.Factories.Exceptions;

public class SomeRoomsAreUnavailableException : AppException
{
    public SomeRoomsAreUnavailableException(IEnumerable<int> unavailableRoomNumbers) 
        : base($"Some rooms are unavailable: {string.Join(", ", unavailableRoomNumbers)}")
    {
    }
}