using HotelUp.Customer.Application.Queries;
using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Application.Queries.DTOs;
using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.Queries.Handlers;

public class GetFreeRoomsHandler : IQueryHandler<GetFreeRooms, IEnumerable<RoomDto>>
{
    private readonly ReadDbContext _context;
    public GetFreeRoomsHandler(ReadDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<RoomDto>> HandleAsync(GetFreeRooms query)
    {
        var roomsQuery = _context.Rooms
            .AsNoTracking()
            .Except(_context.Reservations
                .AsNoTracking()
                .Where(r => r.Status == ReservationStatus.Valid)
                .Where(r => !(r.Period.To < query.StartDate || r.Period.From > query.EndDate))
                .SelectMany(r => r.Rooms))
            .AsQueryable();
        if (query.RoomType is not null)
        {
            roomsQuery = roomsQuery.Where(r => r.Type == query.RoomType);
        }
        if (query.RoomCapacity is not null)
        {
            roomsQuery = roomsQuery.Where(r => r.Capacity == query.RoomCapacity);
        }
        return (await roomsQuery.ToListAsync()).Select(x => new RoomDto(x));
    }
}