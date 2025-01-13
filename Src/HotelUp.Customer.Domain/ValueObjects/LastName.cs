using HotelUp.Customer.Domain.ValueObjects.Abstractions;
using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Customer.Domain.ValueObjects;

public record LastName : IValueObject
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
    public static ValueConverter GetValueConverter()
    {
        return new ValueConverter<LastName, string>(
            vo => vo.Value,
            value => new LastName(value));
    }
}