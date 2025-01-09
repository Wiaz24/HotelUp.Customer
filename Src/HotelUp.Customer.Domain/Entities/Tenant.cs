using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities.Abstractions;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Entities;

public class Tenant : Entity<Guid>
{
    public FirstName FirstName { get; private set; } = null!;
    public LastName LastName { get; private set; } = null!;
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Pesel Pesel { get; private set; } = null!;
    public DocumentType DocumentType { get; private set; }
    public PresenceStatus Status { get; private set; }
    internal Tenant(TenantData data)
    {
        Id = Guid.NewGuid();
        FirstName = data.FirstName;
        LastName = data.LastName;
        PhoneNumber = data.PhoneNumber;
        Email = data.Email;
        Pesel = data.Pesel;
        DocumentType = data.DocumentType;
        Status = PresenceStatus.Pending;
    }
    
    private Tenant()
    {
    }
}