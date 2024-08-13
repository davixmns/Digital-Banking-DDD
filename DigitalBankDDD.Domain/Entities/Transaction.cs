using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using DigitalBankDDD.Domain.ValueObjects;

namespace DigitalBankDDD.Domain.Entities;

public class Transaction : BaseEntity
{
    public Amount Amount { get; set; }
    public int FromAccountId { get; set; }
    public Account FromAccount { get; set; }
    public int ToAccountId { get; set; }
    public Account ToAccount { get; set; }
    public string Description { get; set; }
    public string Hash { get; private set; }
    public DateTime CreatedAt { get; set; }
    
    public Transaction() 
    { }
    
    public Transaction(Amount amount, Account fromAccount, Account toAccount, string description)
    {
        Amount = amount;
        FromAccount = fromAccount;
        FromAccountId = fromAccount.Id;
        ToAccount = toAccount;
        ToAccountId = toAccount.Id;
        Description = description;
        CreatedAt = DateTime.Now;
        Hash = Guid.NewGuid().ToString();
    }
}