using DigitalBankDDD.Application.Dtos;
using FluentValidation;
using System.Text.RegularExpressions;
using FluentValidation.Validators;

namespace DigitalBankDDD.Web.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.EmailOrCpf)
            .NotEmpty().WithMessage("Email or Cpf is required")
            .Must(BeAValidEmailOrCpf).WithMessage("Invalid Email or CPF");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
    }

    private bool BeAValidEmailOrCpf(string emailOrCpf)
    {
        var cpfRegex = new Regex(@"^\d{11}$"); 
        var cpfOk = cpfRegex.IsMatch(emailOrCpf);
        var emailOk = new EmailValidator<string>().IsValid(null, emailOrCpf);
        return cpfOk || emailOk;
    }
}