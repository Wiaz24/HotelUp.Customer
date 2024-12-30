using HotelUp.Customer.Domain.ValueObjects.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects;

public record RoomFloor
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
}