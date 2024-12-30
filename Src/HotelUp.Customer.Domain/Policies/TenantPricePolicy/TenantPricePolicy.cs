using HotelUp.Customer.Domain.Policies.TenantPricePolicy.Options;
using HotelUp.Customer.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace HotelUp.Customer.Domain.Policies.TenantPricePolicy;

public class TenantPricePolicy : ITenantPricePolicy
{
    private readonly TenantPricePolicyOptions _tenantPricePolicyOptions;

    public TenantPricePolicy(IOptionsSnapshot<TenantPricePolicyOptions> tenantPricePolicyOptions)
    {
        _tenantPricePolicyOptions = tenantPricePolicyOptions.Value;
    }

    public bool IsApplicable(TenantPricePolicyData tenantPricePolicyData)
    {
        return tenantPricePolicyData.NumOfTenants > 1;
    }

    public Money CalculateAccomodationPrice(TenantPricePolicyData data)
    {
        return data.CombinedRoomPrice * (decimal) Math.Pow(_tenantPricePolicyOptions.Discount, data.NumOfTenants);
    }
}