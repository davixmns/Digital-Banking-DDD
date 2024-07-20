using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DigitalBankDDD.Domain.Entities;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }
    
    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    public int FromAccountId { get; set; }
    
    [Required]
    public int ToAccountId { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
}