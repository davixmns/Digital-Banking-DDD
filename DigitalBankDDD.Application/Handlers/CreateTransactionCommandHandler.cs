using DigitalBankDDD.Application.Commands;
using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Application.Interfaces;
using DigitalBankDDD.Application.Wrapper;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Interfaces;
using MediatR;

namespace DigitalBankDDD.Application.Handlers;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, AppResult<TransactionResponseDto>>
{
    private readonly ITransactionService _transactionService;
    private readonly IRepository<Account> _accountRepository;
    
    public CreateTransactionCommandHandler(ITransactionService transactionService, IUnitOfWork unitOfWork)
    {
        _transactionService = transactionService;
        _accountRepository = unitOfWork.GetRepository<Account>();
    }
    
    public async Task<AppResult<TransactionResponseDto>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var fromAccount = await _accountRepository.GetAsync(a => a.Id == request.FromAccountId);
        var toAccount = await _accountRepository.GetAsync(a => a.Id == request.ToAccountId);

        return await _transactionService.CreateTransactionAsync(
            fromAccount: fromAccount!,
            toAccount: toAccount!,
            amount: request.Amount,
            description: request.Description
        );
    }
}