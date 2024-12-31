using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.Factories.Exceptions;

public class CannotCreateReservationWithoutTenantsException : AppException
{
    public CannotCreateReservationWithoutTenantsException() 
        : base("Cannot create reservation without tenants.")
    {
    }
}