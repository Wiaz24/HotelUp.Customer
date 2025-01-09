using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Customer.Domain.ValueObjects;

public record FirstName
{
    public string Value { get; private init; }

    public FirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidFirstNameException(value);
        }

        Value = value;
    }

    public static implicit operator string(FirstName valueObject) => valueObject.Value;

    public static implicit operator FirstName(string value) => new(value);
}


public class FirstNameConverter : ValueConverter<FirstName, string>
{
    public FirstNameConverter() : base(
        v => v.Value,
        v => new FirstName(v))
    {
    }
}