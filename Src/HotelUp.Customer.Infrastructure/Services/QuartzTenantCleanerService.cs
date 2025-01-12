using HotelUp.Customer.Domain.Services;
using HotelUp.Customer.Infrastructure.Quartz.Jobs;

using Quartz;

namespace HotelUp.Customer.Infrastructure.Services;

public class QuartzTenantCleanerService : ITenantCleanerService
{
    private readonly ISchedulerFactory _schedulerFactory;
    public QuartzTenantCleanerService(ISchedulerFactory schedulerFactory)
    {
        _schedulerFactory = schedulerFactory;
    }

    public async Task EnqueueForAnonymizationAsync(Guid reservationId, DateTimeOffset anonymizationDate)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        var dataMap = new JobDataMap
        {
            { "ReservationId", reservationId.ToString() }
        };
        IJobDetail job = JobBuilder.Create<TenantCleanerJob>()
            .UsingJobData(dataMap)
            .WithIdentity($"TenantCleanerJob-{reservationId}")
            .Build();
        
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity($"TenantCleanerTrigger-{reservationId}")
            .StartAt(anonymizationDate)
            .Build();
        
        await scheduler.ScheduleJob(job, trigger);
    }
}