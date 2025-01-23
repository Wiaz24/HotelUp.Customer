using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Application.Commands.Exceptions;
using HotelUp.Customer.Domain.Repositories;

namespace HotelUp.Customer.Application.Commands.Handlers;

public class AddAdditionalCostHandler : ICommandHandler<AddAdditionalCost>
{
    private readonly IReservationRepository _reservationRepository;

    public AddAdditionalCostHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task HandleAsync(AddAdditionalCost command)
    {
        var reservation = await _reservationRepository.GetAsync(command.ReservationId);
        if (reservation is null)
        {
            throw new ReservationNotFoundException(command.ReservationId);
        }
        if (reservation.Bill is null)
        {
            throw new BillNotFoundException(command.ReservationId);
        }
        reservation.Bill.AddAdditionalCost(command.Amount);
        await _reservationRepository.UpdateAsync(reservation);
    }
}