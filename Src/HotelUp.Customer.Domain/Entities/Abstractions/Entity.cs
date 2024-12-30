using HotelUp.Customer.Domain.Events;

namespace HotelUp.Customer.Domain.Entities.Abstractions;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; } = default!;

    public IEnumerable<IDomainEvent> Events => _events;
    
    private readonly List<IDomainEvent> _events = new();
    
    protected void AddEvent(IDomainEvent domainEvent) => _events.Add(domainEvent);
    
    public void ClearEvents() => _events.Clear();
    
    internal Entity()
    {
    }
}