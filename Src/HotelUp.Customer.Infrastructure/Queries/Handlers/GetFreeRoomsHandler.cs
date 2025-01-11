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
        var startDate = DateTime.SpecifyKind(query.StartDate, DateTimeKind.Utc);
        var endDate = DateTime.SpecifyKind(query.EndDate, DateTimeKind.Utc);
        var roomsQuery = _context.Rooms
            .Except(_context.Reservations
                .Where(r => r.Status == ReservationStatus.Valid)
                .Where(r => !(r.Period.To < startDate || r.Period.From > endDate))
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