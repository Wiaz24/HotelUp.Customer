using HotelUp.Customer.Application.Queries.DTOs;

namespace HotelUp.Customer.Application.Events;

public record ReservationCreatedEvent
{
    public Guid ReservationId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public required List<RoomDto> Rooms { get; init; }
}