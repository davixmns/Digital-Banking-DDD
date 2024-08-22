using DigitalBankDDD.Application.Commands;
using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Extensions;
using DigitalBankDDD.Application.Utils;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Interfaces;
using FluentValidation;

namespace DigitalBankDDD.Application.Validators;

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    private readonly IRepository<Account> _accountRepository;
    
    public CreateTransactionCommandValidator(IUnitOfWork unitOfWork)
    {
        _accountRepository = unitOfWork.GetRepository<Account>();

        RuleFor(x => x.Amount)
            .SetValidator(new AmountValidator());
        
        RuleFor(x => x.FromAccountId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("From Account Id is required.")
            .GreaterThan(0).WithMessage("From Account Id must be greater than 0.")
            .AccountExists(_accountRepository).WithMessage("From Account does not exist.");

        RuleFor(x => x.ToAccountId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("To Account Id is required.")
            .GreaterThan(0).WithMessage("To Account Id must be greater than 0.")
            .AccountExists(_accountRepository).WithMessage("To Account does not exist.");
    }
}