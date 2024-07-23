using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

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
    
    public string? Hash { get; private set; }
    
    public DateTime CreatedAt { get; set; }

    public Transaction()
    {
        Hash = GenerateHash();
    }

    private string GenerateHash()
    {
        var inputString = $"{TransactionId}{Amount}{FromAccountId}{ToAccountId}{Description}{CreatedAt}";
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
}