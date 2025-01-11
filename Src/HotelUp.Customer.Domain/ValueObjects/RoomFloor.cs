using HotelUp.Customer.Domain.ValueObjects.Abstractions;
using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Customer.Domain.ValueObjects;

public record RoomFloor : IValueObject
{
    public int Value { get; }

    public RoomFloor(int floor)
    {
        if (floor < 0 || floor > 100)
        {
            throw new InvalidRoomFloorException(floor);
        }

        Value = floor;
    }
    
    public static implicit operator int(RoomFloor roomFloor) => roomFloor.Value;
    public static implicit operator RoomFloor(int value) => new(value);
    public static ValueConverter GetValueConverter()
    {
        return new ValueConverter<RoomFloor, int>(
            vo => vo.Value,
            value => new RoomFloor(value));
    }
}