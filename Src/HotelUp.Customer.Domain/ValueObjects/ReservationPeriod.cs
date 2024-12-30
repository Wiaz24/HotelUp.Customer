using HotelUp.Customer.Domain.ValueObjects.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects;

public record ReservationPeriod
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }
    
    public ReservationPeriod(DateTime from, DateTime to)
    {
        if (from > to)
        {
            throw new ReservationPeriodInvalidDatesException();
        }
        From = from;
        To = to;
    }
}