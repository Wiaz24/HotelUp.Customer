using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Services;

public interface IHotelHourService
{
    public HotelDay GetCurrentHotelDay();
}