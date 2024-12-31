using HotelUp.Customer.Domain.ValueObjects;
using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Shouldly;

namespace HotelUp.Customer.Unit.Domain.ValueObjects.Room;

public class RoomFloorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(100)]
    public void WithCorrectFloor_ShouldCreateRoomFloor(int capacity)
    {
        // Act
        var exception = Record.Exception(() => new RoomFloor(capacity));

        // Assert
        exception.ShouldBeNull();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void WithIncorrectFloor_ShouldThrowInvalidRoomFloorException(int capacity)
    {
        // Act
        var exception = Record.Exception(() => new RoomFloor(capacity));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidRoomFloorException>();
    }
}