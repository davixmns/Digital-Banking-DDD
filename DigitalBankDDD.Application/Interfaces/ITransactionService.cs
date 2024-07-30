using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;

namespace DigitalBankDDD.Application.Interfaces;

public interface ITransactionService
{
    Task<ApiResult<TransactionResponseDto>> CreateTransactionAsync(TransactionRequestDto transactionRequestDto);
}