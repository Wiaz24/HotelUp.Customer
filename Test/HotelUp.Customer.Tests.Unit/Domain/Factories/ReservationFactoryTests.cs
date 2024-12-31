using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.Factories;
using HotelUp.Customer.Domain.Factories.Abstractions;
using HotelUp.Customer.Domain.Policies.RoomPricePolicy;
using HotelUp.Customer.Domain.Policies.TenantPricePolicy;
using HotelUp.Customer.Domain.ValueObjects;
using HotelUp.Customer.Unit.Domain.Repositories;
using Shouldly;

namespace HotelUp.Customer.Unit.Domain.Factories;

public class ReservationFactoryTests
{
    private readonly IEnumerable<IRoomPricePolicy> _roomPricePolicies;
    private readonly ITenantPricePolicy _tenantPricePolicy;
    private readonly IHotelDayFactory _hotelDayFactory;
    
    public ReservationFactoryTests(IEnumerable<IRoomPricePolicy> roomPricePolicies, 
        ITenantPricePolicy tenantPricePolicy, IHotelDayFactory hotelDayFactory)
    {
        _roomPricePolicies = roomPricePolicies;
        _tenantPricePolicy = tenantPricePolicy;
        _hotelDayFactory = hotelDayFactory;
    }
    
    [Fact]
    public async Task Create_IfThereAreNoTenants_ShouldThrowCannotCreateReservationWithoutTenantsException()
    {
        // Arrange
        var roomRepository = new TestRoomRepository();
        var clientRepository = new TestClientRepository();
        var reservationFactory = new ReservationFactory(_roomPricePolicies, 
            _tenantPricePolicy, roomRepository, _hotelDayFactory);
        var clientFactory = new ClientFactory(clientRepository);
        var client = await clientFactory.Create(Guid.NewGuid());

        throw new NotImplementedException();
        // clientRepository.Clients.Add(client.Id, client);
        // // Act
        // var exception = await Record.ExceptionAsync(() => reservationFactory
        //     .Create(client, 
        //         new List<int> { 1, 2 }, 
        //         new List<TenantData>(), 
        //         new DateOnly(2022, 1, 1), 
        //         new DateOnly(2022, 1, 2));
        //
        // // Assert
        // exception.ShouldNotBeNull();
        // exception.ShouldBeOfType<CannotCreateReservationWithoutTenantsException>();
    }
}