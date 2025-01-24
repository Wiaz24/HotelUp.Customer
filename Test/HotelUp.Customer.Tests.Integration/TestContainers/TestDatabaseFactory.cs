using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Volumes;

using Testcontainers.PostgreSql;

namespace HotelUp.Customer.Tests.Integration.TestContainers;

internal static class TestDatabaseFactory
{
    private const int DefaultPort = 5432;
    private static int _numInstances = 0;
    // Container port starts from 5433 to avoid conflicts with local Postgres
    private static int GetPort => DefaultPort + Interlocked.Increment(ref _numInstances);
    internal static PostgreSqlContainer Create()
    {
        var initSqlPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "init.sql");
        var postgresqlConfPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "postgresql.conf");
        var port = GetPort;
        return new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("TestDb")
            .WithPortBinding(port, DefaultPort)
            .WithUsername("Postgres")
            .WithPassword("Postgres")
            .WithCommand("-c", "config_file=/etc/postgresql.conf")
            .WithBindMount(initSqlPath, "/docker-entrypoint-initdb.d/init.sql")
            .WithBindMount(postgresqlConfPath, "/etc/postgresql.conf")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(DefaultPort))
            .Build();
    }
}