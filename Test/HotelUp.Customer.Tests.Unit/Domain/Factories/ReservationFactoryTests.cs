using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Factories;
using HotelUp.Customer.Domain.Factories.Abstractions;
using HotelUp.Customer.Domain.Factories.Exceptions;
using HotelUp.Customer.Domain.Policies.RoomPricePolicy;
using HotelUp.Customer.Domain.Policies.TenantPricePolicy;
using HotelUp.Customer.Domain.Services;
using HotelUp.Customer.Domain.ValueObjects;
using HotelUp.Customer.Infrastructure.Services;
using HotelUp.Customer.Tests.Shared.Utils;
using HotelUp.Customer.Tests.Shared.Utils.Domain.Repositories;

using NSubstitute;

using Quartz;

using Shouldly;

namespace HotelUp.Customer.Unit.Domain.Factories;

public class ReservationFactoryTests
{
    private readonly IEnumerable<IRoomPricePolicy> _roomPricePolicies;
    private readonly ITenantPricePolicy _tenantPricePolicy;
    private readonly HotelDayFactory _hotelDayFactory;
    private readonly HotelDay _hotelDay;
    private readonly ITenantCleanerService _tenantCleanerService;
    private readonly IScheduler _scheduler;

    public ReservationFactoryTests()
    {
        _scheduler = Substitute.For<IScheduler>();
        ISchedulerFactory schedulerFactory = Substitute.For<ISchedulerFactory>();
        schedulerFactory.GetScheduler("CustomerScheduler").Returns(_scheduler);
        _tenantCleanerService = new QuartzTenantCleanerService(schedulerFactory);
        
        _roomPricePolicies = PoliciesGenerator.GenerateRoomPricePolicies();
        _tenantPricePolicy = PoliciesGenerator.GenerateTenantPricePolicy();
        _hotelDayFactory = HotelDayGenerator.GenerateHotelDayFactory(14,10);
        _hotelDay = _hotelDayFactory.Create();
    }
    
    [Fact]
    public async Task Create_FirstReservationEver_ShouldCreate()
    {
        // Arrange
        var roomRepository = new InMemoryRoomRepository();
        var clientRepository = new InMemoryClientRepository();
        var reservationFactory = new ReservationFactory(_roomPricePolicies, 
            _tenantPricePolicy, roomRepository, _hotelDayFactory, _tenantCleanerService);
        var clientFactory = new ClientFactory(clientRepository);
        var client = await clientFactory.Create(Guid.NewGuid());
        clientRepository.Clients.Add(client.Id, client);

        await roomRepository.CreateSampleRooms(5);
        var tenantData = TenantDataGenerator.GenerateSampleTenantsDataDtos(1)
            .Select(dto => dto.ToTenantData());

        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2022, 1, 7);
        // Act
        var reservation = await reservationFactory.Create(client, 
            new List<int> { 1 }, 
            tenantData.ToList(), startDate, endDate);
        
