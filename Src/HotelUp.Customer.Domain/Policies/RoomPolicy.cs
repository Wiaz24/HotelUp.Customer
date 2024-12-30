namespace HotelUp.Customer.Domain.Policies;

public abstract class RoomPolicy : IPricePolicy
{
    public abstract float TypeMultiplier { get; init; }
    public abstract float CapacityMultiplier { get; init; }
    
    public bool IsApplicable(PolicyData policyData)
    {
        throw new NotImplementedException();
    }
}