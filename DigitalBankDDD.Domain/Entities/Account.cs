using DigitalBankDDD.Domain.ValueObjects;
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
    
    public DomainResult TransferTo(Account destinationAccount, Amount amount)
    {
        if (!HasBalance(amount))
            return DomainResult.Failure("Insufficient balance.");
        
        Withdraw(amount);
        destinationAccount.Deposit(amount);
        
        return DomainResult.Success();
    }
    
    private void Deposit(Amount amount)
    {
        Balance += amount.Value;
    }

    private void Withdraw(Amount amount)
    {
        Balance -= amount.Value;
    }
    
    private bool HasBalance(Amount amount)
    {
        return Balance >= amount.Value;
    }
}