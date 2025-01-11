using HotelUp.Customer.Domain.ValueObjects.Abstractions;
using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Customer.Domain.ValueObjects;

public record FirstName : IValueObject
{
    public string Value { get; init; }
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
    public static ValueConverter GetValueConverter()
    {
        return new ValueConverter<FirstName, string>(
            vo => vo.Value,
            value => new FirstName(value));
    }
}