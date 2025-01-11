using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Infrastructure.EF.Config;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.EF.Contexts;

public sealed class ReadDbContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Client> Clients { get; set; }
    
    public ReadDbContext(DbContextOptions<ReadDbContext> options) 
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddCommonConfiguration();
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