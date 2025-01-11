using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Infrastructure.EF.Config;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.EF.Contexts;

public sealed class WriteDbContext : DbContext
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
        modelBuilder.AddCommonConfiguration();
        base.OnModelCreating(modelBuilder);
    }
}