using HotelUp.Customer.Domain.Policies.RoomPricePolicy.Options;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Domain.Policies.RoomPricePolicy;

internal static class Extensions
{
    internal static IServiceCollection AddRoomPricePolicy(this IServiceCollection services)
    {
        services.AddOptions<EconomyRoomPricePolicyOptions>()
            .BindConfiguration("RoomPricePolicies:EconomyRoomPolicy")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddOptions<BasicRoomPricePolicyOptions>()
            .BindConfiguration("RoomPricePolicies:BasicRoomPolicy")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddOptions<PremiumRoomPricePolicyOptions>()
            .BindConfiguration("RoomPricePolicies:PremiumRoomPolicy")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IRoomPricePolicy, EconomyRoomPricePolicy>();
        services.AddScoped<IRoomPricePolicy, BasicRoomPricePolicy>();
        services.AddScoped<IRoomPricePolicy, PremiumRoomPricePolicy>();
        return services;
    }
}