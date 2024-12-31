using HotelUp.Customer.Domain.ValueObjects.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects;

public record RoomCapacity
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
}