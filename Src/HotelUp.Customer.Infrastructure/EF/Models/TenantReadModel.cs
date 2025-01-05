using HotelUp.Customer.Domain.Consts;
using DocumentType = HotelUp.Customer.Domain.Consts.DocumentType;

namespace HotelUp.Customer.Infrastructure.EF.Models;

public class TenantReadModel
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string Pesel { get; set; }
    public required DocumentType DocumentType { get; set; }
    public required PresenceStatus Status { get; set; }
}