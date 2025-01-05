using HotelUp.Customer.Application.Queries;
using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Application.Queries.DTOs;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using HotelUp.Customer.Infrastructure.EF.Models;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.Queries.Handlers;

// public class GetFreeRoomsHandler : IQueryHandler<GetFreeRooms, IEnumerable<RoomDto>>
// {
//     // private readonly ReadDbContext _context;
//     //
//     // public GetFreeRoomsHandler(ReadDbContext context)
//     // {
//     //     _context = context;
//     // }
//     //
//     // public async Task<IEnumerable<RoomDto>> HandleAsync(GetFreeRooms query)
//     // {
//     //     var occupiedRooms = _context.Reservations
//     //         .AsNoTracking()
//     //         .Where(r => !(r.Period.To < query.StartDate || r.Period.From > query.EndDate))
//     //         .SelectMany(r => r.Rooms);
//     //     var result = await _context.Rooms
//     //         .AsNoTracking()
//     //         .Except(occupiedRooms)
//     //         .ToListAsync();
//     //     return result.Select(r => r.ToDto());
//     // }
// }