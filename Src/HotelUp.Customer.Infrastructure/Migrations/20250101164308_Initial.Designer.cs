﻿// <auto-generated />
using System;
using System.Collections.Generic;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HotelUp.Customer.Infrastructure.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    [Migration("20250101164308_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("HotelUp.Customer")
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.AdditionalCost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BillId")
                        .HasColumnType("uuid");

                    b.ComplexProperty<Dictionary<string, object>>("Price", "HotelUp.Customer.Domain.Entities.AdditionalCost.Price#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");
                        });

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.ToTable("AdditionalCosts", "HotelUp.Customer");
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.ComplexProperty<Dictionary<string, object>>("AccomodationPrice", "HotelUp.Customer.Domain.Entities.Bill.AccomodationPrice#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");
                        });

                    b.HasKey("Id");

                    b.ToTable("Bills", "HotelUp.Customer");
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Clients", "HotelUp.Customer");
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BillId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("SettlementDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("SettlementDate");

                    b.ComplexProperty<Dictionary<string, object>>("Amount", "HotelUp.Customer.Domain.Entities.Payment.Amount#Money", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");
                        });

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.ToTable("Payments", "HotelUp.Customer");
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Status");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.ComplexProperty<Dictionary<string, object>>("Period", "HotelUp.Customer.Domain.Entities.Reservation.Period#ReservationPeriod", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<DateTime>("From")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<DateTime>("To")
                                .HasColumnType("timestamp with time zone");
                        });

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Reservations", "HotelUp.Customer");
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("integer")
                        .HasColumnName("Capacity");

                    b.Property<int>("Floor")
                        .HasColumnType("integer")
                        .HasColumnName("Floor");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ImageUrl");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Type");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<bool>("WithSpecialNeeds")
                        .HasColumnType("boolean")
                        .HasColumnName("WithSpecialNeeds");

                    b.HasKey("Id");

                    b.ToTable("Rooms", "HotelUp.Customer");
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("DocumentType");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("LastName");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Pesel");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("PhoneNumber");

                    b.Property<Guid?>("ReservationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.ToTable("Tenants", "HotelUp.Customer");
                });

            modelBuilder.Entity("ReservationRoom", b =>
                {
                    b.Property<Guid>("ReservationId")
                        .HasColumnType("uuid");

                    b.Property<int>("RoomsId")
                        .HasColumnType("integer");

                    b.HasKey("ReservationId", "RoomsId");

                    b.HasIndex("RoomsId");

                    b.ToTable("ReservationRoom", "HotelUp.Customer");
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.AdditionalCost", b =>
                {
                    b.HasOne("HotelUp.Customer.Domain.Entities.Bill", null)
                        .WithMany("AdditionalCosts")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Bill", b =>
                {
                    b.HasOne("HotelUp.Customer.Domain.Entities.Reservation", null)
                        .WithOne("Bill")
                        .HasForeignKey("HotelUp.Customer.Domain.Entities.Bill", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Payment", b =>
                {
                    b.HasOne("HotelUp.Customer.Domain.Entities.Bill", null)
                        .WithMany("Payments")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Reservation", b =>
                {
                    b.HasOne("HotelUp.Customer.Domain.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Tenant", b =>
                {
                    b.HasOne("HotelUp.Customer.Domain.Entities.Reservation", null)
                        .WithMany("Tenants")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ReservationRoom", b =>
                {
                    b.HasOne("HotelUp.Customer.Domain.Entities.Reservation", null)
                        .WithMany()
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelUp.Customer.Domain.Entities.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Bill", b =>
                {
                    b.Navigation("AdditionalCosts");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Reservation", b =>
                {
                    b.Navigation("Bill");

                    b.Navigation("Tenants");
                });
#pragma warning restore 612, 618
        }
    }
}
