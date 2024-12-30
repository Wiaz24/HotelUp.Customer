using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects.Exceptions;

public class InvalidPeselException : AppException
{
    public InvalidPeselException(string? message) : base($"PESEL number is invalid. {message}")
    {
    }
}