using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;

namespace DigitalBankDDD.Application.Interfaces;

public interface ITransactionService
{
    Task<ApiResult<Transaction>> CreateTransactionAsync(Account fromAccount, Account toAccount, double amount);
}