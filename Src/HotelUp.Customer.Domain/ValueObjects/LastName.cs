using HotelUp.Customer.Domain.ValueObjects.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects;

public record LastName
{
    public string Value { get; }

    public LastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidLastNameException(value);
        }

        Value = value;
    }

    public static implicit operator string(LastName lastName) => lastName.Value;

    public static implicit operator LastName(string lastName) => new(lastName);
}