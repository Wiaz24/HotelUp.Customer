using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Factories;
using HotelUp.Customer.Domain.Factories.Abstractions;
using HotelUp.Customer.Domain.Repositories;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Tests.Shared.Utils.Domain.Repositories;

public class InMemoryRoomRepository : IRoomRepository
{
    // public Dictionary<Guid, Reservation> Reservations = new();
    public readonly List<Reservation> Reservations = new();
    public readonly Dictionary<int, Room> Rooms = new();
    private readonly ImageUrl _exampleImageUrl = new("http://www.yourserver.com/logo.png");
    private readonly IRoomFactory _roomFactory;

    public InMemoryRoomRepository()
    {
        _roomFactory = new RoomFactory(this);
    }

    public async Task CreateSampleRooms(int numOfRooms, int roomCapacity = 2)
    {
        for (int i = 1; i <= numOfRooms; i++)
        {
            var room = await _roomFactory.Create(i, roomCapacity, 2,
                false, RoomType.Basic, _exampleImageUrl);
            Rooms.Add(room.Number, room);
        }
    }
    public Task<IEnumerable<Room>> GetAvailableRoomsAsync(ReservationPeriod period)
    {
        var usedRooms = Reservations.Where(x => x.Period.PartiallyOverlapsWith(period))
            .SelectMany(x => x.Rooms);
        var availableRooms = Rooms.Values.Except(usedRooms);
        return Task.FromResult(availableRooms);
    }

    public Task<Room?> GetRoomAsync(int number)
    {
        return Task.FromResult(Rooms.TryGetValue(number, out var room) ? room : null);
    }

    public Task AddAsync(Room room)
    {
        Rooms.Add(room.Number, room);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Room room)
    {
        Rooms[room.Number] = room;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Room room)
    {
        Rooms.Remove(room.Number);
        return Task.CompletedTask;
    }
}