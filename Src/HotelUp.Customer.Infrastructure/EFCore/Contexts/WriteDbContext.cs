using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Infrastructure.EFCore.Config;
using HotelUp.Customer.Infrastructure.EFCore.Postgres;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelUp.Customer.Infrastructure.EFCore.Contexts;

public sealed class WriteDbContext : DbContext
{
    private readonly PostgresOptions _postgresOptions;
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Room> Rooms { get; set; }
    
    public WriteDbContext(DbContextOptions<WriteDbContext> options, IOptions<PostgresOptions> postgresOptions) 
        : base(options)
    {
        _postgresOptions = postgresOptions.Value;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddCommonConfiguration(_postgresOptions);
        base.OnModelCreating(modelBuilder);
    }
}