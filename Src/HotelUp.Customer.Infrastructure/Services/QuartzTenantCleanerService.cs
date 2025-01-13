using HotelUp.Customer.Domain.Services;
using HotelUp.Customer.Infrastructure.Quartz.Jobs;

using Quartz;

namespace HotelUp.Customer.Infrastructure.Services;

public class QuartzTenantCleanerService : ITenantCleanerService
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly TimeProvider _timeProvider;
    public QuartzTenantCleanerService(ISchedulerFactory schedulerFactory, TimeProvider timeProvider)
    {
        _schedulerFactory = schedulerFactory;
        _timeProvider = timeProvider;
    }

    public async Task EnqueueForAnonymizationAsync(Guid reservationId, DateOnly anonymizationDate)
    {
        var scheduler = await _schedulerFactory.GetScheduler("CustomerScheduler");
        if (scheduler is null)
        {
            throw new NullReferenceException("Scheduler with name 'CustomerScheduler' not found");
        }
        var dataMap = new JobDataMap
        {
            { "ReservationId", reservationId.ToString() }
        };
        IJobDetail job = JobBuilder.Create<TenantCleanerJob>()
            .UsingJobData(dataMap)
            .WithIdentity($"TenantCleanerJob-{reservationId}")
            .Build();

        var currentTime = _timeProvider.GetUtcNow();
        TimeOnly executionTime = new TimeOnly(currentTime.Hour, currentTime.Minute, currentTime.Second);
        var executionDateTimeOffset = new DateTimeOffset(
            anonymizationDate.ToDateTime(executionTime), 
            TimeSpan.Zero);
        
        ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity($"TenantCleanerTrigger-{reservationId}")
            .StartAt(executionDateTimeOffset)
            .Build();
        
        await scheduler.ScheduleJob(job, trigger);
    }
}