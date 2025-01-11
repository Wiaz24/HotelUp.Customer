using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.ValueObjects;
using HotelUp.Customer.Infrastructure.EF.CustomExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        
        builder.Property(x => x.Capacity)
            .HasConversion(
                cap => cap.Value,
                cap => new RoomCapacity(cap));

        builder.Property(x => x.Floor)
            .HasConversion(
                floor => floor.Value,
                floor => new RoomFloor(floor));

        builder.Property(x => x.WithSpecialNeeds);

        builder.Property(x => x.Type);

        builder.Property(x => x.ImageUrl)
            .HasConversion(
                url => url.Value,
                url => new ImageUrl(url));
        
        builder.ToTable($"{nameof(Room)}s");
    }
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property<uint>("Version")
            .IsRowVersion();
        
        builder.Property(x => x.Status);
        
        builder.HasValueObject(x => x.Period);
        
        builder.ComplexProperty(x => x.Period, cp =>
        {
            cp.Property(p => p.From)
                .HasConversion(
                    d => DateTime.SpecifyKind(d, DateTimeKind.Utc),
                    d => d);
            cp.Property(p => p.To)
                .HasConversion(
                    d => DateTime.SpecifyKind(d, DateTimeKind.Utc),
                    d => d);
        });

        builder.HasMany(x => x.Rooms)
            .WithMany();
        
        builder.HasOne(x => x.Client)
            .WithMany();
        
        builder.OwnsOne(x => x.Bill, b =>
        {
            b.WithOwner();
            
            b.Property(p => p.AccomodationPrice)
                .HasConversion<MoneyConverter>();
            
            b.OwnsMany(x => x.Payments, pb =>
            {
                pb.WithOwner()
                    .HasPrincipalKey(p => p.Id);

                pb.Property(p => p.Amount)
                    .HasConversion<MoneyConverter>();
                
                pb.Property(p => p.SettlementDate)
                    .HasConversion(
                        date => DateTime.SpecifyKind(date, DateTimeKind.Utc),
                        date => date);
            });
            
            b.OwnsMany(x => x.AdditionalCosts, ac =>
            {
                ac.WithOwner()
                    .HasPrincipalKey(p => p.Id);

                ac.Property(p => p.Price)
                    .HasConversion<MoneyConverter>();
            });
            
            b.ToTable($"{nameof(Bill)}s");
        });
        
        builder.OwnsMany(x => x.Tenants, tb =>
        {
            tb.WithOwner()
                .HasPrincipalKey(p => p.Id);

            tb.Property(x => x.FirstName)
                .HasConversion<FirstNameConverter>();

            tb.Property(x => x.LastName)
                .HasConversion<LastNameConverter>();

            tb.Property(x => x.PhoneNumber)
                .HasConversion<PhoneNumberConverter>();

            tb.HasValueObject(x => x.Email);

            tb.Property(x => x.Pesel)
                .HasConversion<PeselConverter>();

            tb.Property(x => x.DocumentType);
            tb.Property(x => x.Status);
            
            tb.ToTable($"{nameof(Tenant)}s");
        });
        
        builder.ToTable($"{nameof(Reservation)}s");
    }
}