using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Customer.Domain.ValueObjects;

public record LastName
{
    public string Value { get; private init; }

    public LastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidLastNameException(value);
        }

        Value = value;
    }

    public static implicit operator string(LastName valueObject) => valueObject.Value;
    public static implicit operator LastName(string value) => new(value);
}


public class LastNameConverter : ValueConverter<LastName, string>
{
    public LastNameConverter() : base(
        v => v.Value,
        v => new LastName(v))
    {
    }
}