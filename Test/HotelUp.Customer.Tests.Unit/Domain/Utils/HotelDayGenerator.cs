using HotelUp.Customer.Domain.Factories;
using HotelUp.Customer.Domain.Factories.Options;
using HotelUp.Customer.Domain.ValueObjects;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace HotelUp.Customer.Unit.Domain.Utils;

public static class HotelDayGenerator
{
    public static HotelDay GenerateHotelDay(int startHour = 14, int endHour = 10)
    {
        var hotelDayFactory = GenerateHotelDayFactory(startHour, endHour);
        return hotelDayFactory.Create();
    }
    
    public static HotelDayFactory GenerateHotelDayFactory(int startHour = 14, int endHour = 10)
    {
        var hotelDayOptions = new HotelDayOptions
        {
            StartHour = new TimeOnly(startHour,00),
            EndHour = new TimeOnly(endHour,00)
        };
        var hotelDayOptionsSnapshot = Substitute.For<IOptionsSnapshot<HotelDayOptions>>();
        hotelDayOptionsSnapshot.Value.Returns(hotelDayOptions);
        return new HotelDayFactory(hotelDayOptionsSnapshot);
    }
}