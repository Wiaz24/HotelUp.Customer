using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects.Exceptions;

public class InvalidImageUrlException : AppException
{
    public InvalidImageUrlException(string message) : base($"Image URL is invalid: {message}") { } 
}