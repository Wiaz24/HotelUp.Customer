namespace HotelUp.Customer.API.DTOs;

public record UserCreatedDto
{
    public required string ApiKey { get; init; }
    public required Guid UserId { get; init; }
    public required string UserEmail { get; init; }
}