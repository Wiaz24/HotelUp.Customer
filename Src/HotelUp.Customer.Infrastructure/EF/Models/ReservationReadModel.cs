using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Infrastructure.EF.Models;

public class ReservationReadModel
{
    public required Guid Id { get; set; }
    public required ClientReadModel Client { get; set; }
    public required ReservationStatus Status { get; set; }
    public required ReservationPeriod Period { get; set; }
    public required IEnumerable<TenantReadModel> Tenants { get; set; }
    public required IEnumerable<RoomReadModel> Rooms { get; set; }
    public BillReadModel? Bill { get; set; }
}