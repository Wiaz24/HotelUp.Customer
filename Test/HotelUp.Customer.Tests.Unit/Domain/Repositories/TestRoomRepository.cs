using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Repositories;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Unit.Domain.Repositories;

internal class TestRoomRepository : IRoomRepository
{
    // public Dictionary<Guid, Reservation> Reservations = new();
    public readonly Dictionary<int, Room> Rooms = new();
    public Task<IEnumerable<Room>> GetAvailableRoomsAsync(ReservationPeriod period)
    {
        throw new NotImplementedException();
    }

    public Task<Room?> GetRoomAsync(int number)
    {
        return Task.FromResult(Rooms.TryGetValue(number, out var room) ? room : null);
    }
}