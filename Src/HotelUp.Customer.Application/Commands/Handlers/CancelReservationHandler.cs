using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Application.Commands.Exceptions;
using HotelUp.Customer.Application.Events;
using HotelUp.Customer.Domain.Repositories;

using MassTransit;

namespace HotelUp.Customer.Application.Commands.Handlers;

public class CancelReservationHandler : ICommandHandler<CancelReservation>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IPublishEndpoint _bus;
    public CancelReservationHandler(IReservationRepository reservationRepository, IPublishEndpoint bus)
    {
        _reservationRepository = reservationRepository;
        _bus = bus;
    }

    public async Task HandleAsync(CancelReservation command)
    {
        var reservation = await _reservationRepository.GetAsync(command.Id);
        if (reservation is null)
        {
            throw new ReservationNotFoundException(command.Id);
        }
        reservation.Cancel();
        await _reservationRepository.UpdateAsync(reservation);
        await _bus.Publish(new ReservationCanceledEvent(reservation.Id));
    }
}