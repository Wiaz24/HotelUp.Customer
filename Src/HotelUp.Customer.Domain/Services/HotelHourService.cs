using HotelUp.Customer.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace HotelUp.Customer.Domain.Services;

public class HotelHourService : IHotelHourService
{
    private readonly IOptions<HotelDay> _hotelDay;

    public HotelHourService(IOptionsSnapshot<HotelDay> hotelDay)
    {
        _hotelDay = hotelDay;
    }

    public HotelDay GetCurrentHotelDay()
    {
        return new HotelDay(_hotelDay.Value.StartHour, _hotelDay.Value.EndHour);
    }
}