using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Application.Queries.DTOs;

public record ReservationDto
{
    public Guid Id { get; init; }
    public ReservationStatus Status { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public List<RoomDto> Rooms { get; init; }
    public BillDto? Bill { get; init; }
    public List<TenantDto> Tenants { get; init; }
    
    public ReservationDto(Reservation reservation)
    {
        Id = reservation.Id;
        Status = reservation.Status;
        StartDate = reservation.Period.From;
        EndDate = reservation.Period.To;
        Rooms = reservation.Rooms
            .Select(x => new RoomDto(x))
            .ToList();
        Bill = reservation.Bill is not null
            ? new BillDto(reservation.Bill)
            : null;
        Tenants = reservation.Tenants
            .Select(x => new TenantDto(x))
            .ToList();
    }
}
    
    