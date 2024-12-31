using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using MassTransit;

namespace HotelUp.Customer.Domain.ValueObjects;

public record FirstName
{
    public string Value { get; }
    
    public FirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidFirstNameException(value);
        }
        Value = value;
    }
    
    public static implicit operator string(FirstName firstName) => firstName.Value;
    public static implicit operator FirstName(string firstName) => new(firstName);
}