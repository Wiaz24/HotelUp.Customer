using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects.Exceptions;

public class ReservationPeriodInvalidDatesException : BusinessRuleException
{
    public ReservationPeriodInvalidDatesException() : base("Reservation start date must be greater than end date.")
    {
    }
}