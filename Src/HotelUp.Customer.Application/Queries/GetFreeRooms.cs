using HotelUp.Customer.Application.Queries.Abstractions;
using HotelUp.Customer.Application.Queries.DTOs;
using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Application.Queries;

public record GetFreeRooms(DateTime StartDate, DateTime EndDate, RoomType? RoomType, int? RoomCapacity) 
    : IQuery<IEnumerable<RoomDto>>;