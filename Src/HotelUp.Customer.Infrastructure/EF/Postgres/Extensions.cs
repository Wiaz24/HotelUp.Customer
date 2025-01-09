using HotelUp.Customer.Domain.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;

namespace HotelUp.Customer.Infrastructure.EF.Postgres;

public static class Extensions
{
    private const string SchemaName = "customer";
    private const string SectionName = "Postgres";
    
    public static IServiceCollection ConfigurePostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetSection(SectionName));
        
        return services;
    }
    public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
    {
        var options = services.BuildServiceProvider().GetRequiredService<IOptions<PostgresOptions>>();
        var connectionString = options.Value.ConnectionString;
        
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.MapEnum<DocumentType>($"{SchemaName}.document_type");
        dataSourceBuilder.MapEnum<PresenceStatus>($"{SchemaName}.presence_status");
        dataSourceBuilder.MapEnum<ReservationStatus>($"{SchemaName}.reservation_status");
        dataSourceBuilder.MapEnum<RoomType>($"{SchemaName}.room_type");
        var dataSource = dataSourceBuilder.Build();
        services.AddDbContext<T>(x => x.UseNpgsql(dataSource));
        
        return services;
    }
}