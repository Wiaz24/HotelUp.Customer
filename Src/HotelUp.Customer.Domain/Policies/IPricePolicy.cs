namespace HotelUp.Customer.Domain.Policies;

public interface IPricePolicy
{
    bool IsApplicable(PolicyData policyData);
}