namespace HotelUp.Customer.Domain.Services;

public interface ITenantCleanerService
{
    public Task EnqueueForAnonymizationAsync(Guid reservationId, DateTimeOffset anonymizationDate);
}