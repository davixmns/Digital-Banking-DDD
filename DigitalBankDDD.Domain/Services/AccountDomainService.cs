using System.Linq.Expressions;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Exceptions;
using DigitalBankDDD.Domain.Interfaces;

namespace DigitalBankDDD.Domain.Services;

public sealed class AccountDomainService : IAccountDomainService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Account> _accountRepository;
    
    public AccountDomainService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _accountRepository = _unitOfWork.GetRepository<Account>();
    }

    public async Task<Account> CreateAccountAsync(Account account)
    {
        if (await GetAccountAsync(a => a.Email == account.Email) is not null)
            throw new DomainException("This email is already in use.");

        if (await GetAccountAsync(a => a.Cpf == account.Cpf) is not null)
            throw new DomainException("This CPF is already in use.");

        var createdAccount = _accountRepository.Save(account);
        
        await _unitOfWork.CommitAsync();

        return createdAccount;
    }
    
    public async Task<Account?> GetAccountAsync(Expression<Func<Account, bool>> predicate)
    {
        return await _accountRepository.GetAsync(predicate);
    }
}