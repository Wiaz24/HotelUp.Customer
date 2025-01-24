using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Application.Commands.Exceptions;

public class BillNotFoundException : BusinessRuleException
{
    public BillNotFoundException(Guid reservationId) 
        : base($"Bill for reservation with id {reservationId} not found.")
    {
    }
}