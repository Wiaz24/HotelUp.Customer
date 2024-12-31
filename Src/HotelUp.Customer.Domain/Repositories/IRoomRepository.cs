using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Repositories;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAvailableRoomsAsync(ReservationPeriod period);
    Task<Room?> GetRoomAsync(int number);
}

public class MockRoomRepository : IRoomRepository
{
    public Task<IEnumerable<Room>> GetAvailableRoomsAsync(ReservationPeriod period)
    {
        throw new NotImplementedException();
    }

    public Task<Room?> GetRoomAsync(int number)
    {
        throw new NotImplementedException();
    }
}