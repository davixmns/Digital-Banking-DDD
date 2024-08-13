using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Utils;
using FluentValidation;

namespace DigitalBankDDD.Application.Validators;

public class AccountValidator : AbstractValidator<Account>
{
    public AccountValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long");
        
        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("CPF is required")
            .Must(CpfValidator.IsValid).WithMessage("Invalid CPF");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Birth Date is required");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required")
            .Must(PhoneNumberValidator.IsValid).WithMessage("Invalid Phone Number");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
    }
}