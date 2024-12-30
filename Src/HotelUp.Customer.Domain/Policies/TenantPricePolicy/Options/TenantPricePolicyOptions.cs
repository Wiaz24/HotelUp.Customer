using System.ComponentModel.DataAnnotations;

namespace HotelUp.Customer.Domain.Policies.TenantPricePolicy.Options;

public class TenantPricePolicyOptions
{
    [Range(0,1)]
    public required decimal Discount { get; set; }
}