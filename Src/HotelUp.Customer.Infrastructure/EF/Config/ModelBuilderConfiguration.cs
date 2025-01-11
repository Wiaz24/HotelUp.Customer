using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.EF.Config;

internal static class ModelBuilderConfiguration
{
    private const string Schema = "customer";
    
    internal static ModelBuilder AddCommonConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.HasPostgresEnum<DocumentType>();
        modelBuilder.HasPostgresEnum<PresenceStatus>();
        modelBuilder.HasPostgresEnum<ReservationStatus>();
        modelBuilder.HasPostgresEnum<RoomType>();
        
        var configuration = new EntitiesConfiguration();
        modelBuilder.ApplyConfiguration<Room>(configuration);
        modelBuilder.ApplyConfiguration<Reservation>(configuration);
        modelBuilder.ApplyConfiguration<Client>(configuration);
        return modelBuilder;
    }
}