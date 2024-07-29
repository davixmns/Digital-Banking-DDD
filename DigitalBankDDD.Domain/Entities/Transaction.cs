using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace DigitalBankDDD.Domain.Entities;

public class Transaction : BaseEntity
{
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }
    
    [Required]
    public int FromAccountId { get; set; }
    
    [Required]
    public int ToAccountId { get; set; }
    
    [JsonIgnore]
    public Account FromAccount { get; set; }
    
    [JsonIgnore]
    public Account ToAccount { get; set; }
    
    public string? Description { get; set; }
    
    public string? Hash { get; private set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Transaction()
    {
    }
    
    public Transaction(decimal amount, Account fromAccount, Account toAccount, string description)
    {
        Amount = amount;
        FromAccount = fromAccount;
        FromAccountId = fromAccount.Id;
        ToAccount = toAccount;
        ToAccountId = toAccount.Id;
        Description = description;
        CreatedAt = DateTime.Now;
        Hash = GenerateHash();
    }

    private string GenerateHash()
    {
        var inputString = $"{Id}{Amount}{FromAccountId}{ToAccountId}{Description}{CreatedAt}";
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
}