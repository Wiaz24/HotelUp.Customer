using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Application.Commands.Exceptions;
using HotelUp.Customer.Domain.Repositories;

namespace HotelUp.Customer.Application.Commands.Handlers;

public class AnonymizeReservationTenantsDataHandler : ICommandHandler<AnonymizeReservationTenantsData>
{
    private readonly IReservationRepository _reservationRepository;

    public AnonymizeReservationTenantsDataHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task HandleAsync(AnonymizeReservationTenantsData command)
    {
        var reservation = await _reservationRepository.GetAsync(command.ReservationId);
        if (reservation is null)
        {
            throw new ReservationNotFoundException(command.ReservationId);
        }
        reservation.AnonymizeTenantsData();
        await _reservationRepository.UpdateAsync(reservation);
    }
}