using DotNet.Testcontainers.Builders;
using Testcontainers.PostgreSql;

namespace HotelUp.Customer.Tests.Integration.TestContainers;

internal static class TestDatabaseFactory
{
    private const int StartPort = 5432;
    private static int _numInstances = 0;
    private static int GetPort => StartPort + Interlocked.Increment(ref _numInstances) - 1;
    internal static PostgreSqlContainer Create()
    {
        var port = GetPort;
        return new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("TestDb")
            .WithPortBinding(port, StartPort)
            .WithUsername("Postgres")
            .WithPassword("Postgres")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(StartPort))
            .Build();
    }
}