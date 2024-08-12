namespace DigitalBankDDD.Application.Dtos;

public class AccountResponseDto
{
    public string AccountNumber { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}