using HotelUp.Customer.Domain.ValueObjects.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects;

public record ReservationPeriod
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }
    
    public ReservationPeriod(DateTime from, DateTime to)
    {
        if (from >= to)
        {
            throw new ReservationPeriodInvalidDatesException();
        }
        From = from;
        To = to;
    }

    public ReservationPeriod(DateOnly from, DateOnly to, HotelDay hotelDay)
    {
        if (from >= to)
        {
            throw new ReservationPeriodInvalidDatesException();
        }
        
        From = new DateTime(from.Year, from.Month, from.Day, 
            hotelDay.StartHour.Hour, hotelDay.StartHour.Minute, hotelDay.StartHour.Second);
        To = new DateTime(to.Year, to.Month, to.Day,
            hotelDay.StartHour.Hour, hotelDay.StartHour.Minute, hotelDay.StartHour.Second);
    }
}