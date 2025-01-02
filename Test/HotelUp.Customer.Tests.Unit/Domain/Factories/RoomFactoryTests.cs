using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Factories;
using HotelUp.Customer.Domain.Factories.Exceptions;
using HotelUp.Customer.Domain.ValueObjects;
using HotelUp.Customer.Tests.Shared.Utils.Domain.Repositories;
using Shouldly;

namespace HotelUp.Customer.Unit.Domain.Factories;

public class RoomFactoryTests
{
    [Fact]
    public async Task Create_IfRoomDoesNotExists_ShouldCreateRoom()
    {
        // Arrange
        var roomRepository = new InMemoryRoomRepository();
        var roomFactory = new RoomFactory(roomRepository);
        
        // Act
        var exception = await Record.ExceptionAsync(() => roomFactory
            .Create(1, 
                2, 
                1, 
                false, 
                RoomType.Basic, 
                new ImageUrl("http://www.yourserver.com/logo.png")));
        
        // Assert
        exception.ShouldBeNull();
        
    }
    
    [Fact]
    public async Task Create_IfRoomExists_ThrowsRoomAlreadyExistsException()
    {
        // Arrange
        var roomRepository = new InMemoryRoomRepository();
        var roomFactory = new RoomFactory(roomRepository);
        var firstRoom = await roomFactory
            .Create(1,
                2,
                1,
                false,
                RoomType.Basic,
                new ImageUrl("http://www.yourserver.com/logo.png"));
        roomRepository.Rooms.Add(firstRoom.Id, firstRoom);
            
        // Act
        var exception = await Record.ExceptionAsync(() => roomFactory
            .Create(1, 
                4, 
                2, 
                true, 
                RoomType.Basic, 
                new ImageUrl("http://www.yourserver.com/otherlogo.png")));
        
        //Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<RoomAlreadyExistsException>();
    }
}