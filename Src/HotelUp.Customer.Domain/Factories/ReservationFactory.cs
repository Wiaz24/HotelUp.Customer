using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Factories.Exceptions;
using HotelUp.Customer.Domain.Policies.RoomPricePolicy;
using HotelUp.Customer.Domain.Policies.TenantPricePolicy;
using HotelUp.Customer.Domain.Repositories;
using HotelUp.Customer.Domain.Services;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Factories;

public sealed class ReservationFactory : IReservationFactory
{
    private readonly IEnumerable<IRoomPricePolicy> _roomPricePolicies;
    private readonly ITenantPricePolicy _tenantPricePolicy;
    private readonly IRoomRepository _roomRepository;
    private readonly IHotelHourService _hotelHourService;
    public ReservationFactory(IEnumerable<IRoomPricePolicy> roomPricePolicies, 
        ITenantPricePolicy tenantPricePolicy, IRoomRepository roomRepository, IHotelHourService hotelHourService)
    {
        _roomPricePolicies = roomPricePolicies;
        _tenantPricePolicy = tenantPricePolicy;
        _roomRepository = roomRepository;
        _hotelHourService = hotelHourService;
    }
    public async Task<Reservation> Create(Client client, List<int> roomNumbers, 
        List<Tenant> tenants, DateOnly startDate, DateOnly endDate)
    {
        var hotelDay = _hotelHourService.GetCurrentHotelDay();
        var period = new ReservationPeriod(startDate, endDate, hotelDay);
        
        var availableRooms = (await _roomRepository.GetAvailableRoomsAsync(period)).ToList();
        if (availableRooms is null)
        {
            throw new NoAvailableRoomsException();
        }
        var availableRoomNumbers = availableRooms.Select(room => room.Number).ToList();
        
        if (roomNumbers is null || !roomNumbers.Any())
        {
            throw new CannotCreateReservationWithoutRooms();
        }

        var invalidRoomNumbers = roomNumbers
            .Where(roomNumber => !availableRoomNumbers.Contains(roomNumber))
            .ToList();
        
        if (invalidRoomNumbers.Any())
        {
            throw new SomeRoomsAreUnavailableException(invalidRoomNumbers);
        }
        
        var rooms = availableRooms.Where(room => roomNumbers.Contains(room.Number)).ToList();

        var totalRoomsCapacity = rooms.Sum(x => x.Capacity);
        if (tenants.Count > totalRoomsCapacity)
        {
            throw new NotEnoughRoomSpaceException(totalRoomsCapacity, tenants.Count);
        }

        Money accommodationPrice = new Money(0);
        foreach (var room in rooms)
        {
            var roomPricePolicyData = new RoomPricePolicyData(room);
            var roomPrice = _roomPricePolicies
                .Where(policy => policy.IsApplicable(roomPricePolicyData))
                .Select(policy => policy.CalculateRoomPrice(roomPricePolicyData))
                .Sum(x => x);
            accommodationPrice += roomPrice;
        }
        var tenantPricePolicyData = new TenantPricePolicyData(accommodationPrice, tenants.Count);
        if (_tenantPricePolicy.IsApplicable(tenantPricePolicyData))
        {
            accommodationPrice = _tenantPricePolicy.CalculateAccomodationPrice(tenantPricePolicyData);
        }
        var bill = new Bill(accommodationPrice);
        
        var reservation = new Reservation(client, hotelDay, period, tenants, rooms, bill);
        return reservation;
    }
}