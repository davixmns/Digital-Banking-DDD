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
        if (await AccountExistsAsync(account.Email!))
            throw new DomainException("Account already exists.");

        var createdAccount = _accountRepository.Save(account);
        
        await _unitOfWork.CommitAsync();

        return createdAccount;
    }
    
    private async Task<bool> AccountExistsAsync(string email)
    {
        var accountExists = await _accountRepository.GetAsync(a => a.Email == email);
        return accountExists != null;
    }
}