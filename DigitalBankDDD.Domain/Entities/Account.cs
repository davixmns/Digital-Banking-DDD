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