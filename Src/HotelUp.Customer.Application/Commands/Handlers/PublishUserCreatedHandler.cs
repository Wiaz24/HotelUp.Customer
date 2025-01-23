using HotelUp.Customer.Application.ApplicationServices;
using HotelUp.Customer.Application.Commands.Abstractions;
using HotelUp.Customer.Application.Commands.Exceptions;
using HotelUp.Customer.Application.Events.External;

using MassTransit;

namespace HotelUp.Customer.Application.Commands.Handlers;

public class PublishUserCreatedHandler : ICommandHandler<PublishUserCreated>
{
    private readonly IPublishEndpoint _bus;
    private readonly ILambdaApiKeyValidatorService _lambdaApiKeyValidatorService;

    public PublishUserCreatedHandler(IPublishEndpoint bus, ILambdaApiKeyValidatorService lambdaApiKeyValidatorService)
    {
        _bus = bus;
        _lambdaApiKeyValidatorService = lambdaApiKeyValidatorService;
    }

    public async Task HandleAsync(PublishUserCreated command)
    {
        if (await _lambdaApiKeyValidatorService.ValidateAsync(command.ApiKey) is false)
        {
            throw new InvalidApiKeyException(command.ApiKey);
        }
        var userCreatedEvent = new UserCreatedEvent(command.UserId, command.UserEmail);
        await _bus.Publish(userCreatedEvent);
    }
}