using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects.Exceptions;

public class InvalidEmailException : AppException
{
    public InvalidEmailException(string? message) : base($"Email is invalid. {message}")
    {
    }
}