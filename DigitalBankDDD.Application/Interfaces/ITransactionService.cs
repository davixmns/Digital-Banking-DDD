using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.ValueObjects;

namespace DigitalBankDDD.Application.Interfaces;

public interface ITransactionService
{
    Task<AppResult<TransactionResponseDto>> CreateTransactionAsync(Account fromAccount, Account toAccount, Amount amount, string description);
}