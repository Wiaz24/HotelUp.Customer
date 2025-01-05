using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Infrastructure.EF.Config;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.EF.Contexts;

public class WriteDbContext : DbContext
{
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Room> Rooms { get; set; }
    
    public WriteDbContext(DbContextOptions<WriteDbContext> options) 
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("customer");
        modelBuilder.HasPostgresEnum<DocumentType>();
        modelBuilder.HasPostgresEnum<PresenceStatus>();
        modelBuilder.HasPostgresEnum<ReservationStatus>();
        modelBuilder.HasPostgresEnum<RoomType>();
        
        var configuration = new ReadWriteConfiguration();
        modelBuilder.ApplyConfiguration<Reservation>(configuration);
        modelBuilder.ApplyConfiguration<Client>(configuration);
        modelBuilder.ApplyConfiguration<Room>(configuration);
        
        base.OnModelCreating(modelBuilder);
    }
}