using DigitalBankDDD.Domain.Wrapper;

namespace DigitalBankDDD.Domain.Entities;

public class Account : BaseEntity
{
    public string AccountNumber { get; init; } = Guid.NewGuid().ToString().Substring(0, 10);
    public decimal Balance { get; private set; } = 0;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public DomainResult TransferTo(Account destinationAccount, decimal amount)
    {
        if(amount <= 0)
            return DomainResult.Failure("The amount must be greater than zero.");
        
        if (!HasBalance(amount))
            return DomainResult.Failure("Insufficient balance.");
        
        Withdraw(amount);
        destinationAccount.Deposit(amount);
        
        return DomainResult.Success();
    }
    
    private DomainResult Deposit(decimal amount)
    {
        if (amount <= 0)
            return DomainResult.Failure("The amount must be greater than zero.");
        
        Balance += amount;
        
        return DomainResult.Success();
    }

    private DomainResult Withdraw(decimal amount)
    {
        if (amount <= 0)
            return DomainResult.Failure("The amount must be greater than zero.");
        
        Balance -= amount;
        
        return DomainResult.Success();
    }
    
    private bool HasBalance(decimal amount)
    {
        return Balance >= amount;
    }
}