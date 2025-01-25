using HotelUp.Customer.Application.Queries;
using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Application.Queries.DTOs;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Infrastructure.EFCore.Contexts;

using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.Queries.Handlers;

public class GetUsersReservationsHandler : IQueryHandler<GetUsersReservations, IEnumerable<ReservationDto>>
{
    private readonly ReadDbContext _context;

    public GetUsersReservationsHandler(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReservationDto>> HandleAsync(GetUsersReservations query)
    {
        return await _context.Reservations
            .Where(r => r.Client.Id == query.Id)
            .Include(r => r.Rooms)
            .Select(r => new ReservationDto(r))
            .ToListAsync();
    }
}