namespace HotelUp.Customer.Domain.Factories.Options;

public class HotelDayOptions
{
    public required TimeOnly StartHour { get; init; }
    public required TimeOnly EndHour { get; init; }
}