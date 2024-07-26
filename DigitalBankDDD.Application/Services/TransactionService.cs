using DigitalBankDDD.Application.Interfaces;
using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Exceptions;
using DigitalBankDDD.Domain.Interfaces;

namespace DigitalBankDDD.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionDomainService _transactionDomainService;

    public TransactionService(ITransactionDomainService transactionDomainService)
    {
        _transactionDomainService = transactionDomainService;
    }
    
    public async Task<ApiResult<Transaction>> CreateTransactionAsync(Account fromAccount, Account toAccount, double amount)
    {
        try
        {
            var completeTransaction = await _transactionDomainService.CreateTransactionAsync(fromAccount, toAccount, amount);
            return ApiResult<Transaction>.Success(completeTransaction);
        }
        catch (DomainException exception)
        {
            return ApiResult<Transaction>.Failure(exception.Message);
        }
    }
}