using HotelUp.Customer.Application.Queries;
using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Application.Queries.DTOs;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.Queries.Handlers;

public class GetUsersReservationsHandler : IQueryHandler<GetUsersReservations, IEnumerable<ReservationDto>>
{
    private readonly ReadDbContext _context;
    
    // private static readonly Func<ReadDbContext, Guid, Task<List<Reservation>>> GetUsersReservationsQuery = 
    //     Microsoft.EntityFrameworkCore.EF.CompileAsyncQuery(
    //     (ReadDbContext context, Guid id) => context.Reservations
    //         .AsNoTracking()
    //         .Where(r => r.Client.Id == id)
    //         .ToList());

    public GetUsersReservationsHandler(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReservationDto>> HandleAsync(GetUsersReservations query)
    {
        // var reservations = await GetUsersReservationsQuery(_context, query.Id);
        var reservations = await _context.Reservations
            .AsNoTracking()
            .Where(r => r.Client.Id == query.Id)
            .ToListAsync();
        return reservations.Select(r => new ReservationDto(r));
    }
}