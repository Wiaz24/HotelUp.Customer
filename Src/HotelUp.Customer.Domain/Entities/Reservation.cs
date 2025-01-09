using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities.Abstractions;
using HotelUp.Customer.Domain.Entities.Exceptions;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Entities;

public class Reservation : AggregateRoot<Guid>
{
    public Client Client { get; private set; } = null!;
    public ReservationStatus Status { get; private set; }
    public ReservationPeriod Period { get; private set; } = null!;

    private List<Tenant> _tenants = new();
    public IEnumerable<Tenant> Tenants => _tenants;
    
    private List<Room> _rooms = new();
    public IEnumerable<Room> Rooms => _rooms;
    public Bill? Bill { get; private set; }
    
    internal Reservation(Client client, ReservationPeriod period, 
        IEnumerable<Tenant> tenants, IEnumerable<Room> rooms, Bill bill)
    {
        Id = Guid.NewGuid();
        Client = client;
        Period = period;
        _tenants.AddRange(tenants);
        _rooms.AddRange(rooms);
        Status = ReservationStatus.Valid;
        Bill = bill;
    }
    
    public void Cancel()
    {
        if (Status == ReservationStatus.Canceled)
        {
            return; // Idempotency
        }
        if (DateTime.Now > Period.From - TimeSpan.FromHours(24))
        {
            throw new ReservationCannotBeCanceledException();
        }
        Status = ReservationStatus.Canceled;
        Bill = null;
    }
    
    private Reservation()
    {
    }
}