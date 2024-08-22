using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.ValueObjects;
using MediatR;

namespace DigitalBankDDD.Application.Commands;

public class CreateTransactionCommand : IRequest<AppResult<TransactionResponseDto>>
{
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public string Description { get; set; }
    public Amount Amount { get; set; }

    public CreateTransactionCommand(TransactionRequestDto transactionRequestDto)
    {
        FromAccountId = transactionRequestDto.FromAccountId;
        ToAccountId = transactionRequestDto.ToAccountId;
        Description = transactionRequestDto.Description;
        Amount = transactionRequestDto.Amount;
    }
}