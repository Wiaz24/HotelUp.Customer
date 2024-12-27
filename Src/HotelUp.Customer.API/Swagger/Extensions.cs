using Microsoft.OpenApi.Models;

namespace HotelUp.Customer.API.Swagger;

internal static class Extensions
{
    internal static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelUp.Customer", Version = "v1" });
        });
        return services;
    }

    internal static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger(c => { c.RouteTemplate = $"api/HotelUp.Customer/swagger/{{documentName}}/swagger.json"; });
        app.UseSwaggerUI(c =>
        {
            c.DocumentTitle = "HotelUp.Customer";
            c.SwaggerEndpoint($"/api/HotelUp.Customer/swagger/v1/swagger.json", "API V1");
            c.RoutePrefix = $"api/HotelUp.Customer/swagger";
        });
        return app;
    }
}