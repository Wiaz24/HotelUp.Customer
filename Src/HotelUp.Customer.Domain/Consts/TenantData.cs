namespace HotelUp.Customer.Domain.Consts;

// public record TenantData(string FirstName, string LastName, string Email, string PhoneNumber,
//      string Pesel, DocumentType DocumentType);

public record TenantData
{
    public string FirstName { get; init; } = "John";
    public string LastName { get; init; } = "Doe";
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Pesel { get; init; }
    public DocumentType DocumentType { get; init; }
}