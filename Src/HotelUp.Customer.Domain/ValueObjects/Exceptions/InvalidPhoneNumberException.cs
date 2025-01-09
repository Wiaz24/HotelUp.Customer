using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects.Exceptions;

public class InvalidPhoneNumberException : BusinessRuleException
{
    public InvalidPhoneNumberException(string? message) : base($"Phone number is invalid. {message}")
    {
    }
}