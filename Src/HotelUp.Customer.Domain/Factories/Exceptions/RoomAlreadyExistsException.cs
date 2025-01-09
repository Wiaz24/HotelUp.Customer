using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.Factories.Exceptions;

public class RoomAlreadyExistsException : BusinessRuleException
{
    public RoomAlreadyExistsException(int number) : base($"Room with number {number} already exists.")
    {
    }
}