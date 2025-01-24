using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Repositories;
using HotelUp.Customer.Domain.ValueObjects;
using HotelUp.Customer.Infrastructure.EFCore.Contexts;

using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.Repositories;

public class PostgresRoomRepository : IRoomRepository
{
    private readonly DbSet<Room> _rooms;
    private readonly DbSet<Reservation> _reservations;
    private readonly WriteDbContext _dbContext;

    public PostgresRoomRepository(WriteDbContext dbContext)
    {
        _rooms = dbContext.Set<Room>();
        _reservations = dbContext.Set<Reservation>();
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(ReservationPeriod period)
    {
        var occupiedRooms = _reservations
            .Where(r => r.Status == ReservationStatus.Valid)
            .Where(r => !(r.Period.To < period.From || r.Period.From > period.To))
            .SelectMany(r => r.Rooms);
        return await _rooms.Except(occupiedRooms).ToListAsync();
    }

    public Task<Room?> GetRoomAsync(int number)
    {
        return _rooms.FirstOrDefaultAsync(r => r.Id == number);
    }

    public async Task AddAsync(Room room)
    {
        await _rooms.AddAsync(room);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Room room)
    {
        _rooms.Update(room);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Room room)
    {
        _rooms.Remove(room);
        await _dbContext.SaveChangesAsync();
    }
}