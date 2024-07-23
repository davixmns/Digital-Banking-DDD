using DigitalBankDDD.Domain.Entities;

namespace DigitalBankDDD.Domain.Interfaces;

public interface IAccountDomainService
{
    Task<Account> CreateAccountAsync(Account account);
}