        // Assert
        reservation.ShouldNotBeNull();
        reservation.Period.From.ShouldBe(new DateTime(startDate, _hotelDay.StartHour));
        reservation.Period.To.ShouldBe(new DateTime(endDate, _hotelDay.EndHour));
        reservation.Rooms.Count().ShouldBe(1);
        reservation.Tenants.Count().ShouldBe(1);
        await _scheduler.Received(1).ScheduleJob(Arg.Any<IJobDetail>(), Arg.Any<ITrigger>());
    }

    [Fact]
    public async Task Create_WhenNoRoomsAvailable_ThrowsNoRoomsAvailableException()
    {
        // Arrange
        var roomRepository = new InMemoryRoomRepository();
        var clientRepository = new InMemoryClientRepository();
        var reservationFactory = new ReservationFactory(_roomPricePolicies,
            _tenantPricePolicy, roomRepository, _hotelDayFactory, _tenantCleanerService);
        var clientFactory = new ClientFactory(clientRepository);
        var client = await clientFactory.Create(Guid.NewGuid());
        clientRepository.Clients.Add(client.Id, client);

        var tenantData = TenantDataGenerator.GenerateSampleTenantsDataDtos(2)
            .Select(dto => dto.ToTenantData());

        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2022, 1, 7);
        var roomNumbers = new List<int> { 1, 2 };
        // Act
        var exception = await Record.ExceptionAsync(() => reservationFactory.Create(client,
            roomNumbers,
            tenantData.ToList(), startDate, endDate));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<SomeRoomsAreUnavailableException>();
        exception.Message.ShouldBe($"Some rooms are unavailable: {string.Join(", ", roomNumbers)}");
    }
    
    [Fact]
    public async Task Create_WhenNotEnoughRoomSpace_ThrowsNotEnoughRoomSpaceException()
    {
        // Arrange
        var roomRepository = new InMemoryRoomRepository();
        var clientRepository = new InMemoryClientRepository();
        var reservationFactory = new ReservationFactory(_roomPricePolicies,
            _tenantPricePolicy, roomRepository, _hotelDayFactory, _tenantCleanerService);
        var clientFactory = new ClientFactory(clientRepository);
        var client = await clientFactory.Create(Guid.NewGuid());
        clientRepository.Clients.Add(client.Id, client);

        await roomRepository.CreateSampleRooms(20, 2);
        var tenantData = TenantDataGenerator.GenerateSampleTenantsDataDtos(6)
            .Select(dto => dto.ToTenantData());

        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2022, 1, 7);
        // Act
        var exception = await Record.ExceptionAsync(() => reservationFactory.Create(client,
            new List<int> { 1 },
            tenantData.ToList(), startDate, endDate));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<NotEnoughRoomSpaceException>();
        exception.Message.ShouldBe($"Not enough room space. Total rooms capacity: 2, number of tenants: {tenantData.Count()}");
    }
    
    [Fact]
    public async Task Create_WhenNoTenants_ThrowsCannotCreateReservationWithoutTenantsException()
    {
        // Arrange
        var roomRepository = new InMemoryRoomRepository();
        var clientRepository = new InMemoryClientRepository();
        var reservationFactory = new ReservationFactory(_roomPricePolicies,
            _tenantPricePolicy, roomRepository, _hotelDayFactory, _tenantCleanerService);
        var clientFactory = new ClientFactory(clientRepository);
        var client = await clientFactory.Create(Guid.NewGuid());
        clientRepository.Clients.Add(client.Id, client);

        await roomRepository.CreateSampleRooms(5);
        var tenantData = new List<TenantData>();

        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2022, 1, 7);
        // Act
        var exception = await Record.ExceptionAsync(() => reservationFactory.Create(client,
            new List<int> { 1 },
            tenantData, startDate, endDate));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CannotCreateReservationWithoutTenantsException>();
    }
    
    [Fact]
    public async Task Create_WhenNoRooms_ThrowsCannotCreateReservationWithoutRoomsException()
    {
        // Arrange
        var roomRepository = new InMemoryRoomRepository();
        var clientRepository = new InMemoryClientRepository();
        var reservationFactory = new ReservationFactory(_roomPricePolicies,
            _tenantPricePolicy, roomRepository, _hotelDayFactory, _tenantCleanerService);
        var clientFactory = new ClientFactory(clientRepository);
        var client = await clientFactory.Create(Guid.NewGuid());
        clientRepository.Clients.Add(client.Id, client);

        await roomRepository.CreateSampleRooms(5);
        var tenantData = TenantDataGenerator.GenerateSampleTenantsDataDtos(1)
            .Select(dto => dto.ToTenantData());

        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2022, 1, 7);
        // Act
        var exception = await Record.ExceptionAsync(() => reservationFactory.Create(client,
            new List<int>(),
            tenantData.ToList(), startDate, endDate));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CannotCreateReservationWithoutRoomsException>();
    }
    
    [Fact]
    public async Task Create_WhenSelectedRoomIsUnavailable_ThrowsSomeRoomsAreUnavailableException()
    {
        // Arrange
        var roomRepository = new InMemoryRoomRepository();
        var clientRepository = new InMemoryClientRepository();
        var reservationFactory = new ReservationFactory(_roomPricePolicies,
            _tenantPricePolicy, roomRepository, _hotelDayFactory, _tenantCleanerService);
        var clientFactory = new ClientFactory(clientRepository);
        var client = await clientFactory.Create(Guid.NewGuid());
        
        clientRepository.Clients.Add(client.Id, client);

        await roomRepository.CreateSampleRooms(5);
        var tenantData = TenantDataGenerator.GenerateSampleTenantsDataDtos(1)
            .Select(dto => dto.ToTenantData())
            .ToList();
        var startDate = new DateOnly(2022, 1, 1);
        var endDate = new DateOnly(2022, 1, 7);
        
        var otherClient = await clientFactory.Create(Guid.NewGuid());
        var otherClientReservation = await reservationFactory.Create(otherClient,
            new List<int> { 2 },
            tenantData, startDate.AddDays(2), endDate.AddDays(2));
        roomRepository.Reservations.Add(otherClientReservation);
        
        // Act
        var exception = await Record.ExceptionAsync(() => reservationFactory.Create(client,
            new List<int> { 1, 2 },
            tenantData, startDate, endDate));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<SomeRoomsAreUnavailableException>();
        exception.Message.ShouldBe("Some rooms are unavailable: 2");
    }

}