namespace HotelUp.Customer.Application.ApplicationServices;

public interface ILambdaApiKeyValidatorService
{
    public Task<bool> ValidateAsync(string apiKey);
}