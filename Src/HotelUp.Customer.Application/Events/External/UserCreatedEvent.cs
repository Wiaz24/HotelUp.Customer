namespace HotelUp.Customer.Application.Events.External;

public record UserCreatedEvent(Guid UserId, string Email);