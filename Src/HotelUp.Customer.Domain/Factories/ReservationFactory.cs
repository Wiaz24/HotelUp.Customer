using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Factories.Abstractions;
using HotelUp.Customer.Domain.Policies.RoomPricePolicy;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Factories;

public sealed class ReservationFactory : IReservationFactory
{
    private readonly IEnumerable<IRoomPricePolicy> _roomPricePolicies;
    public ReservationFactory(IEnumerable<IRoomPricePolicy> roomPricePolicies)
    {
        _roomPricePolicies = roomPricePolicies;
    }
    public Reservation Create(Client client, IEnumerable<int> roomNumbers, IEnumerable<Tenant> tenants, ReservationPeriod period)
    {
        throw new NotImplementedException();
    }
}