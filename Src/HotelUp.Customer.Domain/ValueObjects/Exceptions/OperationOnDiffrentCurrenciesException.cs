using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects.Exceptions;

public class OperationOnDiffrentCurrenciesException : AppException
{
    public OperationOnDiffrentCurrenciesException() : base("Cannot perform operation on different currencies")
    {
    }
}