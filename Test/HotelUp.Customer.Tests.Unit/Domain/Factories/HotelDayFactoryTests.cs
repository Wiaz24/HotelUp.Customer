using HotelUp.Customer.Domain.Factories;
using HotelUp.Customer.Domain.Factories.Options;
using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;

namespace HotelUp.Customer.Unit.Domain.Factories;

public class HotelDayFactoryTests
{
    private readonly IOptionsSnapshot<HotelDayOptions> _options;
    
    public HotelDayFactoryTests()
    {
        _options = Substitute.For<IOptionsSnapshot<HotelDayOptions>>();
    }
    
    [Fact]
    public void Create_WithCorrectOptions_ShouldCreate()
    {
        // Arrange
        var hotelDayOptions = new HotelDayOptions
        {
            StartHour = new TimeOnly(14,00),
            EndHour = new TimeOnly(10,00)
        };
        _options.Value.Returns(hotelDayOptions);
        var hotelDayFactory = new HotelDayFactory(_options);
        
        // Act
        var hotelDay = hotelDayFactory.Create();
        
        // Assert
        hotelDay.ShouldNotBeNull();
        hotelDay.StartHour.ShouldBe(hotelDayOptions.StartHour);
        hotelDay.EndHour.ShouldBe(hotelDayOptions.EndHour);
    }
    
    [Fact]
    public void Create_WithIncorrectOptionsHours_ShouldThrow()
    {
        // Arrange
        var hotelDayOptions = new HotelDayOptions
        {
            StartHour = new TimeOnly(10,00),
            EndHour = new TimeOnly(14,00)
        };
        _options.Value.Returns(hotelDayOptions);
        var hotelDayFactory = new HotelDayFactory(_options);
        
        // Act
        var exception = Record.Exception(() => hotelDayFactory.Create());
        
        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<HotelDayInvalidHoursException>();
    }
}