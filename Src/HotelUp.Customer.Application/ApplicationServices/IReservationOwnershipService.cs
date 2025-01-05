namespace HotelUp.Customer.Application.ApplicationServices;

public interface IReservationOwnershipService
{
    Task<bool> IsReservationOwner(Guid reservationId, Guid clientId);
}