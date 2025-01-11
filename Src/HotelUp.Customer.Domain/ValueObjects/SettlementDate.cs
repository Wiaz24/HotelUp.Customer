using HotelUp.Customer.Domain.ValueObjects.Abstractions;
using HotelUp.Customer.Shared.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Customer.Domain.ValueObjects;

public record SettlementDate : IValueObject
{
    public DateTime Value { get; private init; }

    private SettlementDate(DateTime value)
    {
        value = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        if (value > DateTime.UtcNow)
        {
            throw new InvalidSettlementDateException();
        }
        Value = value;
    }
    public static implicit operator DateTime(SettlementDate valueObject) => valueObject.Value;
    public static implicit operator SettlementDate(DateTime value) => new(value);

    public static ValueConverter GetValueConverter()
    {
        return new ValueConverter<SettlementDate, DateTime>(
            v => v.Value,
            v => new SettlementDate(v));
    }
}

public class InvalidSettlementDateException : BusinessRuleException
{
    public InvalidSettlementDateException() : base($"Settlement date cannot be in the future.")
    {
    }
}