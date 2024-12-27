using Testcontainers.PostgreSql;

namespace HotelUp.Customer.Tests.Integration.TestContainers;

internal static class TestDatabaseFactory
{
    internal static PostgreSqlContainer CreatePostgreSqlContainer()
    {
        return new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase($"TestDb{Guid.NewGuid()}")
            .WithUsername("Postgres")
            .WithPassword("Postgres")
            .Build();
    }
}