using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Domain.Factories.Exceptions;

public class ClientAlreadyExistsException : AppException
{
    public ClientAlreadyExistsException(Guid id) : base($"Client with id: '{id}' already exists.")
    {
    }
}