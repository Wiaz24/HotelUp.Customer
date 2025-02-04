﻿using AppAny.Quartz.EntityFrameworkCore.Migrations;
using AppAny.Quartz.EntityFrameworkCore.Migrations.PostgreSQL;

using HotelUp.Customer.Domain.Entities;
using HotelUp.Customer.Infrastructure.EFCore.Postgres;

using Microsoft.EntityFrameworkCore;

namespace HotelUp.Customer.Infrastructure.EFCore.Config;

internal static class ModelBuilderConfiguration
{
    internal static ModelBuilder AddCommonConfiguration(this ModelBuilder modelBuilder, PostgresOptions options)
    {
        var schemaName = options.SchemaName;
        modelBuilder.HasDefaultSchema(schemaName);
        modelBuilder.AddQuartz(builder => builder.UsePostgreSql(schema: schemaName));
        
        var configuration = new EntitiesConfiguration();
        modelBuilder.ApplyConfiguration<Room>(configuration);
        modelBuilder.ApplyConfiguration<Reservation>(configuration);
        modelBuilder.ApplyConfiguration<Client>(configuration);
        return modelBuilder;
    }
}