namespace HotelUp.Customer.Domain.Consts;

public record TenantData
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public required string Pesel { get; init; }
    public DocumentType DocumentType { get; init; }
}