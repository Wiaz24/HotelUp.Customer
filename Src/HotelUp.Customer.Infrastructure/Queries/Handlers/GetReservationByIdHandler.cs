using HotelUp.Customer.Application.Queries;
using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Application.Queries.DTOs;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.Queries.Handlers;

public class GetReservationByIdHandler : IQueryHandler<GetReservationById, ReservationDto?>
{
    private readonly ReadDbContext _context;

    public GetReservationByIdHandler(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<ReservationDto?> HandleAsync(GetReservationById query)
    {
        var reservation = await _context.Reservations
            .Include(r => r.Rooms)
            .SingleOrDefaultAsync(r => r.Client.Id == query.ClientId && r.Id == query.Id);
        return reservation is null ? null : new ReservationDto(reservation);
    }
}