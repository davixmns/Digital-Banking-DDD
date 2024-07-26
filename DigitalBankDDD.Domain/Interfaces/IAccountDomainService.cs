using System.Linq.Expressions;
using DigitalBankDDD.Domain.Entities;

namespace DigitalBankDDD.Domain.Interfaces;

public interface IAccountDomainService
{
    Task<Account> CreateAccountAsync(Account account);
    Task<Account?> GetAccountAsync(Expression<Func<Account, bool>> predicate);
}