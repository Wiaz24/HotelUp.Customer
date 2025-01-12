﻿// <auto-generated />
using System;
using System.Collections.Generic;
using HotelUp.Customer.Domain.Consts;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HotelUp.Customer.Infrastructure.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    partial class WriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("customer")
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "document_type", new[] { "passport", "id_card" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "presence_status", new[] { "pending", "checked_in", "checked_out" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "reservation_status", new[] { "valid", "canceled" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "room_type", new[] { "economy", "basic", "premium" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzBlobTrigger", b =>
                {
                    b.Property<string>("SchedulerName")
                        .HasColumnType("text")
                        .HasColumnName("sched_name");

                    b.Property<string>("TriggerName")
                        .HasColumnType("text")
                        .HasColumnName("trigger_name");

                    b.Property<string>("TriggerGroup")
                        .HasColumnType("text")
                        .HasColumnName("trigger_group");

                    b.Property<byte[]>("BlobData")
                        .HasColumnType("bytea")
                        .HasColumnName("blob_data");

                    b.HasKey("SchedulerName", "TriggerName", "TriggerGroup");

                    b.ToTable("qrtz_blob_triggers", "customer");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzCalendar", b =>
                {
                    b.Property<string>("SchedulerName")
                        .HasColumnType("text")
                        .HasColumnName("sched_name");

                    b.Property<string>("CalendarName")
                        .HasColumnType("text")
                        .HasColumnName("calendar_name");

                    b.Property<byte[]>("Calendar")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("calendar");

                    b.HasKey("SchedulerName", "CalendarName");

                    b.ToTable("qrtz_calendars", "customer");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzCronTrigger", b =>
                {
                    b.Property<string>("SchedulerName")
                        .HasColumnType("text")
                        .HasColumnName("sched_name");

                    b.Property<string>("TriggerName")
                        .HasColumnType("text")
                        .HasColumnName("trigger_name");

                    b.Property<string>("TriggerGroup")
                        .HasColumnType("text")
                        .HasColumnName("trigger_group");

                    b.Property<string>("CronExpression")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cron_expression");

                    b.Property<string>("TimeZoneId")
                        .HasColumnType("text")
                        .HasColumnName("time_zone_id");

                    b.HasKey("SchedulerName", "TriggerName", "TriggerGroup");

                    b.ToTable("qrtz_cron_triggers", "customer");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzFiredTrigger", b =>
                {
                    b.Property<string>("SchedulerName")
                        .HasColumnType("text")
                        .HasColumnName("sched_name");

                    b.Property<string>("EntryId")
                        .HasColumnType("text")
                        .HasColumnName("entry_id");

                    b.Property<long>("FiredTime")
                        .HasColumnType("bigint")
                        .HasColumnName("fired_time");

                    b.Property<string>("InstanceName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("instance_name");

                    b.Property<bool>("IsNonConcurrent")
                        .HasColumnType("bool")
                        .HasColumnName("is_nonconcurrent");

                    b.Property<string>("JobGroup")
                        .HasColumnType("text")
                        .HasColumnName("job_group");

                    b.Property<string>("JobName")
                        .HasColumnType("text")
                        .HasColumnName("job_name");

                    b.Property<int>("Priority")
                        .HasColumnType("integer")
                        .HasColumnName("priority");

                    b.Property<bool?>("RequestsRecovery")
                        .HasColumnType("bool")
                        .HasColumnName("requests_recovery");

                    b.Property<long>("ScheduledTime")
                        .HasColumnType("bigint")
                        .HasColumnName("sched_time");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("state");

                    b.Property<string>("TriggerGroup")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("trigger_group");

                    b.Property<string>("TriggerName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("trigger_name");

                    b.HasKey("SchedulerName", "EntryId");

                    b.HasIndex("InstanceName")
                        .HasDatabaseName("idx_qrtz_ft_trig_inst_name");

                    b.HasIndex("JobGroup")
                        .HasDatabaseName("idx_qrtz_ft_job_group");

                    b.HasIndex("JobName")
                        .HasDatabaseName("idx_qrtz_ft_job_name");

                    b.HasIndex("RequestsRecovery")
                        .HasDatabaseName("idx_qrtz_ft_job_req_recovery");

                    b.HasIndex("TriggerGroup")
                        .HasDatabaseName("idx_qrtz_ft_trig_group");

                    b.HasIndex("TriggerName")
                        .HasDatabaseName("idx_qrtz_ft_trig_name");

                    b.HasIndex("SchedulerName", "TriggerName", "TriggerGroup")
                        .HasDatabaseName("idx_qrtz_ft_trig_nm_gp");

                    b.ToTable("qrtz_fired_triggers", "customer");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzJobDetail", b =>
                {
                    b.Property<string>("SchedulerName")
                        .HasColumnType("text")
                        .HasColumnName("sched_name");

                    b.Property<string>("JobName")
                        .HasColumnType("text")
                        .HasColumnName("job_name");

                    b.Property<string>("JobGroup")
                        .HasColumnType("text")
                        .HasColumnName("job_group");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsDurable")
                        .HasColumnType("bool")
                        .HasColumnName("is_durable");

                    b.Property<bool>("IsNonConcurrent")
                        .HasColumnType("bool")
                        .HasColumnName("is_nonconcurrent");

                    b.Property<bool>("IsUpdateData")
                        .HasColumnType("bool")
                        .HasColumnName("is_update_data");

                    b.Property<string>("JobClassName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("job_class_name");

                    b.Property<byte[]>("JobData")
                        .HasColumnType("bytea")
                        .HasColumnName("job_data");

                    b.Property<bool>("RequestsRecovery")
                        .HasColumnType("bool")
                        .HasColumnName("requests_recovery");

                    b.HasKey("SchedulerName", "JobName", "JobGroup");

                    b.HasIndex("RequestsRecovery")
                        .HasDatabaseName("idx_qrtz_j_req_recovery");

                    b.ToTable("qrtz_job_details", "customer");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzLock", b =>
                {
                    b.Property<string>("SchedulerName")
                        .HasColumnType("text")
                        .HasColumnName("sched_name");

                    b.Property<string>("LockName")
                        .HasColumnType("text")
                        .HasColumnName("lock_name");

                    b.HasKey("SchedulerName", "LockName");

                    b.ToTable("qrtz_locks", "customer");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzPausedTriggerGroup", b =>
                {
                    b.Property<string>("SchedulerName")
                        .HasColumnType("text")
                        .HasColumnName("sched_name");

                    b.Property<string>("TriggerGroup")
                        .HasColumnType("text")
                        .HasColumnName("trigger_group");

                    b.HasKey("SchedulerName", "TriggerGroup");

                    b.ToTable("qrtz_paused_trigger_grps", "customer");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzSchedulerState", b =>
                {
                    b.Property<string>("SchedulerName")
                        .HasColumnType("text")
                        .HasColumnName("sched_name");

                    b.Property<string>("InstanceName")
                        .HasColumnType("text")
                        .HasColumnName("instance_name");

                    b.Property<long>("CheckInInterval")
                        .HasColumnType("bigint")
                        .HasColumnName("checkin_interval");

                    b.Property<long>("LastCheckInTime")
                        .HasColumnType("bigint")
                        .HasColumnName("last_checkin_time");

                    b.HasKey("SchedulerName", "InstanceName");

                    b.ToTable("qrtz_scheduler_state", "customer");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzSimplePropertyTrigger", b =>
                {
                    b.Property<string>("SchedulerName")
                        .HasColumnType("text")
                        .HasColumnName("sched_name");

                    b.Property<string>("TriggerName")
                        .HasColumnType("text")
                        .HasColumnName("trigger_name");

                    b.Property<string>("TriggerGroup")
                        .HasColumnType("text")
                        .HasColumnName("trigger_group");

                    b.Property<bool?>("BooleanProperty1")
                        .HasColumnType("bool")
                        .HasColumnName("bool_prop_1");

                    b.Property<bool?>("BooleanProperty2")
                        .HasColumnType("bool")
                        .HasColumnName("bool_prop_2");

                    b.Property<decimal?>("DecimalProperty1")
                        .HasColumnType("numeric")
                        .HasColumnName("dec_prop_1");

                    b.Property<decimal?>("DecimalProperty2")
                        .HasColumnType("numeric")
                        .HasColumnName("dec_prop_2");

                    b.Property<int?>("IntegerProperty1")
                        .HasColumnType("integer")
                        .HasColumnName("int_prop_1");

                    b.Property<int?>("IntegerProperty2")
                        .HasColumnType("integer")
                        .HasColumnName("int_prop_2");

                    b.Property<long?>("LongProperty1")
                        .HasColumnType("bigint")
                        .HasColumnName("long_prop_1");

                    b.Property<long?>("LongProperty2")
                        .HasColumnType("bigint")
                        .HasColumnName("long_prop_2");

                    b.Property<string>("StringProperty1")
                        .HasColumnType("text")
                        .HasColumnName("str_prop_1");

                    b.Property<string>("StringProperty2")
                        .HasColumnType("text")
                        .HasColumnName("str_prop_2");

                    b.Property<string>("StringProperty3")
                        .HasColumnType("text")
                        .HasColumnName("str_prop_3");

                    b.Property<string>("TimeZoneId")
                        .HasColumnType("text")
                        .HasColumnName("time_zone_id");

                    b.HasKey("SchedulerName", "TriggerName", "TriggerGroup");

                    b.ToTable("qrtz_simprop_triggers", "customer");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzSimpleTrigger", b =>
                {
                    b.Property<string>("SchedulerName")
                        .HasColumnType("text")
                        .HasColumnName("sched_name");

                    b.Property<string>("TriggerName")
                        .HasColumnType("text")
                        .HasColumnName("trigger_name");

                    b.Property<string>("TriggerGroup")
                        .HasColumnType("text")
                        .HasColumnName("trigger_group");

                    b.Property<long>("RepeatCount")
                        .HasColumnType("bigint")
                        .HasColumnName("repeat_count");

                    b.Property<long>("RepeatInterval")
                        .HasColumnType("bigint")
                        .HasColumnName("repeat_interval");

                    b.Property<long>("TimesTriggered")
                        .HasColumnType("bigint")
                        .HasColumnName("times_triggered");

                    b.HasKey("SchedulerName", "TriggerName", "TriggerGroup");

                    b.ToTable("qrtz_simple_triggers", "customer");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzTrigger", b =>
                {
                    b.Property<string>("SchedulerName")
                        .HasColumnType("text")
                        .HasColumnName("sched_name");

                    b.Property<string>("TriggerName")
                        .HasColumnType("text")
                        .HasColumnName("trigger_name");

                    b.Property<string>("TriggerGroup")
                        .HasColumnType("text")
                        .HasColumnName("trigger_group");

                    b.Property<string>("CalendarName")
                        .HasColumnType("text")
                        .HasColumnName("calendar_name");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<long?>("EndTime")
                        .HasColumnType("bigint")
                        .HasColumnName("end_time");

                    b.Property<byte[]>("JobData")
                        .HasColumnType("bytea")
                        .HasColumnName("job_data");

                    b.Property<string>("JobGroup")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("job_group");

                    b.Property<string>("JobName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("job_name");

                    b.Property<short?>("MisfireInstruction")
                        .HasColumnType("smallint")
                        .HasColumnName("misfire_instr");

                    b.Property<long?>("NextFireTime")
                        .HasColumnType("bigint")
                        .HasColumnName("next_fire_time");

                    b.Property<long?>("PreviousFireTime")
                        .HasColumnType("bigint")
                        .HasColumnName("prev_fire_time");

                    b.Property<int?>("Priority")
                        .HasColumnType("integer")
                        .HasColumnName("priority");

                    b.Property<long>("StartTime")
                        .HasColumnType("bigint")
                        .HasColumnName("start_time");

                    b.Property<string>("TriggerState")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("trigger_state");

                    b.Property<string>("TriggerType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("trigger_type");

                    b.HasKey("SchedulerName", "TriggerName", "TriggerGroup");

                    b.HasIndex("NextFireTime")
                        .HasDatabaseName("idx_qrtz_t_next_fire_time");

                    b.HasIndex("TriggerState")
                        .HasDatabaseName("idx_qrtz_t_state");

                    b.HasIndex("NextFireTime", "TriggerState")
                        .HasDatabaseName("idx_qrtz_t_nft_st");

                    b.HasIndex("SchedulerName", "JobName", "JobGroup");

                    b.ToTable("qrtz_triggers", "customer");
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Clients", "customer");
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<ReservationStatus>("Status")
                        .HasColumnType("customer.reservation_status");

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

                    b.ToTable("Reservations", "customer");
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<int>("Floor")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<RoomType>("Type")
                        .HasColumnType("customer.room_type");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<bool>("WithSpecialNeeds")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Rooms", "customer");
                });

            modelBuilder.Entity("ReservationRoom", b =>
                {
                    b.Property<Guid>("ReservationId")
                        .HasColumnType("uuid");

                    b.Property<int>("RoomsId")
                        .HasColumnType("integer");

                    b.HasKey("ReservationId", "RoomsId");

                    b.HasIndex("RoomsId");

                    b.ToTable("ReservationRoom", "customer");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzBlobTrigger", b =>
                {
                    b.HasOne("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzTrigger", "Trigger")
                        .WithMany("BlobTriggers")
                        .HasForeignKey("SchedulerName", "TriggerName", "TriggerGroup")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trigger");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzCronTrigger", b =>
                {
                    b.HasOne("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzTrigger", "Trigger")
                        .WithMany("CronTriggers")
                        .HasForeignKey("SchedulerName", "TriggerName", "TriggerGroup")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trigger");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzSimplePropertyTrigger", b =>
                {
                    b.HasOne("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzTrigger", "Trigger")
                        .WithMany("SimplePropertyTriggers")
                        .HasForeignKey("SchedulerName", "TriggerName", "TriggerGroup")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trigger");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzSimpleTrigger", b =>
                {
                    b.HasOne("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzTrigger", "Trigger")
                        .WithMany("SimpleTriggers")
                        .HasForeignKey("SchedulerName", "TriggerName", "TriggerGroup")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trigger");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzTrigger", b =>
                {
                    b.HasOne("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzJobDetail", "JobDetail")
                        .WithMany("Triggers")
                        .HasForeignKey("SchedulerName", "JobName", "JobGroup")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobDetail");
                });

            modelBuilder.Entity("HotelUp.Customer.Domain.Entities.Reservation", b =>
                {
                    b.HasOne("HotelUp.Customer.Domain.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("HotelUp.Customer.Domain.Entities.Reservation.Bill#HotelUp.Customer.Domain.Entities.Bill", "Bill", b1 =>
                        {
                            b1.Property<Guid>("ReservationId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.HasKey("ReservationId");

                            b1.ToTable("Bills", "customer");

                            b1.WithOwner()
                                .HasForeignKey("ReservationId");

                            b1.OwnsMany("HotelUp.Customer.Domain.Entities.Reservation.Bill#HotelUp.Customer.Domain.Entities.Bill.AdditionalCosts#HotelUp.Customer.Domain.Entities.AdditionalCost", "AdditionalCosts", b2 =>
                                {
                                    b2.Property<Guid>("BillId")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("uuid");

                                    b2.HasKey("BillId", "Id");

                                    b2.ToTable("AdditionalCost", "customer");

                                    b2.WithOwner()
                                        .HasForeignKey("BillId")
                                        .HasPrincipalKey("Id");

                                    b2.OwnsOne("HotelUp.Customer.Domain.Entities.Reservation.Bill#HotelUp.Customer.Domain.Entities.Bill.AdditionalCosts#HotelUp.Customer.Domain.Entities.AdditionalCost.Price#HotelUp.Customer.Domain.ValueObjects.Money", "Price", b3 =>
                                        {
                                            b3.Property<Guid>("AdditionalCostBillId")
                                                .HasColumnType("uuid");

                                            b3.Property<Guid>("AdditionalCostId")
                                                .HasColumnType("uuid");

                                            b3.Property<decimal>("Amount")
                                                .HasColumnType("numeric");

                                            b3.Property<string>("Currency")
                                                .IsRequired()
                                                .HasColumnType("text");

                                            b3.HasKey("AdditionalCostBillId", "AdditionalCostId");

                                            b3.ToTable("AdditionalCost", "customer");

                                            b3.WithOwner()
                                                .HasForeignKey("AdditionalCostBillId", "AdditionalCostId");
                                        });

                                    b2.Navigation("Price")
                                        .IsRequired();
                                });

                            b1.OwnsMany("HotelUp.Customer.Domain.Entities.Reservation.Bill#HotelUp.Customer.Domain.Entities.Bill.Payments#HotelUp.Customer.Domain.Entities.Payment", "Payments", b2 =>
                                {
                                    b2.Property<Guid>("BillId")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("uuid");

                                    b2.Property<DateTime>("SettlementDate")
                                        .HasColumnType("timestamp with time zone");

                                    b2.HasKey("BillId", "Id");

                                    b2.ToTable("Payment", "customer");

                                    b2.WithOwner()
                                        .HasForeignKey("BillId")
                                        .HasPrincipalKey("Id");

                                    b2.OwnsOne("HotelUp.Customer.Domain.Entities.Reservation.Bill#HotelUp.Customer.Domain.Entities.Bill.Payments#HotelUp.Customer.Domain.Entities.Payment.Amount#HotelUp.Customer.Domain.ValueObjects.Money", "Amount", b3 =>
                                        {
                                            b3.Property<Guid>("PaymentBillId")
                                                .HasColumnType("uuid");

                                            b3.Property<Guid>("PaymentId")
                                                .HasColumnType("uuid");

                                            b3.Property<decimal>("Amount")
                                                .HasColumnType("numeric");

                                            b3.Property<string>("Currency")
                                                .IsRequired()
                                                .HasColumnType("text");

                                            b3.HasKey("PaymentBillId", "PaymentId");

                                            b3.ToTable("Payment", "customer");

                                            b3.WithOwner()
                                                .HasForeignKey("PaymentBillId", "PaymentId");
                                        });

                                    b2.Navigation("Amount")
                                        .IsRequired();
                                });

                            b1.OwnsOne("HotelUp.Customer.Domain.Entities.Reservation.Bill#HotelUp.Customer.Domain.Entities.Bill.AccomodationPrice#HotelUp.Customer.Domain.ValueObjects.Money", "AccomodationPrice", b2 =>
                                {
                                    b2.Property<Guid>("BillReservationId")
                                        .HasColumnType("uuid");

                                    b2.Property<decimal>("Amount")
                                        .HasColumnType("numeric");

                                    b2.Property<string>("Currency")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("BillReservationId");

                                    b2.ToTable("Bills", "customer");

                                    b2.WithOwner()
                                        .HasForeignKey("BillReservationId");
                                });

                            b1.Navigation("AccomodationPrice")
                                .IsRequired();

                            b1.Navigation("AdditionalCosts");

                            b1.Navigation("Payments");
                        });

                    b.OwnsMany("HotelUp.Customer.Domain.Entities.Reservation.Tenants#HotelUp.Customer.Domain.Entities.Tenant", "Tenants", b1 =>
                        {
                            b1.Property<Guid>("ReservationId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<DocumentType?>("DocumentType")
                                .HasColumnType("customer.document_type");

                            b1.Property<string>("Email")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("FirstName")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("LastName")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("Pesel")
                                .HasMaxLength(11)
                                .HasColumnType("character varying(11)");

                            b1.Property<string>("PhoneNumber")
                                .HasMaxLength(15)
                                .HasColumnType("character varying(15)");

                            b1.Property<PresenceStatus>("Status")
                                .HasColumnType("customer.presence_status");

                            b1.HasKey("ReservationId", "Id");

                            b1.ToTable("Tenants", "customer");

                            b1.WithOwner()
                                .HasForeignKey("ReservationId");
                        });

                    b.Navigation("Bill");

                    b.Navigation("Client");

                    b.Navigation("Tenants");
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

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzJobDetail", b =>
                {
                    b.Navigation("Triggers");
                });

            modelBuilder.Entity("AppAny.Quartz.EntityFrameworkCore.Migrations.QuartzTrigger", b =>
                {
                    b.Navigation("BlobTriggers");

                    b.Navigation("CronTriggers");

                    b.Navigation("SimplePropertyTriggers");

                    b.Navigation("SimpleTriggers");
                });
#pragma warning restore 612, 618
        }
    }
}
