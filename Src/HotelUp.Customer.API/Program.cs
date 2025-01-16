using System.Text.Json.Serialization;

using Delta;

using HotelUp.Customer.API.Cors;
using HotelUp.Customer.API.Swagger;
using HotelUp.Customer.Application;
using HotelUp.Customer.Domain;
using HotelUp.Customer.Infrastructure;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using HotelUp.Customer.Shared;

var builder = WebApplication.CreateBuilder(args);


builder.AddShared();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddCustomSwagger(builder.Configuration);
builder.Services.AddCorsForFrontend(builder.Configuration);
builder.Services.AddDomain(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.UseDelta();
app.UseShared();
app.UseCustomSwagger();
app.UseCorsForFrontend();
app.MapControllers();
app.Run();

public interface IApiMarker
{
}