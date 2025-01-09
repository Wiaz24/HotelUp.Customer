using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects.Exceptions;

public class InvalidEmailException : BusinessRuleException
{
    public InvalidEmailException(string? message) : base($"Email is invalid. {message}")
    {
    }
}