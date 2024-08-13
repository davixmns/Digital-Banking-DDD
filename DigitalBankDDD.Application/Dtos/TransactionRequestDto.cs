using System.ComponentModel.DataAnnotations;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.ValueObjects;

namespace DigitalBankDDD.Application.Dtos;

public class TransactionRequestDto
{
    public int FromAccountId { get; set; }
    public Account? FromAccount { get; set; }
    public int ToAccountId { get; set; }
    public Account? ToAccount { get; set; }
    public string Description { get; set; } = string.Empty;
    
    public Amount Amount { get; set; }
}