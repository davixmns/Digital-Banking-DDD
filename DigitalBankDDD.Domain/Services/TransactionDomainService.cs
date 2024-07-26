using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.Interfaces;

namespace DigitalBankDDD.Domain.Services;

public class TransactionDomainService : ITransactionDomainService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Transaction> _transactionRepository;

    public TransactionDomainService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _transactionRepository = _unitOfWork.GetRepository<Transaction>();
    }
    
    public Task<Transaction> CreateTransactionAsync(Account fromAccount, Account toAccount, double amount)
    {
        throw new System.NotImplementedException();
    }
}