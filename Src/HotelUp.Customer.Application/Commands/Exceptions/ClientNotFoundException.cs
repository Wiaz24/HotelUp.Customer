using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Application.Commands.Exceptions;

public class ClientNotFoundException : AppException
{
    public ClientNotFoundException(Guid id) : base($"Client with id: '{id}' was not found.")
    {
    }
}