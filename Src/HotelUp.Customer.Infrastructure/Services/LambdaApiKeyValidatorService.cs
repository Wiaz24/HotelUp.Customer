using HotelUp.Customer.Application.ApplicationServices;

using Microsoft.Extensions.Options;

namespace HotelUp.Customer.Infrastructure.Services;

public class LambdaApiKeyValidatorService : ILambdaApiKeyValidatorService
{
    private readonly LambdaOptions _options;

    public LambdaApiKeyValidatorService(IOptionsSnapshot<LambdaOptions> options)
    {
        _options = options.Value;
    }

    public Task<bool> ValidateAsync(string apiKey)
    {
        return Task.FromResult(apiKey == _options.ApiKey);
    }
}