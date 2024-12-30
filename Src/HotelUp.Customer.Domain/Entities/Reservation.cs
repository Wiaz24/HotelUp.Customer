using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities.Abstractions;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Entities;

public class Reservation : AggregateRoot<Guid>
{
    public Client Client { get; private set; }
    public ReservationStatus Status { get; private set; }
    public HotelDay HotelDay { get; private set; }
    public ReservationPeriod Period { get; private set; }
    
    private List<Tenant> _tenants = new();
    public IEnumerable<Tenant> Tenants => _tenants;
    
    public Bill? Bill { get; private set; }
}