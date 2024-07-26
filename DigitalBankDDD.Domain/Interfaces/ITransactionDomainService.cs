using DigitalBankDDD.Domain.Entities;

namespace DigitalBankDDD.Domain.Interfaces;

public interface ITransactionDomainService
{
    Task<Transaction> CreateTransactionAsync(Account fromAccount, Account toAccount, double amount);
}