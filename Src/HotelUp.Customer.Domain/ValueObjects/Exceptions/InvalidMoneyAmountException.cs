using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects.Exceptions;

public class InvalidMoneyAmountException : AppException
{
    public InvalidMoneyAmountException(decimal amount) 
        : base($"Invalid money amount: {amount}. Amount must be greater than or equal to 0.")
    {
    }
}