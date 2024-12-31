using System.ComponentModel.DataAnnotations;

namespace HotelUp.Customer.Domain.Policies.RoomPricePolicy.Options;

public class BasicRoomPricePolicyOptions
{
    [Range(1, 10000)]
    public required decimal BasePrice { get; init; }
    [Range(0, 1)]
    public required decimal CapacityMultiplier { get; init; }
}