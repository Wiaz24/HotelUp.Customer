using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.Factories.Exceptions;

public class CannotCreateReservationWithoutTenants : AppException
{
    public CannotCreateReservationWithoutTenants() : base("Cannot create reservation without tenants.")
    {
    }
}