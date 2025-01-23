using HotelUp.Customer.Shared.Exceptions;

namespace HotelUp.Customer.Application.Commands.Exceptions;

public class InvalidApiKeyException : BusinessRuleException
{
    public InvalidApiKeyException(string apiKey) : base($"Invalid API key: {apiKey}")
    {
    }
}