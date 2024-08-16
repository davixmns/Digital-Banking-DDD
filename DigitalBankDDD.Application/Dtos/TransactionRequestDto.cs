using System.ComponentModel.DataAnnotations;
using DigitalBankDDD.Domain.Entities;
using DigitalBankDDD.Domain.ValueObjects;

namespace DigitalBankDDD.Application.Dtos;

public class TransactionRequestDto
{
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public string Description { get; set; } = string.Empty;
    public Amount Amount { get; set; }
}