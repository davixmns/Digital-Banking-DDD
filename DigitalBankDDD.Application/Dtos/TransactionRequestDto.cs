using System.ComponentModel.DataAnnotations;

namespace DigitalBankDDD.Application.Dtos;

public class TransactionRequestDto
{
    [Required]
    public int FromAccountId { get; set; }
    
    [Required]
    public int ToAccountId { get; set; }
    
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, double.MaxValue)]
    public double Amount { get; set; }
}