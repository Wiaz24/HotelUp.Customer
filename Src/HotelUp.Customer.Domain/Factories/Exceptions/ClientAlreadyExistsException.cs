using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.Factories.Exceptions;

public class ClientAlreadyExistsException : BusinessRuleException
{
    public ClientAlreadyExistsException(Guid id) : base($"Client with id: '{id}' already exists.")
    {
    }
}