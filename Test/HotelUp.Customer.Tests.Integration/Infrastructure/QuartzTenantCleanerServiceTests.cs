using HotelUp.Customer.Infrastructure.Services;

using Microsoft.Extensions.DependencyInjection;

using NSubstitute;

using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;

using Shouldly;

namespace HotelUp.Customer.Tests.Integration.Infrastructure;

[Collection(nameof(QuartzTenantCleanerServiceTests))]
public class QuartzTenantCleanerServiceTests : IntegrationTestsBase
{
    private readonly ISchedulerFactory _schedulerFactory;

    public QuartzTenantCleanerServiceTests(TestWebAppFactory factory) : base(factory)
    {
        _schedulerFactory = Factory.Services.GetRequiredService<ISchedulerFactory>();
    }
    
    [Fact]
    public async Task EnqueueForAnonymizationAsync_WhenCalled_ShouldEnqueueJob()
    {
        // Arrange
        var scheduler = await _schedulerFactory.GetScheduler("CustomerScheduler");
        scheduler.ShouldNotBeNull();

        var fakeTimeProvider = Substitute.For<TimeProvider>();
        fakeTimeProvider.GetUtcNow()
            .Returns(new DateTimeOffset(DateTime.UtcNow.AddSeconds(5), TimeSpan.Zero));
        
        var cleanerService = new QuartzTenantCleanerService(_schedulerFactory, fakeTimeProvider);

        
        // Act
        await cleanerService.EnqueueForAnonymizationAsync(Guid.NewGuid(),
            DateOnly.FromDateTime(DateTime.UtcNow));
        
        // Assert
        var jobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
        jobKeys.Count.ShouldBe(1);
        var jobKey = jobKeys.First();
        var triggers = await scheduler.GetTriggersOfJob(jobKey);
        triggers.Count.ShouldBe(1);
        var trigger = triggers.First()! as SimpleTriggerImpl;
        trigger.ShouldNotBeNull();
        trigger.TimesTriggered.ShouldBe(0);
        
        await Task.Delay(TimeSpan.FromSeconds(10));
        
        triggers = await scheduler.GetTriggersOfJob(jobKey);
        triggers.Count.ShouldBe(1);
        trigger = triggers.First()! as SimpleTriggerImpl;
        trigger.ShouldNotBeNull();
        trigger.TimesTriggered.ShouldBe(1);
    }
}