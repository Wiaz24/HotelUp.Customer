using System.Net.Http.Headers;
using System.Security.Claims;
using HotelUp.Customer.Infrastructure.EF.Contexts;
using HotelUp.Customer.Tests.Integration.Utils;
using MassTransit;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace HotelUp.Customer.Tests.Integration;

public abstract class ControllerTestsBase : IClassFixture<TestWebAppFactory>
{
    protected TestWebAppFactory Factory { get; }
    protected readonly IServiceProvider _serviceProvider;
    
    protected ControllerTestsBase(TestWebAppFactory factory)
    {
        Factory = factory;
        _serviceProvider = factory.Services;
    }
}