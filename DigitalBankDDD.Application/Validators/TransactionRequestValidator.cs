using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Interfaces;
using FluentValidation;

namespace DigitalBankDDD.Application.Validators;

public class TransactionRequestValidator : AbstractValidator<TransactionRequestDto>
{
    private readonly IRepository<Account> _accountRepository;
    
    public TransactionRequestValidator(IUnitOfWork unitOfWork)
    {
        _accountRepository = unitOfWork.GetRepository<Account>();

        RuleFor(x => x.FromAccountId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("From Account Id is required.")
            .GreaterThan(0).WithMessage("From Account Id must be greater than 0.");
            // .Must(AccountExists).WithMessage("From Account not found.");

            RuleFor(x => x.ToAccountId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("To Account Id is required.")
                .GreaterThan(0).WithMessage("To Account Id must be greater than 0.");
            // .Must(AccountExists).WithMessage("To Account not found.");

        RuleFor(x => x.Amount)
            .SetValidator(new AmountValidator());
    }

    private bool AccountExists(TransactionRequestDto dto, int accountId)
    {
        var account = _accountRepository.GetAsync(a => a.Id == accountId).Result;

        if (account != null)
        {
            if (dto.FromAccountId == accountId)
            {
                dto.FromAccount = account;
            }
            else if (dto.ToAccountId == accountId)
            {
                dto.ToAccount = account;
            }

            return true;
        }

        return false;
    }
}