using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Domain.Factories.Abstractions;

public interface IReservationFactory
{
    Task<Reservation> Create(Client client, List<int> roomNumbers,
        List<TenantData> tenants, DateOnly startDate, DateOnly endDate);
}