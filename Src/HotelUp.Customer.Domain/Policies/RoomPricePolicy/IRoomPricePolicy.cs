using HotelUp.Customer.Domain.ValueObjects;

namespace HotelUp.Customer.Domain.Policies.RoomPricePolicy;

public interface IRoomPricePolicy
{
    bool IsApplicable(RoomPricePolicyData roomPricePolicyData);
    Money CalculateRoomPrice(RoomPricePolicyData roomPricePolicyData);
}