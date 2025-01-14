using HotelUp.Customer.Application.Queries.DTOs;

namespace HotelUp.Customer.Application.Events;

public record RoomCreatedEvent(RoomDto RoomDto);