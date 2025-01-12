using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Factories;
using HotelUp.Customer.Tests.Shared.Utils.Domain.Repositories;

namespace HotelUp.Customer.Tests.Shared.Utils;

public static class RoomGenerator
{
    private const string ExampleImageUrl = "https://www.images.com/img.png";
    
    public static async Task<IEnumerable<Room>> GenerateSampleRooms(int count, int capacity = 1, int startNumber = 1)
    {
        var roomRepository = new InMemoryRoomRepository();
        var roomFactory = new RoomFactory(roomRepository);

        var rooms = new List<Room>();
        for (int i = 0; i < count; i++)
        {
            var room = await roomFactory.Create(i + startNumber, capacity, 1, 
                false, RoomType.Basic, ExampleImageUrl);
            rooms.Add(room);
        }
        return rooms;
    }
}