using HotelUp.Customer.Application.Commands.Abstractions;

namespace HotelUp.Customer.Application.Commands;

public record PublishUserCreated(string ApiKey, Guid UserId, string UserEmail) : ICommand;