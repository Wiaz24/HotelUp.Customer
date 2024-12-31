using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Factories;

public interface IReservationFactory
{
    Task<Reservation> Create(Client client, List<int> roomNumbers,
        List<Tenant> tenants, DateOnly startDate, DateOnly endDate);
}