namespace HotelUp.Customer.Infrastructure.EF.Postgres;

public sealed class PostgresOptions
{
    public required string ConnectionString { get; init; }
}