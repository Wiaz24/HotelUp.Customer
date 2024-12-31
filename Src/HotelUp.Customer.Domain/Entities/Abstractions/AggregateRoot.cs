namespace HotelUp.Customer.Domain.Entities.Abstractions;

public abstract class AggregateRoot<TId> : Entity<TId>
{
    internal AggregateRoot()
    {
    }
}