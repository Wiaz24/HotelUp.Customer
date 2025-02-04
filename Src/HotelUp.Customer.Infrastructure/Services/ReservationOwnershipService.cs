using HotelUp.Customer.Application.ApplicationServices;
using HotelUp.Customer.Infrastructure.EFCore.Contexts;

using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.Services;

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