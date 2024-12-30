using HotelUp.Customer.Infrastructure.EF.Config;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.EF.Contexts;

public class WriteDbContext : DbContext
{
    // public DbSet<Entity> Entities { get; set; }
    
    public WriteDbContext(DbContextOptions<WriteDbContext> options) 
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("HotelUp.Customer");
        
        var configuration = new WriteConfiguration();
        // modelBuilder.ApplyConfiguration<Entity>(configuration);
        
        base.OnModelCreating(modelBuilder);
    }
}