using HotelUp.Customer.Domain.ValueObjects;
using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Shouldly;

namespace HotelUp.Customer.Unit.Domain.ValueObjects.Room;

public class ImageUrlTests
{
    [Theory]
    [InlineData("http://www.yourserver.com/logo.png")]
    [InlineData("https://www.yourserver.com/logo.png")]
    public void WithCorrectProtocol_ShouldCreateImageUrl(string url)
    {
        // Act
        var exception = Record.Exception(() => new ImageUrl(url));

        // Assert
        exception.ShouldBeNull();
    }
    
    [Theory]
    [InlineData("wss://www.yourserver.com/logo.png")]
    [InlineData("ftp://www.yourserver.com/logo.png")]
    [InlineData("http:/www.yourserver.com/logo.png")]
    public void WithIncorrectProtocol_ShouldThrowInvalidImageUrlException(string url)
    {
        // Act
        var exception = Record.Exception(() => new ImageUrl(url));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidImageUrlException>();
    }
    
    [Theory]
    [InlineData("http://www.yourserver.com/logo.png")]
    [InlineData("http://yourserver.com/logo.jpg")]
    [InlineData("http://www.yourserver.com/logo.jpeg")]
    public void WithCorrectImageFormat_ShouldCreateImageUrl(string url)
    {
        // Act
        var exception = Record.Exception(() => new ImageUrl(url));

        // Assert
        exception.ShouldBeNull();
    }
    
    [Theory]
    [InlineData("http://www.yourserver.com/logo.gif")]
    [InlineData("http://www.yourserver.com/logo.bmp")]
    public void WithIncorrectImageFormat_ShouldThrowInvalidImageUrlException(string url)
    {
        // Act
        var exception = Record.Exception(() => new ImageUrl(url));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidImageUrlException>();
    }
    
}