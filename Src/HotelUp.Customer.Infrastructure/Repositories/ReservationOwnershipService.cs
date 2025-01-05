using HotelUp.Customer.Application.ApplicationServices;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.Repositories;

public class ReservationOwnershipService : IReservationOwnershipService
{
    private readonly ReadDbContext _context;

    public ReservationOwnershipService(ReadDbContext context)
    {
        _context = context;
    }

    public Task<bool> IsReservationOwner(Guid reservationId, Guid clientId)
    {
        return _context.Reservations
            .AnyAsync(r => r.Id == reservationId && r.Client.Id == clientId);
    }
}