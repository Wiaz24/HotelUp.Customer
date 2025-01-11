using FluentValidation;
using HotelUp.Customer.Domain.ValueObjects.Abstractions;
using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Customer.Domain.ValueObjects;

public record PhoneNumber : IValueObject
{
    public string Value { get; private init; } = null!;

    private PhoneNumber()
    {
    }

    private PhoneNumber(string value)
    {
        var phoneNumber = new PhoneNumber
        {
            Value = value
        };
        var validator = new PhoneNumberValidator();
        var result = validator.Validate(phoneNumber);
        if (!result.IsValid)
        {
            var failure = result.Errors.FirstOrDefault();
            throw new InvalidPhoneNumberException(failure?.ErrorMessage);
        }
        Value = value;
    }
    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
    public static implicit operator PhoneNumber(string value) => new(value);
    private class PhoneNumberValidator : AbstractValidator<PhoneNumber>
    {
        public PhoneNumberValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .Matches(@"^(\+)?(\s*\d+\s*){9,}$");
        }
    }

    public static ValueConverter GetValueConverter()
    {
        return new ValueConverter<PhoneNumber, string>(
            vo => vo.Value,
            value => new PhoneNumber(value));
    }
}