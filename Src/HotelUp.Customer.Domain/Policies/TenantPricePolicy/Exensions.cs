using HotelUp.Customer.Domain.Policies.TenantPricePolicy.Options;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Domain.Policies.TenantPricePolicy;

public static class Exensions
{
    public static IServiceCollection AddTenantPricePolicy(this IServiceCollection services)
    {
        services.AddOptions<TenantPricePolicyOptions>()
            .BindConfiguration("TenantPricePolicy")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<ITenantPricePolicy, TenantPricePolicy>();
        return services;
    }
    
}