using System.Text.Json;
using HotelUp.Customer.Domain.ValueObjects.Abstractions;
using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Customer.Domain.ValueObjects;

public record ReservationPeriod : IValueObject
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
        
        From = new DateTime(from, hotelDay.StartHour);
        To = new DateTime(to, hotelDay.EndHour);
    }

    public static ValueConverter GetStringValueConverter()
    {
        return new ValueConverter<ReservationPeriod, string>(
            vo => JsonSerializer.Serialize(vo, JsonSerializerOptions.Default),
            value => JsonSerializer.Deserialize<ReservationPeriod>(value, JsonSerializerOptions.Default)!);
    }
}

public static class ReservationPeriodExtensions
{
    public static bool PartiallyOverlapsWith(this ReservationPeriod period, ReservationPeriod otherPeriod)
    {
        return !(otherPeriod.To < period.From || otherPeriod.From > period.To);
    }
}