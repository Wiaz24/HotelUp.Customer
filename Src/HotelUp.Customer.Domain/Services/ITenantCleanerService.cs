namespace HotelUp.Customer.Domain.Services;

public interface ITenantCleanerService
{
    public Task EnqueueForAnonymizationAsync(Guid reservationId, DateOnly anonymizationDate);
}