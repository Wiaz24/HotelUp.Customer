using DotNet.Testcontainers.Builders;
using Testcontainers.RabbitMq;

namespace HotelUp.Customer.Tests.Integration.TestContainers;

internal static class RabbitMqContainerFactory
{
    private const int StartPort = 5673;
    private static int _numInstances = 0;
    private static int GetPort => StartPort + Interlocked.Increment(ref _numInstances) - 1;
    internal static RabbitMqContainer Create()
    {
        var port = GetPort;
        return new RabbitMqBuilder()
            .WithImage("rabbitmq:management")
            .WithPortBinding(port, StartPort)
            .WithEnvironment("RABBITMQ_DEFAULT_USER", "guest")
            .WithEnvironment("RABBITMQ_DEFAULT_PASS", "guest")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(StartPort))
            .Build();
    }
}