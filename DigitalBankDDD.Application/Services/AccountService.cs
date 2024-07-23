using DigitalBankDDD.Application.Interfaces;
using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Exceptions;
using DigitalBankDDD.Domain.Interfaces;

namespace DigitalBankDDD.Application.Services;

public sealed class AccountService : IAccountService
{
    private readonly IAccountDomainService _accountDomainService;
    
    public AccountService(IAccountDomainService accountDomainService)
    {
        _accountDomainService = accountDomainService;
    }
    
    public async Task<ApiResult<Account>> CreateAccountAsync(Account account)
    {
        try
        {
            var createdAccount = await _accountDomainService.CreateAccountAsync(account);
            return ApiResult<Account>.Success(createdAccount);
        }
        catch (DomainException ex)
        {
            return ApiResult<Account>.Failure(ex.Message);
        }
    }
}