using HotelUp.Customer.Shared.Messaging.RabbitMQ;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.RabbitMq;
using Extensions = HotelUp.Customer.Shared.Messaging.Extensions;

namespace HotelUp.Customer.Tests.Integration.Utils;

public static class MockRabbitMq
{
    public static void AddMockRabbitMq(this IServiceCollection services, 
        RabbitMqContainer dbContainer)
    {
        var options = new RabbitMqOptions
        {
            Host = dbContainer.Hostname,
            UserName = "guest",
            Password = "guest"
        };
        var assembliesWithConsumers = Extensions.GetAssembliesWithConsumers();
        services.RemoveMassTransitHostedService();
        
        var massTransitServices = services
            .Where(s => s.ServiceType.FullName!.StartsWith("MassTransit"))
            .ToList();
        foreach (var service in massTransitServices)
        {
            services.Remove(service);
        }
        
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.AddConsumers(assembliesWithConsumers);
            busConfigurator.UsingRabbitMq((context, rabbitMqConfigurator) =>
            {
                rabbitMqConfigurator.Host(options.Host, hostConfigurator =>
                {
                    hostConfigurator.Username(options.UserName);
                    hostConfigurator.Password(options.Password);
                });
                rabbitMqConfigurator.ConfigureEndpoints(context);
            });
        });
    }
}