using DigitalBankDDD.Application.Interfaces;
using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Exceptions;
using DigitalBankDDD.Domain.Interfaces;

namespace DigitalBankDDD.Application.Services;

public sealed class AccountService : IAccountService
{
    private readonly IAccountDomainService _accountDomainService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Account> _accountRepository;

    public AccountService(IAccountDomainService accountDomainService, IUnitOfWork unitOfWork)
    {
        _accountDomainService = accountDomainService;
        _unitOfWork = unitOfWork;
        _accountRepository = unitOfWork.GetRepository<Account>();
    }
    
    public async Task<ApiResult<Account>> CreateAccountAsync(Account account)
    {
        try
        {
            if (await _accountRepository.GetAsync(a => a.Cpf == account.Cpf) is not null)
                return ApiResult<Account>.Failure("This CPF is already in use.");
            
            if(await _accountRepository.GetAsync(a => a.Email == account.Email) is not null)
                return ApiResult<Account>.Failure("This email is already in use.");
            
            _accountDomainService.ValidateAccountCreation(account);

            var createdAccount = _accountRepository.Save(account);
            
            await _unitOfWork.CommitAsync();

            return ApiResult<Account>.Success(createdAccount);
        }
        catch (DomainException ex)
        {
            return ApiResult<Account>.Failure(ex.Message);
        }
    }
}