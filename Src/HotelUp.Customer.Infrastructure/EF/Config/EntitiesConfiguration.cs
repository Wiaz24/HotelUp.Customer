using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Infrastructure.EF.CustomExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelUp.Customer.Infrastructure.EF.Config;

internal sealed class EntitiesConfiguration
    : IEntityTypeConfiguration<Reservation>, 
        IEntityTypeConfiguration<Client>, 
        IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.ToTable($"{nameof(Client)}s");
    }
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property<uint>("Version")
            .IsRowVersion();
        
        builder.Property(x => x.Type);
        builder.Property(x => x.WithSpecialNeeds);
        builder.HasValueObject(x => x.Capacity);
        builder.HasValueObject(x => x.Floor);
        builder.HasValueObject(x => x.ImageUrl)?
            .HasMaxLength(255);

        builder.ToTable($"{nameof(Room)}s");
    }
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property<uint>("Version")
            .IsRowVersion();
        
        builder.Property(x => x.Status);
        builder.HasValueObject(x => x.Period);

        builder.HasMany(x => x.Rooms)
            .WithMany();
        
        builder.HasOne(x => x.Client)
            .WithMany();
        
        builder.OwnsOne(x => x.Bill, b =>
        {
            b.WithOwner();
            b.HasValueObject(x => x.AccomodationPrice);
            b.OwnsMany(x => x.Payments, pb =>
            {
                pb.WithOwner()
                    .HasPrincipalKey(p => p.Id);

                pb.HasValueObject(x => x.Amount);
                pb.HasValueObject(x => x.SettlementDate);
            });
            
            b.OwnsMany(x => x.AdditionalCosts, ac =>
            {
                ac.WithOwner()
                    .HasPrincipalKey(p => p.Id);
                
                ac.HasValueObject(p => p.Price);
            });
            
            b.ToTable($"{nameof(Bill)}s");
        });
        
        builder.OwnsMany(x => x.Tenants, tb =>
        {
            tb.WithOwner()
                .HasPrincipalKey(p => p.Id);

            tb.Property(x => x.DocumentType);
            tb.Property(x => x.Status);
            tb.HasValueObject(x => x.FirstName)?
                .HasMaxLength(50);
            tb.HasValueObject(x => x.LastName)?
                .HasMaxLength(50);
            tb.HasValueObject(x => x.PhoneNumber)?
                .HasMaxLength(15);
            tb.HasValueObject(x => x.Email)?
                .HasMaxLength(50);
            tb.HasValueObject(x => x.Pesel)?
                .HasMaxLength(11);
            
            tb.ToTable($"{nameof(Tenant)}s");
        });
        
        builder.ToTable($"{nameof(Reservation)}s");
    }
}