using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;

namespace DigitalBankDDD.Application.Interfaces;

public interface IAccountService
{
    Task<ApiResult<Account>> CreateAccountAsync(Account account);
}