using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DigitalBankDDD.Domain.Entities;

public class Account
{
    [Key]
    public int AccountId { get; set; }
    
    public decimal Balance { get; set; }
    
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }
    
    [EmailAddress]
    [StringLength(50)]
    public string? Email { get; set; }
    
    [StringLength(20)]
    [MinLength(11)]
    public string? Cpf { get; set; }
    
    [Required] 
    [MinLength(12)] 
    public string? Password { get; set; }
    
    [JsonIgnore]
    public List<Transaction>? Transactions { get; set; }
    
    public Account()
    {
        Transactions = new List<Transaction>();
    }
}