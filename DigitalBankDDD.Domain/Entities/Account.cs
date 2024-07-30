using System.ComponentModel.DataAnnotations;
using DigitalBankDDD.Domain.Exceptions;

namespace DigitalBankDDD.Domain.Entities;

public class Account : BaseEntity
{
    public string AccountNumber { get; private set; } = Guid.NewGuid().ToString().Substring(0, 10);
    
    //se colocar como privado o EF core não consegue mapear
    public decimal Balance { get; private set; } = 0;
    
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
    
    [Required]
    [EmailAddress]
    [StringLength(50)]
    public string? Email { get; set; }
    
    [StringLength(20)]
    [RegularExpression(@"\d{11}", ErrorMessage = "Invalid CPF size")]
    public string Cpf { get; init; } = string.Empty;
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
    
    
    [StringLength(20)]
    [RegularExpression(@"^(\+[0-9]{2,3}\s?)?\(?[0-9]{2}\)?\s?[0-9]{5}-?[0-9]{4}$", ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; }
    
    [Required] 
    [MinLength(8)] 
    public string? Password { get; set; } = string.Empty;

    public void TransferTo(Account destinationAccount, decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("The amount must be greater than zero.");
        
        if (!HasBalance(amount))
            throw new DomainException("Insufficient balance.");
        
        Withdraw(amount);
        destinationAccount.Deposit(amount);
    }
    
    private void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("The amount must be greater than zero.");
        
        Balance += amount;
    }

    private void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("The amount must be greater than zero.");

        if (!HasBalance(amount))
            throw new DomainException("Insufficient balance.");
        
        Balance -= amount;
    }
    
    private bool HasBalance(decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("The amount must be greater than zero.");

        return Balance >= amount;
    }
}