namespace HotelUp.Customer.Application.Events;

public record UserCreatedEvent(Guid UserId, string Email);