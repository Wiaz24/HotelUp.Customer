using HotelUp.Customer.Infrastructure.EF.Config;
using HotelUp.Customer.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.EF.Contexts;

public class ReadDbContext : DbContext
{
    // public DbSet<RoomReadModel> Rooms { get; set; }
    // public DbSet<ReservationReadModel> Reservations { get; set; }
    // public DbSet<ClientReadModel> Clients { get; set; }
    
    public ReadDbContext(DbContextOptions<ReadDbContext> options) 
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("HotelUp.Customer");
        
        // var configuration = new ReadConfiguration();
        // modelBuilder.ApplyConfiguration<RoomReadModel>(configuration);
        // modelBuilder.ApplyConfiguration<ReservationReadModel>(configuration);
        // modelBuilder.ApplyConfiguration<ClientReadModel>(configuration);
        
        base.OnModelCreating(modelBuilder);
    }
}