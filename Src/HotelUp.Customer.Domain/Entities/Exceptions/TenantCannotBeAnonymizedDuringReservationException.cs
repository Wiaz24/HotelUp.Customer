using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.Entities.Exceptions;

public class TenantCannotBeAnonymizedDuringReservationException : BusinessRuleException
{
    public TenantCannotBeAnonymizedDuringReservationException() 
        : base("Tenant cannot be anonymized during reservation.")
    {
    }
}