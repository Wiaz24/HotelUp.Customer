using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Policies.TenantPricePolicy;

public record TenantPricePolicyData(Money CombinedRoomPrice, int NumOfTenants);