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
    
    
    [StringLength(20)] //aceiar telefones nesse formato +55 11 99999-9999
    [RegularExpression(@"^[+]{1}(?:[0-9\-\(\)\/\.]\s?){6, 15}[0-9]{1}$", ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; }
    
    [Required] 
    [MinLength(12)] 
    public string? Password { get; set; } = string.Empty;
    
    [JsonIgnore]
    public List<Transaction>? Transactions { get; set; }
    
    public Account()
    {
        Transactions = new List<Transaction>();
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