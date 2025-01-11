using FluentValidation;
using HotelUp.Customer.Domain.ValueObjects.Abstractions;
using HotelUp.Customer.Domain.ValueObjects.Exceptions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelUp.Customer.Domain.ValueObjects;

public record Email : IValueObject
{
    private string Value { get; init; } = null!;
    private Email() { }
    private Email(string value)
    {
        var email = new Email()
        {
            Value = value
        };
        var validator = new EmailValidator();
        var result = validator.Validate(email);
        if (!result.IsValid)
        {
            var failure = result.Errors.FirstOrDefault();
            throw new InvalidEmailException(failure?.ErrorMessage);
        }
        Value = value;
    }

    private class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .EmailAddress();
        }
    }
    public static implicit operator string(Email email) => email.Value;
    public static implicit operator Email(string value) => new(value);
    public static ValueConverter GetValueConverter()
    {
        return new ValueConverter<Email, string>(
            vo => vo.Value,
            value => new Email(value));
    }
}