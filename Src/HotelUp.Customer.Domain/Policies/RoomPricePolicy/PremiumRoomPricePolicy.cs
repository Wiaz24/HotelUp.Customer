using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Policies.RoomPricePolicy.Options;
using HotelUp.Customer.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace HotelUp.Customer.Domain.Policies.RoomPricePolicy;

public class PremiumRoomPricePolicy : IRoomPricePolicy
{
    private readonly Money _basePrice;
    private readonly decimal _capacityMultiplier;

    public PremiumRoomPricePolicy(IOptionsSnapshot<PremiumRoomPricePolicyOptions> options)
    {
        _basePrice = options.Value.BasePrice;
        _capacityMultiplier = options.Value.CapacityMultiplier;
    }
    public bool IsApplicable(RoomPricePolicyData roomPricePolicyData)
    {
        return roomPricePolicyData.Room.Type == RoomType.Premium;
    }

    public Money CalculateRoomPrice(RoomPricePolicyData roomPricePolicyData)
    {
        var amount = _basePrice.Amount + roomPricePolicyData.Room.Capacity * _capacityMultiplier;
        return new Money(amount, _basePrice.Currency);
    }
}