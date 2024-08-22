using AutoMapper;
using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Interfaces;
using DigitalBankDDD.Application.Utils;
using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Interfaces;
using DigitalBankDDD.Domain.ValueObjects;

namespace DigitalBankDDD.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Transaction> _transactionRepository;
    private readonly IMapper _mapper;

    public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _transactionRepository = _unitOfWork.GetRepository<Transaction>();
        _mapper = mapper;
    }

    public async Task<AppResult<TransactionResponseDto>> CreateTransactionAsync(Account fromAccount, Account toAccount, Amount amount, string description)
    {
        var transferResult = fromAccount.TransferTo(toAccount, amount);

        if (!transferResult.IsSuccess)
            return AppResult<TransactionResponseDto>.Failure(transferResult.ErrorMessage);

        var createdTransaction = _transactionRepository.Save(new Transaction(
            amount: amount,
            fromAccount: fromAccount,
            toAccount: toAccount!,
            description: description
        ));

        await _unitOfWork.CommitAsync();
        
        var transactionResponseDto = _mapper.Map<TransactionResponseDto>(createdTransaction);
        
        return AppResult<TransactionResponseDto>.Success(transactionResponseDto);
    }
}