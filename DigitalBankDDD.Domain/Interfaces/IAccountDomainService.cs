using System.Linq.Expressions;
using DigitalBankDDD.Domain.Entities;

namespace DigitalBankDDD.Domain.Interfaces;

public interface IAccountDomainService
{
    void ValidateAccountCreation(Account account);
}