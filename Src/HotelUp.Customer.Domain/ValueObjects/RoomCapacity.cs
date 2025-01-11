using HotelUp.Customer.Domain.ValueObjects.Abstractions;
using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Customer.Domain.ValueObjects;

public record RoomCapacity : IValueObject
{
    public int Value { get; }
    
    public RoomCapacity(int value)
    {
        if (value <= 0 || value > 4)
        {
            throw new InvalidRoomCapacityException(value);
        }
        Value = value;
    }
    public static implicit operator int(RoomCapacity roomCapacity) => roomCapacity.Value;
    public static implicit operator RoomCapacity(int value) => new(value);
    public static ValueConverter GetValueConverter()
    {
        return new ValueConverter<RoomCapacity, int>(
            vo => vo.Value,
            value => new RoomCapacity(value));
    }
}