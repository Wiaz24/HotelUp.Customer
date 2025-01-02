using System.ComponentModel;
using HotelUp.Customer.Domain.Consts;

namespace HotelUp.Customer.API.DTOs;

public record TenantDataDto
{
    [DefaultValue("John")]
    public required string FirstName { get; init; }
    
    [DefaultValue("Doe")]
    public required string LastName { get; init; }
    
    [DefaultValue("john.doe@email.com")]
    public required string Email { get; init; }
    
    [DefaultValue("123456789")]
    public required string PhoneNumber { get; init; }
    
    [DefaultValue("12345678901")]
    public required string Pesel { get; init; }
    
    [DefaultValue(DocumentType.Passport)]
    public required DocumentType DocumentType { get; init; }
    
    public TenantData ToTenantData()
    {
        return new TenantData
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Pesel = Pesel,
            DocumentType = DocumentType
        };
    }
}