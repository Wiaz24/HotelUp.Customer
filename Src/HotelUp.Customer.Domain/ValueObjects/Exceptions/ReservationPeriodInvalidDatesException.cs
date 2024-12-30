using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects.Exceptions;

public class ReservationPeriodInvalidDatesException : AppException
{
    public ReservationPeriodInvalidDatesException() : base("Reservation to date must be greater than from date.")
    {
    }
}