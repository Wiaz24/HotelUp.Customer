using System.Text.Json;
using HotelUp.Customer.Domain.ValueObjects.Abstractions;
using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Customer.Domain.ValueObjects;

public record HotelDay : IValueObject
{
    public TimeOnly StartHour { get; init; }
    public TimeOnly EndHour { get; init; }
    internal HotelDay(TimeOnly startHour, TimeOnly endHour)
    {
        if (startHour < endHour)
        {
            throw new HotelDayInvalidHoursException();
        }
        StartHour = startHour;
        EndHour = endHour;
    }

    public static ValueConverter GetValueConverter()
    {
        return new ValueConverter<HotelDay, string>(
            vo => JsonSerializer.Serialize(vo, JsonSerializerOptions.Default),
            value => JsonSerializer.Deserialize<HotelDay>(value, JsonSerializerOptions.Default)!);
    }
}