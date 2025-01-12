using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Factories.Abstractions;
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
    private readonly IHotelDayFactory _hotelDayFactory;
    private readonly ITenantCleanerService _tenantCleanerService;
    public ReservationFactory(IEnumerable<IRoomPricePolicy> roomPricePolicies, 
        ITenantPricePolicy tenantPricePolicy, IRoomRepository roomRepository, 
        IHotelDayFactory hotelDayFactory, ITenantCleanerService tenantCleanerService)
    {
        _roomPricePolicies = roomPricePolicies;
        _tenantPricePolicy = tenantPricePolicy;
        _roomRepository = roomRepository;
        _hotelDayFactory = hotelDayFactory;
        _tenantCleanerService = tenantCleanerService;
    }
    public async Task<Reservation> Create(Client client, List<int> roomNumbers, 
        List<TenantData> tenantsData, DateOnly startDate, DateOnly endDate)
    {
        var hotelDay = _hotelDayFactory.Create();
        var period = new ReservationPeriod(startDate, endDate, hotelDay);

        var availableRooms = (await _roomRepository.GetAvailableRoomsAsync(period)).ToList();

        var availableRoomNumbers = availableRooms.Select(room => room.Number).ToList();
        
        if (roomNumbers is null || !roomNumbers.Any())
        {
            throw new CannotCreateReservationWithoutRoomsException();
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
        if (tenantsData.Count > totalRoomsCapacity)
        {
            throw new NotEnoughRoomSpaceException(totalRoomsCapacity, tenantsData.Count);
        }
        if (tenantsData.Any() is false)
        {
            throw new CannotCreateReservationWithoutTenantsException();
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
        var tenantPricePolicyData = new TenantPricePolicyData(accommodationPrice, tenantsData.Count);
        if (_tenantPricePolicy.IsApplicable(tenantPricePolicyData))
        {
            accommodationPrice = _tenantPricePolicy.CalculateAccomodationPrice(tenantPricePolicyData);
        }
        var bill = new Bill(accommodationPrice);
        
        var tenants = tenantsData.Select(tenantData => new Tenant(tenantData));
        var reservation = new Reservation(client, period, tenants, rooms, bill);
        await _tenantCleanerService.EnqueueForAnonymizationAsync(reservation.Id, 
            reservation.Period.To + TimeSpan.FromDays(30));
        
        return reservation;
    }
}