using HotelUp.Customer.Shared.Auth;
using HotelUp.Customer.Shared.Exceptions;
using HotelUp.Customer.Shared.Logging;
using HotelUp.Customer.Shared.Messaging;
using HotelUp.Customer.Shared.SystemsManager;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Shared;

public static class Extensions
{
    public static WebApplicationBuilder AddShared(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);
        builder.Services.AddHealthChecks();
        builder.Services.AddAuth(builder.Configuration);
        builder.Services.AddHttpClient();
        builder.Services.AddMessaging();
        builder.AddCustomSystemsManagers();
        builder.Services.AddTransient<ExceptionMiddleware>();
        builder.AddCustomLogging();
        return builder;
    }
    
    public static IApplicationBuilder UseShared(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHealthChecks("/api/customer/_health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        app.UseAuth();
        return app;
    }
}