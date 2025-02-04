using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;

namespace HotelUp.Customer.Application.Queries.DTOs;

public record TenantDto
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Email { get; init; }
    public string? Pesel { get; init; }
    public DocumentType? DocumentType { get; init; }
    public PresenceStatus Status { get; init; }
    
    public TenantDto(Tenant tenant)
    {
        FirstName = tenant.FirstName?.Value;
        LastName = tenant.LastName?.Value;
        PhoneNumber = tenant.PhoneNumber?.Value;
        Email = tenant.Email?.Value;
        Pesel = tenant.Pesel?.Value;
        DocumentType = tenant.DocumentType;
        Status = tenant.Status;
    }
}