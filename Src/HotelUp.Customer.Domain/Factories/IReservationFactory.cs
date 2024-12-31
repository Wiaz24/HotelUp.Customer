using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Factories;

public interface IReservationFactory
{
    Task<Reservation> Create(Client client, List<int> roomNumbers,
        List<TenantData> tenants, DateOnly startDate, DateOnly endDate);
}