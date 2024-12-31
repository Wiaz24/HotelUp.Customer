using HotelUp.Customer.Domain.Factories.Abstractions;
using HotelUp.Customer.Domain.Factories.Options;
using HotelUp.Customer.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace HotelUp.Customer.Domain.Factories;

public class HotelDayFactory : IHotelDayFactory
{
    private readonly HotelDayOptions _options;

    public HotelDayFactory(IOptionsSnapshot<HotelDayOptions> options)
    {
        _options = options.Value;
    }

    public HotelDay Create()
    {
        return new HotelDay(_options.StartHour, _options.EndHour);
    }
}