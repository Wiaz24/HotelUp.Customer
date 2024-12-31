using FluentValidation;
using HotelUp.Customer.Domain.ValueObjects.Exceptions;

namespace HotelUp.Customer.Domain.ValueObjects;

public record Pesel
{
    public string Value { get; init; }

    private Pesel(){}
    
    public Pesel(string value)
    {
        var pesel = new Pesel()
        {
            Value = value
        };
        var validator = new PeselValidator();
        var result = validator.Validate(pesel);
        if (!result.IsValid)
        {
            var failure = result.Errors.FirstOrDefault();
            throw new InvalidPeselException(failure?.ErrorMessage);
        }
        Value = value;
    }
    
    public static implicit operator string(Pesel pesel) => pesel.Value;
    public static implicit operator Pesel(string pesel) => new(pesel);
    
    private class PeselValidator : AbstractValidator<Pesel>
    {
        public PeselValidator()
        {
            RuleFor(pesel => pesel.Value)
                .NotEmpty().WithMessage("The PESEL number cannot be empty.")
                .Length(11).WithMessage("The PESEL number must be exactly 11 digits long.")
                .Must(BeANumber).WithMessage("The PESEL number must contain only digits.");
        }

        private bool BeANumber(string pesel)
        {
            return long.TryParse(pesel, out _);
        }
    }
}