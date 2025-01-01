using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Repositories;
using HotelUp.Customer.Domain.ValueObjects;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.Repositories;

public class PostgresRoomRepository : IRoomRepository
{
    private readonly DbSet<Room> _rooms;
    private readonly DbSet<Reservation> _reservations;

    public PostgresRoomRepository(WriteDbContext dbContext)
    {
        _rooms = dbContext.Set<Room>();
        _reservations = dbContext.Set<Reservation>();
    }

    public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(ReservationPeriod period)
    {
        var occupiedRooms = _reservations
            .Where(r => r.Period.PartiallyOverlapsWith(period))
            .SelectMany(r => r.Rooms);
        return await _rooms.Except(occupiedRooms).ToListAsync();
    }

    public Task<Room?> GetRoomAsync(int number)
    {
        return _rooms.FirstOrDefaultAsync(r => r.Id == number);
    }
}