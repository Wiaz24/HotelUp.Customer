using HotelUp.Customer.Domain.Policies.RoomPricePolicy;
using HotelUp.Customer.Domain.Policies.RoomPricePolicy.Options;
using HotelUp.Customer.Domain.Policies.TenantPricePolicy;
using HotelUp.Customer.Domain.Policies.TenantPricePolicy.Options;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace HotelUp.Customer.Tests.Shared.Utils;

public static class PoliciesGenerator
{
    public static IEnumerable<IRoomPricePolicy> GenerateRoomPricePolicies()
    {
        EconomyRoomPricePolicyOptions economyRoomOptions = new()
        {
            BasePrice = 120,
            CapacityMultiplier = 0.2m
        };
        BasicRoomPricePolicyOptions basicRoomOptions = new()
        {
            BasePrice = 180,
            CapacityMultiplier = 0.2m
        };
        PremiumRoomPricePolicyOptions premiumRoomOptions = new()
        {
            BasePrice = 250,
            CapacityMultiplier = 0.2m
        };
        var economyRoomPricePolicySnapshot = Substitute.For<IOptionsSnapshot<EconomyRoomPricePolicyOptions>>();
        var basicRoomPricePolicySnapshot = Substitute.For<IOptionsSnapshot<BasicRoomPricePolicyOptions>>();
        var premiumRoomPricePolicySnapshot = Substitute.For<IOptionsSnapshot<PremiumRoomPricePolicyOptions>>();
        economyRoomPricePolicySnapshot.Value.Returns(economyRoomOptions);
        basicRoomPricePolicySnapshot.Value.Returns(basicRoomOptions);
        premiumRoomPricePolicySnapshot.Value.Returns(premiumRoomOptions);
        return new List<IRoomPricePolicy>
        {
            new EconomyRoomPricePolicy(economyRoomPricePolicySnapshot),
            new BasicRoomPricePolicy(basicRoomPricePolicySnapshot),
            new PremiumRoomPricePolicy(premiumRoomPricePolicySnapshot)
        };
    }
    
    public static ITenantPricePolicy GenerateTenantPricePolicy()
    {
        TenantPricePolicyOptions tenantPricePolicyOptions = new()
        {
            Discount = 0.97
        };
        var tenantPricePolicySnapshot = Substitute.For<IOptionsSnapshot<TenantPricePolicyOptions>>();
        tenantPricePolicySnapshot.Value.Returns(tenantPricePolicyOptions);
        return new TenantPricePolicy(tenantPricePolicySnapshot);
    }
}