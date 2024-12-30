using HotelUp.Customer.Domain.ValueObjects.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects;

public record HotelDay
{
    public TimeOnly StartHour { get; init; }
    public TimeOnly EndHour { get; init; }
    
    public HotelDay(TimeOnly startHour, TimeOnly endHour)
    {
        if (startHour > endHour)
        {
            throw new HotelDayInvalidHoursException();
        }
        StartHour = startHour;
        EndHour = endHour;
    }
    
    
}