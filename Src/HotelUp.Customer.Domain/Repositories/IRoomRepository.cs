using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Repositories;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAvailableRoomsAsync(ReservationPeriod period);
    Task<Room?> GetRoomAsync(int number);
    Task AddAsync(Room room);
    Task UpdateAsync(Room room);
    Task DeleteAsync(Room room);
}