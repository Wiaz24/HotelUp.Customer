using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Repositories;
using HotelUp.Customer.Infrastructure.EFCore.Contexts;

using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.Repositories;

public class PostgresReservationRepository : IReservationRepository
{
    private readonly DbSet<Reservation> _reservations;
    private readonly WriteDbContext _dbContext;

    public PostgresReservationRepository(WriteDbContext dbContext)
    {
        _reservations = dbContext.Set<Reservation>();
        _dbContext = dbContext;
    }

    public Task<Reservation?> GetAsync(Guid id)
    {
        return _reservations
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(Reservation reservation)
    {
        await _reservations.AddAsync(reservation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Reservation reservation)
    {
        _reservations.Update(reservation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Reservation reservation)
    {
        _reservations.Remove(reservation);
        await _dbContext.SaveChangesAsync();
    }
}