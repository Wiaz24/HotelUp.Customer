using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Application.Commands.Exceptions;
using HotelUp.Customer.Domain.Repositories;

namespace HotelUp.Customer.Application.Commands.Handlers;

public class CancelReservationHandler : ICommandHandler<CancelReservation>
{
    private readonly IReservationRepository _reservationRepository;

    public CancelReservationHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
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
    }
}