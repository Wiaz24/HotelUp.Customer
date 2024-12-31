using HotelUp.Customer.Domain.ValueObjects;
using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Shouldly;

namespace HotelUp.Customer.Unit.Domain.ValueObjects.Reservation;

public class ReservationPeriodTests
{
    [Fact]
    public void Ctor_WithValidDates_CreatesReservationPeriod()
    {
        // Arrange
        var from = DateTime.Now;
        var to = DateTime.Now.AddDays(1);

        // Act
        var reservationPeriod = new ReservationPeriod(from, to);

        // Assert
        reservationPeriod.From.ShouldBe(from);
        reservationPeriod.To.ShouldBe(to);
    }
    
    [Fact]
    public void Ctor_WithFromDateGreaterThanToDate_ThrowsReservationPeriodInvalidDatesException()
    {
        // Arrange
        var from = DateTime.Now;
        var to = DateTime.Now.AddDays(-1);

        // Act
        var exception = Record.Exception(() => new ReservationPeriod(from, to));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ReservationPeriodInvalidDatesException>();
    }
    
    [Theory]
    [InlineData(3, 12)]
    [InlineData(-3, 3)]
    [InlineData(-3, 12)]
    [InlineData(3, 5)]
    public void ReservationPeriod_PartiallyOverlapsWith_WhenPeriodsOverlaps_ShouldReturnTrue(int fromDays, int toDays)
    {
        // Arrange
        var basePeriod = new ReservationPeriod(DateTime.Now, DateTime.Now.AddDays(7));
        var otherPeriod = new ReservationPeriod(DateTime.Now.AddDays(fromDays), DateTime.Now.AddDays(toDays));
        // Act
        var result = basePeriod.PartiallyOverlapsWith(otherPeriod);

        // Assert
        result.ShouldBeTrue();
    }
    
    [Theory]
    [InlineData(-7, -3)]
    [InlineData(7, 10)]
    public void ReservationPeriod_PartiallyOverlapsWith_WhenPeriodsDoNotOverlap_ShouldReturnFalse(int fromDays, int toDays)
    {
        // Arrange
        var basePeriod = new ReservationPeriod(DateTime.Now, DateTime.Now.AddDays(7));
        var otherPeriod = new ReservationPeriod(DateTime.Now.AddDays(fromDays), DateTime.Now.AddDays(toDays));
        // Act
        var result = basePeriod.PartiallyOverlapsWith(otherPeriod);

        // Assert
        result.ShouldBeFalse();
    }
    
}