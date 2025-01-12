using HotelUp.Customer.Application.Commands;
using HotelUp.Customer.Application.Commands.Handlers;

using Quartz;

namespace HotelUp.Customer.Infrastructure.Quartz.Jobs;

public class TenantCleanerJob : IJob
{
    private readonly AnonymizeReservationTenantsDataHandler _handler;

    public TenantCleanerJob(AnonymizeReservationTenantsDataHandler handler)
    {
        _handler = handler;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var reservationId = context.MergedJobDataMap.GetGuid("ReservationId");
        var command = new AnonymizeReservationTenantsData(reservationId);
        await _handler.HandleAsync(command);
    }
}