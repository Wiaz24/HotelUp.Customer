using HotelUp.Customer.Domain.Policies.RoomPricePolicy;
using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Policies.TenantPricePolicy;

public interface ITenantPricePolicy
{
    bool IsApplicable(TenantPricePolicyData tenantPricePolicyData);
    Money CalculateAccomodationPrice(TenantPricePolicyData tenantPricePolicyData);
}