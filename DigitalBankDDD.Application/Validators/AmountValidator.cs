using DigitalBankDDD.Domain.ValueObjects;
using FluentValidation;

namespace DigitalBankDDD.Application.Validators;

public class AmountValidator : AbstractValidator<Amount>
{
    public AmountValidator()
    {
        RuleFor(x => x.Value)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");
    }
}