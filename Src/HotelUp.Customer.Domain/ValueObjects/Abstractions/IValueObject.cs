using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Customer.Domain.ValueObjects.Abstractions;

public interface IValueObject
{
    public static abstract ValueConverter GetValueConverter();
}