using HotelUp.Customer.Infrastructure.EF.Config;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.EF.Contexts;

public class ReadDbContext : DbContext
{
    // public DbSet<ReadDbModel> Reads { get; set; }
    
    public ReadDbContext(DbContextOptions<ReadDbContext> options) 
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("HotelUp.Customer");
        
        var configuration = new ReadConfiguration();
        // modelBuilder.ApplyConfiguration<ReadDbModel>(configuration);
        
        base.OnModelCreating(modelBuilder);
    }
}