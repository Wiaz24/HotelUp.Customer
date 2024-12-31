using HotelUp.Customer.Domain.ValueObjects;
using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Shouldly;

namespace HotelUp.Customer.Unit.Domain.ValueObjects.Room;

public class RoomCapacityTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(4)]
    public void WithCorrectCapacity_ShouldCreateRoomCapacity(int capacity)
    {
        // Act
        var exception = Record.Exception(() => new RoomCapacity(capacity));

        // Assert
        exception.ShouldBeNull();
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    public void WithIncorrectCapacity_ShouldThrowException(int capacity)
    {
        // Act
        var exception = Record.Exception(() => new RoomCapacity(capacity));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidRoomCapacityException>();
    }
}