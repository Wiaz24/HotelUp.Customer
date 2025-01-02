using HotelUp.Customer.API.DTOs;
using HotelUp.Customer.Domain.Consts;

namespace HotelUp.Customer.Tests.Shared.Utils;

public static class TenantDataGenerator
{
    public static IEnumerable<TenantDataDto> GenerateSampleTenantsDataDtos(int count)
    {
        var tenants = new List<TenantDataDto>();
        for (var i = 0; i < count; i++)
        {
            var data = new TenantDataDto()
            {
                FirstName = $"John{i}",
                LastName = $"Doe{i}",
                Email = $"johndoe{i}@email.com",
                PhoneNumber = "123456789",
                Pesel = "78060399743",
                DocumentType = DocumentType.Passport
            };
            tenants.Add(data);
        }
        return tenants;
    }
}