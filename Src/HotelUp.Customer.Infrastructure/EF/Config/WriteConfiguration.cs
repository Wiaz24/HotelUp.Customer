using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelUp.Customer.Infrastructure.EF.Config;

internal sealed class WriteConfiguration
    : IEntityTypeConfiguration<Reservation>, 
        IEntityTypeConfiguration<Client>, 
        IEntityTypeConfiguration<Room>,
        IEntityTypeConfiguration<Tenant>,
        IEntityTypeConfiguration<Bill>,
        IEntityTypeConfiguration<AdditionalCost>,
        IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property<uint>("Version")
            .IsRowVersion();

        builder.HasOne(x => x.Client)
            .WithMany();
        
        builder.Property(x => x.Status)
            .HasConversion(
                status => status.ToString(),
                status => Enum.Parse<ReservationStatus>(status))
            .HasColumnName("Status");

        builder.ComplexProperty(x => x.Period);

        builder.HasMany(x => x.Tenants)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Rooms)
            .WithMany();
        
        builder.HasOne(x => x.Bill)
            .WithOne()
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.ToTable("Reservations");
    }

    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.ToTable("Clients");
    }

    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property<uint>("Version")
            .IsRowVersion();
        
        builder.Property(x => x.Capacity)
            .HasConversion(
                cap => cap.Value,
                cap => new RoomCapacity(cap))
            .HasColumnName("Capacity");
        
        builder.Property(x => x.Floor)
            .HasConversion(
                floor => floor.Value,
                floor => new RoomFloor(floor))
            .HasColumnName("Floor");

        builder.Property(x => x.WithSpecialNeeds)
            .HasColumnName("WithSpecialNeeds");
        
        builder.Property(x => x.Type)
            .HasConversion(
                type => type.ToString(),
                type => Enum.Parse<RoomType>(type))
            .HasColumnName("Type");
        
        builder.Property(x => x.ImageUrl)
            .HasConversion(
                url => url.Value,
                url => new ImageUrl(url))
            .HasColumnName("ImageUrl");
        
        builder.ToTable("Rooms");
    }

    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.FirstName)
            .HasConversion(
                name => name.Value,
                name => new FirstName(name))
            .HasColumnName("FirstName");
        
        builder.Property(x => x.LastName)
            .HasConversion(
                name => name.Value,
                name => new LastName(name))
            .HasColumnName("LastName");
        
        builder.Property(x => x.PhoneNumber)
            .HasConversion(
                phone => phone.Value,
                phone => new PhoneNumber(phone))
            .HasColumnName("PhoneNumber");
        
        builder.Property(x => x.Email)
            .HasConversion(
                email => email.Value,
                email => new Email(email))
            .HasColumnName("Email");
        
        builder.Property(x => x.Pesel)
            .HasConversion(
                pesel => pesel.Value,
                pesel => new Pesel(pesel))
            .HasColumnName("Pesel");
        
        builder.Property(x => x.DocumentType)
            .HasConversion(
                type => type.ToString(),
                type => Enum.Parse<DocumentType>(type))
            .HasColumnName("DocumentType");
        
        builder.Property(x => x.Status)
            .HasConversion(
                status => status.ToString(),
                status => Enum.Parse<PresenceStatus>(status))
            .HasColumnName("Status");
        
        builder.ToTable("Tenants");
    }

    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ComplexProperty(x => x.AccomodationPrice);
        
        builder.HasMany(x => x.AdditionalCosts)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Payments)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.ToTable("Bills");
    }

    public void Configure(EntityTypeBuilder<AdditionalCost> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.ComplexProperty(x => x.Price);
                
        builder.ToTable("AdditionalCosts");
    }

    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.ComplexProperty(x => x.Amount);
        
        builder.Property(x => x.SettlementDate)
            .HasColumnName("SettlementDate");
        
        builder.ToTable("Payments");
    }
}