using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Infrastructure.EFCore.Config;
using HotelUp.Customer.Infrastructure.EFCore.Postgres;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelUp.Customer.Infrastructure.EFCore.Contexts;

public sealed class ReadDbContext : DbContext
{
    private readonly PostgresOptions _postgresOptions;
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Client> Clients { get; set; }
    
    public ReadDbContext(DbContextOptions<ReadDbContext> options, IOptions<PostgresOptions> postgresOptions) 
        : base(options)
    {
        _postgresOptions = postgresOptions.Value;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddCommonConfiguration(_postgresOptions);
        base.OnModelCreating(modelBuilder);
    }
    public override int SaveChanges()
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }
}