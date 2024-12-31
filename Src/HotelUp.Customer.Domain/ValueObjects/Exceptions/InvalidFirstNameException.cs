using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects.Exceptions;

public class InvalidFirstNameException : AppException
{
    public InvalidFirstNameException(string name) : base($"Invalid first name: {name}")
    {
    }
}