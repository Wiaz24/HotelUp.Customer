using MassTransit;

namespace HotelUp.Customer.Shared.Messaging.RabbitMQ;

public class CustomNameFormatter : IEntityNameFormatter
{
    public string FormatEntityName<T>()
    {
        return $"HotelUp.Customer:{typeof(T).Name}";
    }
}