using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects.Exceptions;

public class HotelDayInvalidHoursException : BusinessRuleException
{
    public HotelDayInvalidHoursException() : base("End hour must be greater than start hour")
    {
    }
}