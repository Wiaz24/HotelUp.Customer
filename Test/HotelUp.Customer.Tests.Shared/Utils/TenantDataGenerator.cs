using HotelUp.Customer.Domain.Consts;

namespace HotelUp.Customer.Tests.Shared.Utils;

public static class TenantDataGenerator
{
    public static IEnumerable<TenantData> GenerateSampleTenantsData(int count)
    {
        var tenants = new List<TenantData>();
        for (var i = 0; i < count; i++)
        {
            var firstName = $"John{i}";
            var lastName = $"Doe{i}";
            var email = $"johndoe{i}@email.com";
            var phoneNumber = "123456789";
            var pesel = "78060399743";
            var documentType = DocumentType.Passport;
            tenants.Add(new TenantData(firstName, lastName, email, phoneNumber, pesel, documentType));
        }
        return tenants;
    }
}