using AutoMapper;
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
    private readonly IMapper _mapper;

    public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _transactionRepository = _unitOfWork.GetRepository<Transaction>();
        _accountRepository = _unitOfWork.GetRepository<Account>();
        _mapper = mapper;
    }
    
    public async Task<ApiResult<TransactionResponseDto>> CreateTransactionAsync(TransactionRequestDto transactionRequestDto)
    {
        try
        {
            var fromAccount = await _accountRepository.GetAsync(a => a.Id == transactionRequestDto.FromAccountId);
            var toAccount = await _accountRepository.GetAsync(a => a.Id == transactionRequestDto.ToAccountId);

            if (fromAccount is null || toAccount is null)
                return ApiResult<TransactionResponseDto>.Failure("Account not found.");

            fromAccount.TransferTo(toAccount, transactionRequestDto.Amount);
            
            var transaction = new Transaction(
                amount: transactionRequestDto.Amount,
                fromAccount: fromAccount,
                toAccount: toAccount,
                description: transactionRequestDto.Description
            );
            
            var createdTransaction = _transactionRepository.Save(transaction);
            
            await _unitOfWork.CommitAsync();
            
            return ApiResult<TransactionResponseDto>.Success(_mapper.Map<TransactionResponseDto>(createdTransaction));
        }
        catch (DomainException exception)
        {
            await _unitOfWork.RollBackAsync();
            return ApiResult<TransactionResponseDto>.Failure(exception.Message);
        }
    }
}