using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities.Abstractions;
using HotelUp.Customer.Domain.Entities.Exceptions;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Entities;

public class Tenant : Entity<Guid>
{
    public FirstName? FirstName { get; private set; }
    public LastName? LastName { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public Email? Email { get; private set; }
    public Pesel? Pesel { get; private set; }
    public DocumentType? DocumentType { get; private set; }
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
    
    public void CheckIn()
    {
        Status = PresenceStatus.CheckedIn;
    }
    
    public void CheckOut()
    {
        Status = PresenceStatus.CheckedOut;
    }
    
    internal void Anonymize()
    {
        if (Status == PresenceStatus.CheckedIn)
        {
            throw new TenantCannotBeAnonymizedDuringReservationException();
        }
        FirstName = null;
        LastName = null;
        PhoneNumber = null;
        Email = null;
        Pesel = null;
        DocumentType = null;
    }
}