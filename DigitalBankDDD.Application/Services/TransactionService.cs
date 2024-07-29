using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Interfaces;
using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Exceptions;
using DigitalBankDDD.Domain.Interfaces;

namespace DigitalBankDDD.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Transaction> _transactionRepository;
    private readonly IRepository<Account> _accountRepository;

    public TransactionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _transactionRepository = _unitOfWork.GetRepository<Transaction>();
        _accountRepository = _unitOfWork.GetRepository<Account>();
    }
    
    public async Task<ApiResult<Transaction>> CreateTransactionAsync(TransactionRequestDto transactionRequestDto)
    {
        try
        {
            var fromAccount = await _accountRepository.GetAsync(a => a.Id == transactionRequestDto.FromAccountId);
            var toAccount = await _accountRepository.GetAsync(a => a.Id == transactionRequestDto.ToAccountId);
            
            if (fromAccount is null || toAccount is null)
                return ApiResult<Transaction>.Failure("Account not found.");
            
            if(fromAccount.HasBalance((decimal) transactionRequestDto.Amount))
                return ApiResult<Transaction>.Failure("Insufficient balance.");

            var transaction = new Transaction(
                amount: (decimal) transactionRequestDto.Amount,
                fromAccount: fromAccount,
                toAccount: toAccount,
                description: transactionRequestDto.Description
            );
            
            fromAccount.Withdraw((decimal) transactionRequestDto.Amount);
            toAccount.Deposit((decimal) transactionRequestDto.Amount);
            
            var createdTransaction = _transactionRepository.Save(transaction);
            
            await _unitOfWork.CommitAsync();
            
            return ApiResult<Transaction>.Success(createdTransaction);
        }
        catch (DomainException exception)
        {
            return ApiResult<Transaction>.Failure(exception.Message);
        }
    }
}