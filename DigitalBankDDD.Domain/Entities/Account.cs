using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DigitalBankDDD.Domain.Entities;

public class Account : BaseEntity
{
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
    public DateOnly BirthDate { get; set; }
    
    
    [StringLength(20)]
    [RegularExpression(@"^(\+[0-9]{2,3}\s?)?\(?[0-9]{2}\)?\s?[0-9]{5}-?[0-9]{4}$", ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; }
    
    [Required] 
    [MinLength(12)] 
    public string? Password { get; set; } = string.Empty;
    
    public Account()
    {
    }
    
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("The amount must be greater than zero.");
        
        Balance += amount;
    }
    
    public void Withdraw(decimal amount)
    {
        Balance -= amount;
    }
    
    public bool HasBalance(decimal amount)
    {
        return Balance >= amount;
    }
}