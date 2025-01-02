using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Infrastructure.EF.Config;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.EF.Contexts;

public class WriteDbContext : DbContext
{
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<AdditionalCost> AdditionalCosts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    
    public WriteDbContext(DbContextOptions<WriteDbContext> options) 
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("HotelUp.Customer");
        
        var configuration = new WriteConfiguration();
        modelBuilder.ApplyConfiguration<Reservation>(configuration);
        modelBuilder.ApplyConfiguration<Client>(configuration);
        modelBuilder.ApplyConfiguration<Room>(configuration);
        modelBuilder.ApplyConfiguration<Tenant>(configuration);
        modelBuilder.ApplyConfiguration<Bill>(configuration);
        modelBuilder.ApplyConfiguration<AdditionalCost>(configuration);
        modelBuilder.ApplyConfiguration<Payment>(configuration);
        
        base.OnModelCreating(modelBuilder);
    }
}