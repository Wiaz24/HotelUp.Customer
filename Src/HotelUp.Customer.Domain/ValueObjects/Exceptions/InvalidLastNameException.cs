using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects.Exceptions;

public class InvalidLastNameException : BusinessRuleException
{
    public InvalidLastNameException(string value) : base($"Invalid last name: {value}")
    {
    }
}