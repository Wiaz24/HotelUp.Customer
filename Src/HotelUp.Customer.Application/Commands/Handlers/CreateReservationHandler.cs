using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Application.Commands.Exceptions;
using HotelUp.Customer.Application.Events;
using HotelUp.Customer.Application.Queries.DTOs;
using HotelUp.Customer.Domain.Factories.Abstractions;
using HotelUp.Customer.Domain.Repositories;
using MassTransit;

namespace HotelUp.Customer.Application.Commands.Handlers;

public class CreateReservationHandler : ICommandHandler<CreateReservation, Guid>
{
    private readonly IClientRepository _clientRepository;
    private readonly IReservationFactory _reservationFactory;
    private readonly IReservationRepository _reservationRepository;
    private readonly IPublishEndpoint _bus;

    public CreateReservationHandler(IClientRepository clientRepository, 
        IReservationFactory reservationFactory, IReservationRepository reservationRepository, IBus bus)
    {
        _clientRepository = clientRepository;
        _reservationFactory = reservationFactory;
        _reservationRepository = reservationRepository;
        _bus = bus;
    }

    public async Task<Guid> HandleAsync(CreateReservation command)
    {
        var client = await _clientRepository.GetAsync(command.ClientId);
        if (client is null)
        {
            throw new ClientNotFoundException(command.ClientId);
        }
        var reservation = await _reservationFactory.Create(client, command.RoomNumbers.ToList(), 
            command.TenantsData.ToList(), command.StartDate, command.EndDate);
        await _reservationRepository.AddAsync(reservation);
        await _bus.Publish(new ReservationCreatedEvent(new ReservationDto(reservation)));
        return reservation.Id;
    }
}