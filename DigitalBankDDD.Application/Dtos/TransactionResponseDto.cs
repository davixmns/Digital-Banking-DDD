using DigitalBankDDD.Domain.ValueObjects;

namespace DigitalBankDDD.Application.Dtos;

public class TransactionResponseDto
{
    public Amount Amount { get; set; }
    public string Hash { get; set; }
    public string Description { get; set; }
    public AccountResponseDto ToAccount { get; set; }
    public AccountResponseDto FromAccount { get; set; }
    
    public TransactionResponseDto(Amount amount, string hash, string description, AccountResponseDto toAccount, AccountResponseDto fromAccount)
    {
        Amount = amount;
        Hash = hash;
        Description = description;
        ToAccount = toAccount;
        FromAccount = fromAccount;
    }
}