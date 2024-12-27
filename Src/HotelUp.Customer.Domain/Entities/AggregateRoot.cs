namespace HotelUp.Customer.Domain.Entities;

public abstract class AggregateRoot<TId> : Entity<TId>
{
    internal AggregateRoot()
    {
    }
}