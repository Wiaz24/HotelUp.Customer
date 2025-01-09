using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.Factories.Exceptions;

public class SomeRoomsAreUnavailableException : BusinessRuleException
{
    public SomeRoomsAreUnavailableException(IEnumerable<int> unavailableRoomNumbers) 
        : base($"Some rooms are unavailable: {string.Join(", ", unavailableRoomNumbers)}")
    {
    }
}