// using HotelUp.Customer.Domain.Consts;
// using HotelUp.Customer.Infrastructure.EF.Models;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace HotelUp.Customer.Infrastructure.EF.Config;
//
// internal sealed class ReadConfiguration
//     : IEntityTypeConfiguration<AdditionalCostReadModel>,
//         IEntityTypeConfiguration<BillReadModel>,
//         IEntityTypeConfiguration<ClientReadModel>,
//         IEntityTypeConfiguration<PaymentReadModel>,
//         IEntityTypeConfiguration<ReservationReadModel>,
//         IEntityTypeConfiguration<RoomReadModel>,
//         IEntityTypeConfiguration<TenantReadModel>
//
// {
//     public void Configure(EntityTypeBuilder<AdditionalCostReadModel> builder)
//     {
//         builder.HasKey(x => x.Id);
//
//         builder.ComplexProperty(x => x.Price);
//         
//         builder.ToTable("AdditionalCosts");
//     }
//
//     public void Configure(EntityTypeBuilder<BillReadModel> builder)
//     {
//         builder.HasKey(x => x.Id);
//
//         builder.ComplexProperty(x => x.AccomodationPrice);
//         
//         builder.OwnsMany(x => x.AdditionalCosts);
//         
//         builder.OwnsMany(x => x.Payments);
//         
//         builder.ToTable("Bills");
//     }
//
//     public void Configure(EntityTypeBuilder<ClientReadModel> builder)
//     {
//         builder.HasKey(x => x.Id);
//         
//         builder.ToTable("Clients");
//     }
//
//     public void Configure(EntityTypeBuilder<PaymentReadModel> builder)
//     {
//         builder.HasKey(x => x.Id);
//         
//         builder.ComplexProperty(x => x.Amount);
//         
//         builder.Property(x => x.SettlementDate)
//             .HasConversion(
//                 d => DateTime.SpecifyKind(d, DateTimeKind.Utc),
//                 d => d);
//         
//         builder.ToTable("Payments");
//     }
//
//     public void Configure(EntityTypeBuilder<ReservationReadModel> builder)
//     {
//         builder.HasKey(x => x.Id);
//
//         builder.HasOne(x => x.Client)
//             .WithMany();
//         
//         builder.Property(x => x.Status)
//             .HasConversion(
//                 s => s.ToString(), 
//                 s => Enum.Parse<ReservationStatus>(s));
//         
//         builder.ComplexProperty(x => x.Period)
//             .Property(p => p.From)
//             .HasConversion(
//                 d => DateTime.SpecifyKind(d, DateTimeKind.Utc),
//                 d => d);
//         builder.ComplexProperty(x => x.Period)
//             .Property(p => p.To)
//             .HasConversion(
//                 d => DateTime.SpecifyKind(d, DateTimeKind.Utc),
//                 d => d);
//         
//         builder.OwnsMany(x => x.Tenants);
//         
//         builder.HasMany(x => x.Rooms)
//             .WithMany();
//
//         builder.OwnsOne(x => x.Bill);
//         
//         builder.ToTable("Reservations");
//     }
//
//     public void Configure(EntityTypeBuilder<RoomReadModel> builder)
//     {
//         builder.HasKey(x => x.Id);
//
//         builder.Property(x => x.Capacity);
//         
//         builder.Property(x => x.Floor);
//         
//         builder.Property(x => x.WithSpecialNeeds);
//         
//         builder.Property(x => x.Type)
//             .HasConversion(
//                 t => t.ToString(),
//                 t => Enum.Parse<RoomType>(t));
//         
//         builder.Property(x => x.ImageUrl);
//         
//         builder.ToTable("Rooms");
//
//     }
//
//     public void Configure(EntityTypeBuilder<TenantReadModel> builder)
//     {
//         builder.HasKey(x => x.Id);
//         
//         builder.Property(x => x.FirstName);
//         
//         builder.Property(x => x.LastName);
//         
//         builder.Property(x => x.PhoneNumber);
//         
//         builder.Property(x => x.Email);
//         
//         builder.Property(x => x.Pesel);
//         
//         builder.Property(x => x.DocumentType)
//             .HasConversion(
//                 t => t.ToString(),
//                 t => Enum.Parse<DocumentType>(t));
//         
//         builder.Property(x => x.Status)
//             .HasConversion(
//                 s => s.ToString(),
//                 s => Enum.Parse<PresenceStatus>(s));
//         
//         builder.ToTable("Tenants");
//     }
// }