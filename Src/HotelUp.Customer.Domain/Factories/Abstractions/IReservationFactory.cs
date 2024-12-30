using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Factories.Abstractions;

public interface IReservationFactory
{
    public Reservation Create(Client client, IEnumerable<int> roomNumbers, 
        IEnumerable<Tenant> tenants, ReservationPeriod period);
}