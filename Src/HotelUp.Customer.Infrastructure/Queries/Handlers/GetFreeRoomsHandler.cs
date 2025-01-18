using System.Text.Json;

using HotelUp.Customer.Application.Queries;
using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Application.Queries.DTOs;
using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HotelUp.Customer.Infrastructure.Queries.Handlers;

public class GetFreeRoomsHandler : IQueryHandler<GetFreeRooms, IEnumerable<RoomDto>>
{
    private readonly ReadDbContext _context;
    private readonly IMemoryCache _cache;
    public GetFreeRoomsHandler(ReadDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }
    
    public async Task<IEnumerable<RoomDto>> HandleAsync(GetFreeRooms query)
    {
        var cacheKey = JsonSerializer.Serialize(query);
        var cachedResult = _cache.Get<IEnumerable<RoomDto>>(cacheKey);

        if (cachedResult is not null)
        {
            return cachedResult;
        }
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
        var result = (await roomsQuery.ToListAsync()).Select(x => new RoomDto(x)).ToList();
        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
        return result;
    }
}